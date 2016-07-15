using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnDestroy : MonoBehaviour {

	void OnDestroy(){
		SceneManager.LoadScene("main_menu");
	}
}
