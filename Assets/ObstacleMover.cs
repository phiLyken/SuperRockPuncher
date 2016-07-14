using UnityEngine;
using System.Collections;

public class ObstacleMover : MonoBehaviour {

	float current_speed;

	void Start(){
		SetSpeed(2f);
	}

	public void SetSpeed(float new_speed){
		current_speed = new_speed;
	}

	
	// Update is called once per frame
	void Update () {
		transform.Translate( Vector3.down * current_speed * Time.fixedDeltaTime);
	}
}
