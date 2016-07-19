using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PointUI : MonoBehaviour {

	Text _pointsTF;

	// Use this for initialization
	void Start () {
		_pointsTF = GetComponent<Text>();
		PointCounter.Instance.OnPointsUpdated+= UpdatePointTF;
	}
	
	void UpdatePointTF(int score){
		
		_pointsTF.text = score.ToString("D3");
		_pointsTF.transform.localScale = Vector3.one;
		_pointsTF.transform.DOPunchScale( new Vector3(1.1f,1.1f), 0.25f, 0, 1);
	}
}
