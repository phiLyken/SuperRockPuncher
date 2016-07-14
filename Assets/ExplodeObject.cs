using UnityEngine;
using System.Collections;

public class ExplodeObject : MonoBehaviour {

	public GameObject ParticlePrefab;

	void OnDestroy(){
		Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
	}
}
