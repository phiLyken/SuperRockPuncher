using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LaneGenerator : MonoBehaviour {

	public float ElementHeight;

	public GameObject LeftAnchor;
	public GameObject RightAnchor;
	public GameObject BackGroundAnchor;

	public GameObject WallPrefab;
	public GameObject BoothPrefab;
	public GameObject BackGroundPrefab;

	
	float additionalChanceForBooth = 0.075f;

	List<GameObject> LeftElements = new List<GameObject>();
	List<GameObject> RightElements = new List<GameObject>();
	List<GameObject> BackGroundElements = new List<GameObject>();

	public	float MinDistance;

	void Awake(){
		LeftElements.Add(LeftAnchor);
		RightElements.Add(RightAnchor);
		BackGroundElements.Add(BackGroundAnchor);
	}
	void Update(){
		if(MinDistance > GetCurrentLaneLength()){
			GenerateChunk();
		}
	}
	 
	void GenerateChunk(){
		
		while(MinDistance > GetCurrentLaneLength()){
			GameObject prefab_left = Random.value < GetChance(LeftElements) ? BoothPrefab : WallPrefab;
			GameObject prefab_right = Random.value < GetChance(RightElements) ? BoothPrefab : WallPrefab;

			Vector3 position_left = LeftElements[LeftElements.Count-1].transform.position + Vector3.up * ElementHeight;
			Vector3 position_right = RightElements[RightElements.Count-1].transform.position + Vector3.up *  ElementHeight;
			Vector3 position_background = BackGroundElements[BackGroundElements.Count-1].transform.position + Vector3.up *  ElementHeight;

			GameObject new_element_left = GameObject.Instantiate( prefab_left,position_left , Quaternion.identity) as GameObject;
			GameObject new_element_right = GameObject.Instantiate( prefab_right, position_right, Quaternion.identity) as GameObject;
			GameObject new_element_bg = GameObject.Instantiate( BackGroundPrefab, position_background, Quaternion.identity) as GameObject;

			new_element_right.GetComponent<SpriteRenderer>().flipX = true;
			LeftElements.Add(new_element_left);
			RightElements.Add(new_element_right);
			BackGroundElements.Add(new_element_bg);
		}

	}



	float GetCurrentLaneLength(){

		float distance = 0;

		List<GameObject> wall_elements = GetElementsAhead(RightElements);
	
		if(wall_elements.Count > 0){
			distance = wall_elements.Sum( element => element.transform.localScale.y);
		}
	
		return distance;
	}

	List<GameObject> GetElementsAhead(List<GameObject> _wallElements){
		List<GameObject> objects = new List<GameObject>();

		foreach ( GameObject go in _wallElements){
			if(go.transform.position.y >= transform.position.y){
				objects.Add(go);
			}
		}

		return objects;
	}

	float GetChance(List<GameObject> elements){
		
		float start_chance = -0.15f;

		List<GameObject> elements_reversed = new List<GameObject>( elements);
		elements_reversed.Reverse();

		foreach(GameObject go in elements_reversed){
			
			start_chance += additionalChanceForBooth;

			if(go.tag == "Booth"){
				return start_chance;
			}
		}

		return 1;
	}
}
