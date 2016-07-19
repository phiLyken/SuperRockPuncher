using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour {



	public RectTransform Content;

	public static UI_GameOver Instance;

	void Awake(){
		Instance = this;

		Content.gameObject.SetActive(false);
	}

	public void ShowScreen(){
		Content.gameObject.SetActive(true);

		StartCoroutine(ShowSequence() );
	}
	IEnumerator ShowSequence(){
		yield return new WaitForSeconds(0.3f);
		Content.DOMoveY( transform.position.y, 0.3f);
		yield return new WaitForSeconds(0.3f);

	}

	public void Reload(){
		SceneManager.LoadScene("main_menu");
	}
}
