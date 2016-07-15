using UnityEngine;
using System.Collections;

public delegate void IntEventHandler(int _value);

public class PointCounter : MonoBehaviour {
	public float PointsPerSecond;
	public int PointsForSmash;
	public int PointsForTop;
	public IntEventHandler OnPointsUpdated;
	public static PointCounter Instance;



	float acc_points;

	int points_time;
	int obstacles_destroyed;
	int top_reached;


	public void ResetPoints(){

		obstacles_destroyed = 0;
		acc_points = 0;
		points_time = 0;
		top_reached =0;
	}

	public int CurrentScore;

	// Update is called once per frame
	
	void Update(){
		acc_points += Time.deltaTime * PointsPerSecond;
		int new_time_points = (int) Mathf.Round(acc_points);
		if(new_time_points != points_time){

			points_time = new_time_points;
			OnPointsUpdated(GetScore());
		}
	}

	public void ObstacleDestroyed(){
		obstacles_destroyed++;
		OnPointsUpdated(GetScore());
	}

	public void TopReached(){
		top_reached++;
		OnPointsUpdated(GetScore());
	}

	int GetScore(){
		return points_time + obstacles_destroyed * PointsForSmash + top_reached * PointsForTop;
	}

	void Awake(){
		Instance = this;
	}
}
