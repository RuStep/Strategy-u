using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GetInfo : MonoBehaviour {

	public Text[] resourcesText = new Text[9];
	//public Transform[] buildings = new Transform[4];
	static List<Transform> buildings = new List<Transform>();

	public Transform circlePrefab;
	Transform circle;

	int id = -1;
	bool showCircle = false;
	bool showedCircle = false;
	// Use this for initialization
	void Start () {
		buildings = Main.allBuildingsTr;
	}

	void Update () {
		/*Debug.LogWarning(showCircle);
		if(showCircle && !showedCircle)
		{
			circle = Instantiate(circlePrefab, buildings[id].position, Quaternion.Euler(new Vector3(90f, 0, 0))) as Transform;
			circle.GetComponent<Circle>().radius = buildings[id].GetComponent<Building>().range;
			//circle.GetComponent<Circle>().yradius = buildings[i].GetComponent<building>().range;
			circle.GetComponent<Circle>().CreatePoints();
			showedCircle = true;
		}else{
			if(circle != null)
				Destroy(circle.gameObject);
		}*/
	}
	
	// Update is called once per frame
	void OnMouseOver () {

		/*if(circle == null && showCircle)
		{

		}*/

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100f))
		{
			for(int i = 0; i < buildings.Count; i++)
			{
				if(hit.point.x > buildings[i].position.x-0.5f && hit.point.x < buildings[i].position.x+0.5f && hit.point.z > buildings[i].position.z-0.5f && hit.point.z < buildings[i].position.z+0.5f)
				{
					for(int j = 0; j < 9; j++)
						resourcesText[j].text = buildings[i].GetComponent<Building>().resources[j].ToString();

					if(Input.GetMouseButtonUp(0)){
						Debug.Log(buildings[i].GetComponent<Building>().name);
						//buildings[i].position = new Vector3(hit.point.x, 0.5f, hit.point.z);
						if(id == -1)
							id = i;
						else{
							Vector3 pos = buildings[i].position;
							Transform build = buildings[i];
							
							buildings[i].position = buildings[id].position; 
							buildings[id].position = pos;
							buildings[i] = buildings[id];
							buildings[id] = build;
							id = -1;
						}

						//showCircle = true;
						Main.DoTurn();
					}
				}
			}
		} //else showCircle = false;
	}



}
