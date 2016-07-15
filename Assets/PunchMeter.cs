using UnityEngine;
using System.Collections;

public class PunchMeter : MonoBehaviour {

	public float CurrentFill;

	public float FillRate;

	public float PunchCost;

	public AnimationCurve FillToRange;

	public float Range;


	void Update(){
		CurrentFill = Mathf.Clamp01( CurrentFill + Time.deltaTime * FillRate);


	}
	public void Punch(){
		CurrentFill-=PunchCost;
	}

	public float GetRange(){
		return FillToRange.Evaluate( CurrentFill) * Range;
	}


}
