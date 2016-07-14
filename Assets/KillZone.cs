using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D col){

		if(col.gameObject.tag == "Player" || col.gameObject.tag == "Obstacle"){

			ExplodeObject explode = col.GetComponent<ExplodeObject>();
			if(explode != null) {
				explode.SpawnParticles();
			}
			Destroy(col.gameObject);
		}

	}
}
