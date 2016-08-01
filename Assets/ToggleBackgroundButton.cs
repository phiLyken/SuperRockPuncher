using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleBackgroundButton : MonoBehaviour {

	public Sprite Enabled_Image;
	public Sprite Disabled_Image; 

	public Image Image;

	public void Toggle(){
		BackGroundMusic.ToggleMusic();
		SetImage();
	}

	void SetImage(){
		Image.sprite = BackGroundMusic.GetEnabled() ? Enabled_Image : Disabled_Image;
	}

	void Start(){
		SetImage();
	}
}
