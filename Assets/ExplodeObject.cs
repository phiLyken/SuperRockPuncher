using UnityEngine;
using System.Collections;
using EZCameraShake;


public class ExplodeObject : MonoBehaviour {

 
	public GameObject ParticlePrefab;

	public void SpawnParticles(){
		Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
		CameraShaker.Instance.ShakeOnce(8,8,0,0.75f);
       
	}
}
