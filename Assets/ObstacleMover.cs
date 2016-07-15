using UnityEngine;
using System.Collections;

public class ObstacleMover : MonoBehaviour {

	float current_speed;

	public void SetSpeed(float new_speed){
		current_speed = new_speed;
	}

	
	// Update is called once per frame
	void Update () {
		transform.Translate( Vector3.down * current_speed * Time.fixedDeltaTime);
	}
}
