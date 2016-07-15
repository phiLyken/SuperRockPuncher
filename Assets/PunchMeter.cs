using UnityEngine;
using System.Collections;

public class PunchMeter : MonoBehaviour {

	public float CurrentFill;

	public float FillRate;

	public float PunchCost;

	public AnimationCurve FillToRange;

	public float Range;

	public static PunchMeter Instance;

	void Awake(){
		Instance = this;
	}
	void Update(){
		CurrentFill = Mathf.Clamp01( CurrentFill + Time.deltaTime * FillRate);
	}

	public float GetCurrentFill(){
		return CurrentFill;
	}
	public void Punch(){
		CurrentFill-=PunchCost;
	}

	public float GetRange(){
		return FillToRange.Evaluate( CurrentFill) * Range;
	}


}
