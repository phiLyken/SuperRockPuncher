using UnityEngine;

public class PunchMeter : MonoBehaviour {

	public float CurrentFill;

	public float FillRate;

	public float BoothFillRate;

	public float PunchCost;

	public AnimationCurve FillToRange;

	public float Range;

	public static PunchMeter Instance;

    public Player Player;

	void Awake(){
		Instance = this;
	}

	void Update()
	{
	    var fillRate = Player.IsInBooth ? BoothFillRate : FillRate;

        CurrentFill = Mathf.Clamp01( CurrentFill + Time.deltaTime * fillRate);
	}

	public float GetCurrentFill(){
		return CurrentFill;
	}

	public void Punch(){
		CurrentFill = Mathf.Clamp01(CurrentFill - PunchCost);
	}

	public float GetRange(){
		return FillToRange.Evaluate( CurrentFill) * Range;
	}


}
