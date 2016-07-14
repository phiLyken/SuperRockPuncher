using UnityEngine;
using System.Collections;
using EZCameraShake;

public class RockCameraShake : MonoBehaviour {

	public RockCameraShake Instance;
	public float DistanceMagnitudeStrength = 2;
	public float DistanceRoughnessStrength = 20;

	CameraShakeInstance shake;

	// Use this for initialization
	void Start () {
		shake = CameraShaker.Instance.StartShake(0, 0, 1);

	
	}

	public void PunchShake(){
		CameraShaker.Instance.ShakeOnce(0.3f,2f, 0,0.5f);
	}
	// Update is called once per frame
	void Update () {
		float distance = GetDistance();

	//	distance = Mathf.Max(0, 5 - distance);

	//	Debug.Log(distance);

		if(distance > 0){
			shake.Magnitude = (1 / distance ) * DistanceMagnitudeStrength;
			shake.Roughness = (1 / distance ) * DistanceRoughnessStrength;
		}
	}

	float GetDistance(){
		
		GameObject player =	GameObject.FindGameObjectWithTag("Player");
		if(player == null) return 0;

		GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

		return FindClosestDistance( obstacles, player.transform.position);

	}

	float FindClosestDistance(GameObject[] objects, Vector3 position){

		float dist = float.MaxValue;

		if(objects.Length > 0){
			for(int i = 0; i < objects.Length; i++){
				float current_dist = (objects[i].transform.position - position).magnitude;
				if(current_dist <  dist){
					dist = current_dist;
				}
			}
		}

		return dist;
	}
	void Awake(){
		Instance = this;
	}
}
