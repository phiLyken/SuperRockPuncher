using UnityEngine;
using System.Collections;

public class PunchMeterUI : MonoBehaviour {

	public UI_Bar bar;

	void Update(){
		bar.SetProgress( PunchMeter.Instance.GetCurrentFill());
	}
}
