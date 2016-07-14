using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public float MoveSpeed;

	// Update is called once per frame
	void Update () {
	
		transform.Translate(Vector3.up * MoveSpeed * Time.fixedDeltaTime);

	}
}
