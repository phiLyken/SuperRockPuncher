using UnityEngine;
using System.Collections;

public class GameScene : MonoBehaviour {

	public static GameScene Instance;
	public float BaseMoveSpeed;
	public float EnhancedScrollSpeed;
	public float EnhancedScrollDistance;

	float _currentMoveSpeed;
	bool _enhancedScroll;

	// Update is called once per frame
	void Update () {
	
		transform.Translate(Vector3.up * _currentMoveSpeed * Time.fixedDeltaTime);

	}
	void Start(){
		StartScroll();
	}
	void StartScroll(){
		_currentMoveSpeed = BaseMoveSpeed;
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
		_currentMoveSpeed = EnhancedScrollSpeed;
		while( ( startScroll - transform.position).magnitude < EnhancedScrollDistance){
//			Debug.Log(( startScroll - transform.position).magnitude );
			yield return null;
		}
		_currentMoveSpeed = BaseMoveSpeed;
		_enhancedScroll = false;
	}
}
