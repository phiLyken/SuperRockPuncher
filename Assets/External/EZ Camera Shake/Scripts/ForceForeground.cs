using UnityEngine;
using System.Collections;

public class ForceForeground : MonoBehaviour {

	void Start() {
		((ParticleSystem)this.GetComponent(typeof(ParticleSystem))).GetComponent<Renderer>().sortingLayerName = "Foreground";
	}
}
