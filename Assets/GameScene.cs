using UnityEngine;
using System.Collections;

public class GameScene : MonoBehaviour {

	public static GameScene Instance;
	public float BaseMoveSpeed;
	public float EnhancedScrollSpeed;
	public float EnhancedScrollDistance;

	public bool GameEnded;

    public float MaxSpeed;
    public float SpeedIncrease;

	float GetCurrentMoveSpeed()
    {
        return _enhancedScroll ? EnhancedScrollSpeed : BaseMoveSpeed;
    }
	bool _enhancedScroll;

	// Update is called once per frame
	void FixedUpdate () {
        BaseMoveSpeed = Mathf.Min((BaseMoveSpeed + SpeedIncrease * Time.fixedDeltaTime), MaxSpeed);
		transform.Translate(Vector3.up * GetCurrentMoveSpeed() * Time.fixedDeltaTime);

	}
	void Start(){
		GameEnded = false;

	}


	public void TryEnhancedScroll(){
		if(!_enhancedScroll){
			PointCounter.Instance.TopReached();
			StartCoroutine(EnhancedScroll());
		}
	}
	void Awake(){
		Instance = this;
	}


	protected IEnumerator EnhancedScroll(){
		_enhancedScroll = true;
		Vector3 startScroll = transform.position;

		while( ( startScroll - transform.position).magnitude < EnhancedScrollDistance){
//			Debug.Log(( startScroll - transform.position).magnitude );
			yield return null;
		}
		
		_enhancedScroll = false;
	}
}
