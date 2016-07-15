using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointUI : MonoBehaviour {

	Text _pointsTF;

	// Use this for initialization
	void Start () {
		_pointsTF = GetComponent<Text>();
		PointCounter.Instance.OnPointsUpdated+= UpdatePointTF;
	}
	
	void UpdatePointTF(int score){
		_pointsTF.text = score.ToString("D3");
	}
}
