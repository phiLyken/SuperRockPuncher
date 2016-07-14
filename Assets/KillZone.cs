using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D col){

		if(col.gameObject.tag == "Player" || col.gameObject.tag == "Obstacle"){
			Destroy(col.gameObject);
		}

	}
}
