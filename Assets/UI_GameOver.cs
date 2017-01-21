using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour {
	public RectTransform Content;
	public Text ScoreLabel;
	public Text HighscoreLabel;

	public static UI_GameOver Instance;

	void Awake(){
		Instance = this;

		Content.gameObject.SetActive(false);
	}

	public void ShowScreen(){
		Content.gameObject.SetActive(true);

		ScoreLabel.text = PointCounter.Instance.GetScore().ToString();
		HighscoreLabel.text = PointCounter.Instance.GetHighscore().ToString();

		StartCoroutine(ShowSequence());
	}
	IEnumerator ShowSequence(){
		yield return new WaitForSeconds(.5f);
		Content.DOMoveY( transform.position.y, 0.3f);
		yield return new WaitForSeconds(0.3f);

	}

	public void Reload(){
		SceneManager.LoadScene("main_menu");
	}
}
