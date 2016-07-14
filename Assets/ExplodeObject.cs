using UnityEngine;
using System.Collections;

public class ExplodeObject : MonoBehaviour {

	public GameObject ParticlePrefab;

	public void SpawnParticles(){
		Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
	}
}
