using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.IO;



public static class M_Math  {

    
    
    
	public static GameObject GetClosestGameObject(Vector3 originPosition, GameObject[] objects){
		
		if( objects.Length == 0) return null;
		
		GameObject best = objects[0];
	
		float closestDistance = Vector3.Magnitude( best.transform.position - originPosition);

		for(int i = 1 ; i < objects.Length; i++){
			float currentDistance = Vector3.Magnitude( objects[i].transform.position - originPosition);
			if(currentDistance < closestDistance){
				best = objects[i];
				closestDistance = currentDistance;
			}
		}
	
		return best;
	}



    public static Action AddTypedAction<T>(this Action action, T _object, Action<T> call)
    {
        Action new_del = () => call(_object);
        action += new_del;
        return new_del;

    }


    /// <summary>
    /// Clamps a position within bounds. If it exeeds the bounds, the closest point to bounds is returned
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="bounds"></param>
    /// <returns></returns>
    public static Vector3 ClampInBounds(Vector3 vector, Bounds bounds)
    {
        if (bounds.Contains(vector))
        {          
            return vector;
        }
        else
        {
            Vector3 clamped = bounds.ClosestPoint(vector);
            vector = clamped;     
            return vector;
        }
    }


    public static bool Roll(float chance)
    {
        return chance > UnityEngine.Random.value;
    }
    public static List<T> EnumList<T>(){
        var v = Enum.GetValues(typeof(T));
        return v.Cast<T>().ToList();
    }
    public static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T) v.GetValue(UnityEngine.Random.Range(0, v.Length - 1));
        ;
    }

    public static IEnumerator DelayForFrames(int frames)
    {
        for(int i = 0; i < frames; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }
    public static IEnumerator ExecuteDelayed(float time, Action func)
    {
        yield return new WaitForSeconds(time);
        func();
    }

    public static Vector3[] GetTransformBoundPositionTop(Transform transform)
    {
        Vector3[] ret = new Vector3[4];

        Bounds b = new Bounds(transform.position, transform.localScale);

        Vector3 range = b.extents;
        range.y = 0;
        ret[0] = transform.position + range;

        range.x *= -1;
        ret[1] = transform.position + range;

        range.z *= -1;
        ret[2] = transform.position + range;

        range.x *= -1;
        ret[3] = transform.position + range;

        return ret;

    }

    public static List<RaycastHit> GetObjectsFromRays(List<Ray> rays, string tag)
    {
        List<RaycastHit> hits = new List<RaycastHit>();

       foreach(Ray r in rays) {
            Debug.DrawRay(r.origin, r.direction * 5, Color.green, 10f);
            hits.AddRange( Physics.RaycastAll(r,Mathf.Infinity).Where(rhit => rhit.collider.gameObject.tag == tag));
        }

        return hits;
       
    }

     
    public static float GetPercentpointsOfValueInRange(float _value, float _min, float _max){
		if (_value < _min)
						return 0;
		if (_value > _max)
						return 1;
	
		return (_value - _min) / (_max - _min);

	}

    
    public static  float GetDistance2D(Vector3 v1, Vector3 v2)
    {
        v1.y = 0;
        v2.y = 0;

        return (v1 - v2).magnitude;
    }

    public static Vector3 GetVectorInRange(Vector3 Vector, float _min, float _max){
		Debug.Log (GetPercentpointsOfValueInRange (Vector.magnitude, _min, _max));
		return Vector.normalized * GetPercentpointsOfValueInRange(Vector.magnitude, _min, _max);
	}

    public static T CloneMono<T>(T item)
    {
      
        return MonoBehaviour.Instantiate((item as MonoBehaviour).gameObject).GetComponent<T>();
    }


    public static List<T> SpawnFromList<T>(List<T> list)
    {
        List<T> ret = new List<T>();
        foreach(var item in list)
        {
            if (item == null)
                continue;

            ret.Add(CloneMono(item));
        }

        return ret;
       
    }
    public static Vector3 GetInputPos(){

     
                 return GetMouseWorldPos();
		
	}
	public static Vector3 GetTouchWorldPos(){
		Vector3 pos = Vector3.zero;
		
		if( Input.touchCount > 0 ){
			pos = new Vector3( Input.touches[0].position.x, Input.touches[0].position.y, 0);
			return GetPlaneIntersectionY(Camera.main.ScreenPointToRay(pos));
		}
		return pos;
	}
	public static Vector3 GetMouseWorldPos(){	
		return GetPlaneIntersectionY(Camera.main.ScreenPointToRay(Input.mousePosition));		
	}
	public static Vector3 GetCameraCenter()
    {
        return GetPlaneIntersectionY(new Ray(Camera.main.transform.position,Camera.main.transform.forward));
    }
	public static Vector3 GetPlaneIntersectionY(Ray ray){
		
		float dist = Vector3.Dot (Vector3.up, Vector3.zero - ray.origin) / Vector3.Dot (Vector3.up, ray.direction.normalized);
		
		return ray.origin + ray.direction.normalized * dist;
		 
	} 

    public static int GetSecondsNow()
    {
        return ConvertToUnixTimestamp(System.DateTime.Now);
    }

    public static System.DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        System.DateTime origin = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
    }

    public static int ConvertToUnixTimestamp(System.DateTime date)
    {
        System.DateTime origin = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        System.TimeSpan diff = date.ToUniversalTime() - origin;
        return (int)(diff.TotalSeconds);
    }
    


    public static Vector3 GetInputPosToPlane()
    {

        if (Application.isEditor)
        {
            return GetMouseWorldPos();
        }
        else
        {
            return GetTouchWorldPos();
        }
    }

    public static Vector2 GetTouchMousePos()
    {
        if (Application.isEditor)
        {
            return Input.mousePosition;
        }
        else
        {
            return Input.touches[0].position;
        }
    }


    public static string GetStringFromSeconds(int seconds)
    {

        System.TimeSpan t = System.TimeSpan.FromSeconds(seconds);
        string timeText;
        int days = seconds / 86400;

        if (days >= 1)
        {
            string dayText = days > 1 ? "DAYS" : "DAY";
            timeText = string.Format("{0:D1} " + dayText + " - {1:D2}:{2:D2}:{3:D2}", t.Days, t.Hours, t.Minutes, t.Seconds);
        }
        else
        {
            timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
        }
        return timeText;

    }


    public static bool StringArContains(string[] ar, string s)
    {
        for (int i = 0; i < ar.Length; i++) if (ar[i] == s) return true;

        return false;
    }

    public static float VectorDot01(Vector3 _in, Vector3 _out)
    {
        return (Vector3.Dot(_in, _out) * 0.5f) + 0.5f;
    }

    public static Vector2 Get2DForward(Transform tr)
    {
        return new Vector2(tr.forward.x, tr.forward.y);
    }

    public static Vector2 Get2DUP(Transform tr)
    {
        return new Vector2(tr.up.x, tr.up.y);
    }

    public static List<Transform> AddChildrenToList(Transform parent)
    {
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < parent.childCount; i++)
        {
            children.Add(parent.GetChild(i));

        }

        return children;
    }

    public static T GetRandomObject<T>(List<T> objects)
    {
        if (objects == null || objects.Count == 0)
            return default(T) ;   
           
        return objects[UnityEngine.Random.Range(0, objects.Count)];
    }

    public static T GetRandomObject<T>(T[] objects)
    {
        if (objects == null || objects.Length == 0)
            return default(T);

         return objects[UnityEngine.Random.Range(0, objects.Length)];
    }

    public static T GetRandomObjectAndRemove<T>(List<T> objects)
    {
        if (objects.Count == 0) return default(T);

        T chosen = objects[UnityEngine.Random.Range(0, objects.Count)];
        objects.Remove(chosen);
        return chosen;
    }

    public static List<T> GetListFromObject<T>(T obj)
    {
        List<T> list = new List<T>();
        if(obj != null)
             list.Add(obj);
        return list;
    }

    /// <summary>
    /// Returns a certain amount of items randomly from a list 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public static List<T> GetRandomObjects<T>(List<T> list, int num)
    {
        int count = Mathf.Min(num, list.Count);
        List<T> ret = new List<T>();	
        List<T> copy = new List<T>(list);

        for(int i = 0; i < count; i++)
        {
            T item = GetRandomObject(copy);
            copy.Remove(item);
            ret.Add(item);
        }

        return ret;
    }

    

    public static void FadeText(Text t, int cycles, Color Color1, Color Color2, float time1, float time2)
    {
        t.StartCoroutine(FadeTextSequence(t, cycles, Color1, Color2, time1, time2));
    }
    static IEnumerator FadeTextSequence(Text t, int cycles, Color Color1, Color Color2, float time1, float time2)
    {
        int m_cycles = cycles;
        while (cycles <= 0 || m_cycles >= 0)
        {

            yield return t.StartCoroutine(FadeTextOnce(t, Color1, time1));
            m_cycles--;
            if (cycles <= 0 || m_cycles > 0)
            {
                yield return t.StartCoroutine(FadeTextOnce(t, Color2, time2));
            }
            m_cycles--;
            yield return null;  
        }
    }
    public static Quaternion RotateToYSnapped(  Vector3 from, Vector3 to, float snap)
    {

        Quaternion target = RotateToYFlat(from, to);
        float y = target.eulerAngles.y;
        float locked = Mathf.RoundToInt(y / snap) * snap;

        return   Quaternion.Euler(target.eulerAngles.x, locked, target.eulerAngles.z);
        
    }

    public static Quaternion RotateToYFlat(Vector3 from, Vector3 to)
    {
        Vector3 dir = (to - from);
        dir.y = 0;
        dir.Normalize();
        Debug.DrawRay(from, dir * 5);

        return Quaternion.LookRotation(dir);
    }
    static IEnumerator FadeTextOnce(Text tf, Color targetColor, float t)
    {
        Color startcolor = tf.color;
        float time = 0;
        while (time < t)
        {
            Color newColor = Color.Lerp(startcolor, targetColor, time / t);
            tf.color = newColor;
            time += Time.deltaTime;
            yield return null;
        }
        yield break;
    }


    public static float OrthogonalStrength(Vector2 _ref, Vector2 _in)
    {
        Vector2 perp = Vector3.Cross(_ref, new Vector3(0, 0, 1));
        return Vector2.Dot(perp, _in);

    }

    public static bool MouseTouchUp()
    {

        if (Application.isEditor)
        {
            return Input.GetMouseButtonUp(0);
        }
        else
        {
            return Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended);
        }
    }

    public static void SetListAsChild<T>(List<T> objects, Transform transform)
    {
        foreach (var item in objects)
        {
            (item as MonoBehaviour).transform.SetParent(transform);
        }
    }

    public static bool MouseTouchDown()
    {
        if (Application.isEditor)
        {
            return Input.GetMouseButtonDown(0);
        }
        else
        {
            return Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Began);
        }
    }

    public static bool MouseTouchHold()
    {

        if (Application.isEditor)
        {
            return Input.GetMouseButton(0);
        }
        else
        {
            return Input.touchCount > 0;
        }
    }

    public static void CopyTransform(Transform from, Transform to)
    {
        to.position = from.position;
        to.rotation = from.rotation;
        to.localScale = from.localScale;

    }

    /*https://gist.github.com/Arakade/9dd844c2f9c10e97e3d0*/

#if UNITY_EDITOR

    public static void SceneViewText(string text, Vector3 worldPos, Color? colour = null)
    {
        UnityEditor.Handles.BeginGUI();

        var view = UnityEditor.SceneView.currentDrawingSceneView;

		if(view != null){
	        Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
	        Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text)) * 1.5f;
	        GUI.Box(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height, size.x, size.y),"", GUI.skin.box );

	        if (colour.HasValue) GUI.color = colour.Value;
	        GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height , size.x, size.y), text, UnityEditor.EditorStyles.whiteBoldLabel );
	        UnityEditor.Handles.EndGUI();

	        GUI.color = Color.white;
		}
    }

    //http://wiki.unity3d.com/index.php?title=CreateScriptableObjectAsset
    public static class ScriptableObjectUtility
    {
        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static void CreateAsset<T>() where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();

            string path = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject)), "");
            }

            string assetPathAndName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

            UnityEditor.AssetDatabase.CreateAsset(asset, assetPathAndName);

            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            UnityEditor.EditorUtility.FocusProjectWindow();
            UnityEditor.Selection.activeObject = asset;
        }
    }

#endif
   

    public static string StringArrToLines(string[] str)
    {
        if (str == null || str.Length == 0) return "";
        string ret = "";
        foreach (string s in str) ret += s + "\n";
        return ret;
    }

    
    [System.Serializable]
    public class R_Range
    {
        public float min;
        public float max;
        public bool asInt;


        public float Value()
        {
            if (asInt)
            {
                return UnityEngine.Random.Range((int)min, (int)max);
            } else { 
                return UnityEngine.Random.Range(min, max);
            }
        }

        public R_Range(float _min, float _max)
        {
            asInt = false;
            min = _min;
            max = _max;
        }

        public R_Range(int _min, int _max)
        {
            asInt = true;
            min = _min;
            max = _max;
        }

        public override string ToString()
        {
           return "rMin:"+min+" rMax"+max;
        }
    }

    public static List<MonoBehaviour> GetObjectsInRangeAroundCenter(MonoBehaviour center, List<MonoBehaviour> group, float distance)
    {
        return group.Where(tr => (center.transform.position - tr.transform.position).magnitude <= distance).ToList();
    }

    public static MonoBehaviour GetRandomObjectInRangeAroundCenter(MonoBehaviour center, List<MonoBehaviour> group, float distance)
    {
        return GetRandomObject(GetObjectsInRangeAroundCenter(center, group, distance));
    }

    public static List<T> SpawnObjectsToTransform<T>(GameObject prefab, Transform target, int count) where T : Component
    {
        List<T> items = new List<T>();

        for(int i = 0; i<count; i++)
        {
            items.Add(GameObject.Instantiate(prefab).GetComponent<T>());
            items[i].transform.position = target.transform.position;
            items[i].transform.SetParent(target, false);
        }

        return items;
    }
    public static List<GameObject> InsantiateObjects(List<GameObject> source)
    {
        List<GameObject> objects = new List<GameObject>();

        source.ForEach(src => objects.Add(GameObject.Instantiate(src)));

        return objects;
    }



    


}
 

[System.Serializable]
public class FloatClamp
{
    public enum ClampType
    {
        max_float, min_float, value
    }

    public ClampType _ctype;

    /// <summary>
    /// USE GETVALUE INSTEAD!
    /// </summary>
    public float _value;

   
    public void SetValue(float v)
    {
        _value = v;
    }
    public float GetValue()
    {
        switch (_ctype)
        {
            case ClampType.max_float:
                return float.MaxValue;

            case ClampType.min_float:
                return float.MinValue;

            case ClampType.value:
                return _value;
        }

        return 0;
    }
}