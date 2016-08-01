using UnityEngine;
using System.Collections;

public class BackGroundMusic : MonoBehaviour {

	public AudioSource bg_music;
	static BackGroundMusic instance;

	static string _music_setting_key ="_music_enabled";


	public static bool GetEnabled(){
		
		return PlayerPrefs.GetInt(_music_setting_key) == 1;
	}

	void Awake(){
		
		if(!PlayerPrefs.HasKey(_music_setting_key)){
			PlayerPrefs.SetInt(_music_setting_key,1);
		}

		if(instance != null){
			Destroy(this);

		} else {
			instance = this;
			DontDestroyOnLoad(this);
		}

		instance.SetBGMusic();


	}

	public static void ToggleMusic(){

		PlayerPrefs.SetInt(_music_setting_key, GetEnabled() ? 0 : 1);

		instance.SetBGMusic();
		
	}
	void SetBGMusic(){
		
		if(GetEnabled()){
			if(bg_music.isPlaying){
				//bg_music.Stop();
			}
			bg_music.Play();
			Debug.Log("Start music");
		} else {
			bg_music.Stop();
		}
	}


}
