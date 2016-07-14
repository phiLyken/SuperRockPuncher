using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReloadSceneOnDestroy : MonoBehaviour {

	void OnDestroy(){
		SceneManager.LoadScene(0);
	}
}
