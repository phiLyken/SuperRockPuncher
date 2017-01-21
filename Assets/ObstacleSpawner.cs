using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

   
	public float min_time;
	public float random_range;

	public GameObject ObstaclePrefab;
	public Transform SpawnAnchor;

	public float BaseSpeed;
	public float RandomSpeed;

	float next_spawn_time = 3;

    public bool SpawnEnabled;

    void OnEnable()
    {

        SpawnEnabled = (MissionSystem.HasCompletedGlobal("punch"));
    }
	void Update(){
		if(SpawnEnabled && Time.time > next_spawn_time){
			SpawnObstacle();
		}
	}

	public void SpawnObstacle(){

		next_spawn_time = Time.time + min_time + Random.Range(0, random_range);
		GameObject new_obstacle = (Instantiate(ObstaclePrefab, SpawnAnchor.position, Quaternion.identity) as GameObject);
		new_obstacle.GetComponent<ObstacleMover>().SetSpeed( BaseSpeed + Random.Range(0, RandomSpeed  ));
	}
}
