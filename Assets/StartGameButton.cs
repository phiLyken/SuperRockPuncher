using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour {

    // Use this for initialization
	void Start ()
	{
	    GetComponent<Button>().onClick.AddListener(StartGame);
	}

    private void StartGame()
    {
        SceneManager.LoadScene("scene");
    }
}
