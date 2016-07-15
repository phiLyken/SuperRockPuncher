using UnityEngine;
using System.Collections;

public class TopBorder : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){

		if(col.tag == "Player"){
			GameScene.Instance.TryEnhancedScroll();

		}
	}
}
