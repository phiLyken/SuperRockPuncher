using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Blink : MonoBehaviour {
	public Text text;

	public float frequence;

	void Awake(){
		StartCoroutine(BlinkSequence());
	}

	IEnumerator BlinkSequence(){

		while(true){

			yield return new WaitForSeconds(frequence);
			text.enabled = !text.enabled;
		}
		yield break;
	}
}
