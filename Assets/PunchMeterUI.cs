using UnityEngine;
using System.Collections;

public class PunchMeterUI : MonoBehaviour {

	public UI_Bar bar;
    
    void Start()
    {
        bar.gameObject.SetActive(MissionSystem.HasCompletedGlobal("move_out"));
    }

 
	void Update(){
        
		bar.SetProgress( PunchMeter.Instance.GetCurrentFill());
	}
}
