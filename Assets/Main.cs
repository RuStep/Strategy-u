using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour {

	//Общие ресурсы
	public static int[] resources = new int[9];

	public static Transform[,] buildingsMap = new Transform[5,5];
	public Transform[] buildingsPrefabs;

	public static List<Transform> allBuildingsTr = new List<Transform>();
	public static List<Building> allBuildings = new List<Building>();

	float timer = 0f;
	// Use this for initialization
	void Start () {
		SpawnBuilding();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space)){
			SpawnBuilding();
			DoTurn();
		}
	}

	public static void DoTurn () {
		for(int i = 0; i < resources.Length; i++)
			resources[i] = 0;
		
		for(int i = 0; i < allBuildings.Count - 1; i++)
			for(int j = 0; j < allBuildings.Count - 1 - i; j++)
				if(allBuildings[j].priority > allBuildings[j + 1].priority)
			{	
				Building build = allBuildings[j + 1];
				allBuildings[j + 1] = allBuildings[j];
				allBuildings[j] = build;
			}
		
		for(int i = 0; i < allBuildings.Count; i++){
			allBuildings[i].Turn();
		}
		
		for(int i = 0; i < allBuildings.Count; i++)
			for(int j = 0; j < resources.Length; j++)
				resources[j] += allBuildings[i].resources[j];
	}

	public static void SpawnBuilding() {
		int x = 0;
		int y = 0;
		bool placed = false;
		do
		{
			x = Random.Range(0,5);
			y = Random.Range(0,5);
			if(buildingsMap[x,y] == null)
			{
				buildingsMap[x,y] = allBuildingsTr[Random.Range(0, allBuildingsTr.Count)];
				Instantiate(buildingsMap[x,y], new Vector3(-x,0,-y), Quaternion.Euler(270f, 0, 0));
				placed = true;
			}
		}while(!placed);

	}

}

