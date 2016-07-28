using System;
using UnityEngine;
using System.Collections;

public class GameScene : MonoBehaviour
{
	public static GameScene Instance;
	public float BaseMoveSpeed;
	public float EnhancedScrollSpeed;
	public float EnhancedScrollDistance;
	public float MaxSpeed;
	public float SpeedIncrease;

	bool _enhancedScroll;
	private bool _gameEnded;
	public Action OnGameEnded;
	public bool GameEnded
	{
		get { return _gameEnded; }
		set
		{
			if (value && OnGameEnded != null)
			{
				OnGameEnded();
			}
			_gameEnded = value;
		}
	}

	void FixedUpdate () {
        BaseMoveSpeed = Mathf.Min((BaseMoveSpeed + SpeedIncrease * Time.fixedDeltaTime), MaxSpeed);
		transform.Translate(Vector3.up * GetCurrentMoveSpeed() * Time.fixedDeltaTime);

	}

	void Start(){
		GameEnded = false;
	}

	float GetCurrentMoveSpeed()
	{
		return _enhancedScroll ? EnhancedScrollSpeed : BaseMoveSpeed;
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
