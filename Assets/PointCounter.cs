using System;
using UnityEngine;

public class PointCounter : MonoBehaviour {
	public float PointsPerSecond;
	public int PointsForSmash;
	public int PointsForTop;
	public Action<int> OnPointsUpdated;
	public static PointCounter Instance;

	private const string HIGHSCORE_KEY = "Highscore";
	float acc_points;
	int points_time;
	int obstacles_destroyed;
	int top_reached;

	public void ResetPoints()
	{
		obstacles_destroyed = 0;
		acc_points = 0;
		points_time = 0;
		top_reached = 0;
	}

	public void ObstacleDestroyed()
	{
		obstacles_destroyed++;
		if (OnPointsUpdated != null) OnPointsUpdated(GetScore());
	}

	public void TopReached()
	{
		top_reached++;
		if (OnPointsUpdated != null) OnPointsUpdated(GetScore());
	}

	public int GetScore()
	{
		return points_time + obstacles_destroyed * PointsForSmash + top_reached * PointsForTop;
	}

	void Awake(){
		Instance = this;
	}

	void OnEnable()
	{
		GameScene.Instance.OnGameEnded += CheckAndPersistHighscore;
	}

	void OnDisable()
	{
		GameScene.Instance.OnGameEnded -= CheckAndPersistHighscore;
	}

	void Update()
	{
		if(GameScene.Instance.GameEnded)
		{
			return;
		}
		acc_points += Time.deltaTime * PointsPerSecond;
		int new_time_points = (int) Mathf.Round(acc_points);
		if(new_time_points != points_time)
		{
			points_time = new_time_points;
			if (OnPointsUpdated != null) OnPointsUpdated(GetScore());
		}
	}

	private void CheckAndPersistHighscore()
	{
		var score = GetScore();
		if (score > GetHighscore())
		{
			PlayerPrefs.SetInt(HIGHSCORE_KEY, score);
		}
	}

	public int GetHighscore()
	{
		return PlayerPrefs.HasKey(HIGHSCORE_KEY) ? PlayerPrefs.GetInt(HIGHSCORE_KEY) : 0;
	}
}
