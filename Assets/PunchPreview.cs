using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PunchPreview : MonoBehaviour {

    public RectTransform PreviewIcon;
    private GestureRecognizer _recognizer;

    void Start()
    {
        _recognizer = GameObject.FindObjectOfType<Player>().GetComponent<GestureRecognizer>();
        PreviewIcon.gameObject.SetActive(false);
    }
    IEnumerator ShowSwipeIconPreview()
    {


        PreviewIcon.anchoredPosition = GetPreviewPosition( Input.mousePosition);
        PreviewIcon.gameObject.SetActive(true);

        while (Input.GetButton("Fire1"))
        {
            yield return null;
        }

      PreviewIcon.gameObject.SetActive(false);

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            StopAllCoroutines();
            StartCoroutine(ShowSwipeIconPreview());
        } else
        {
          //  PreviewIcon.localPosition = GetPreviewPosition( Input.mousePosition);
        }
    }
    Vector2 GetPreviewPosition(Vector3 InputPosition)
    {
        float dist = TouchKit.instance.ScreenPixelsPerCm * _recognizer.SwipeDistance;
        Debug.Log(dist);
        Vector3 _v = new Vector3(0, dist + PreviewIcon.sizeDelta.y / 2,0);

        Vector2 pos;
        Canvas myCanvas = GetComponent<Canvas>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, InputPosition + _v , myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);

        return pos;
    }
}
