using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour {

	public string name;

	//Приоритет опроса, ранжирование по независимости от других строений
	public int priority;

	//Кол-во ходов раз во сколько мы производим что-то
	//0 - производим при создании, единоразово
	public int turnsCount = 0;

	//Ресурсы заявленные
	public int[] resourcesClaimed = new int[9];

	//Ресурсы фактические
	public int[] resources = new int[9];

	/*
	//>0 Производим, <0 Потребляем
	public int people; //Человеческий ресурс
	public int faith; //Вера
	public int wood; //Древесина
	public int metal; //Метал
	public int iron; //Железо
	public int coal; //Уголь
	public int food; //Еда
	public int tree; //Дерево
	public int leather; //Кожа
	*/

	//Хилпоинты здания
	public int hp;

	//Радиус потребления 
	public float range;

	//Счётчики времени нахождения на поле
	public int age = 0;
	public int counter = 0;

	public void Turn(){
		//if(counter < turnsCount){																	//Если нам не пора производить
			for(int i = 0; i < resources.Length; i++){												//Смотрим все свои ресурсы
				if(resources[i] < Mathf.Abs(resourcesClaimed[i]) && resourcesClaimed[i] < 0){					//Если мы должны получать ресурс и нам его не хватает
					Debug.Log(this.name + " не хватает ресурса №" + i);
					Collider[] colliders = Physics.OverlapSphere(this.transform.position, range);	//Просматривем здания вокруг
					foreach(Collider collider in colliders){
						if(collider.tag == "building" && collider != this.transform.GetComponent<Collider>()){
							Building build = collider.GetComponent<Building>();						//Если здание должно вырабатывать этот ресурс и он у него есть, и он нам нужен, то забираем себе
							if(build.resourcesClaimed[i] > 0 && build.resources[i] > 0 && resources[i] < Mathf.Abs(resourcesClaimed[i]) && resourcesClaimed[i] < 0){			
								Debug.Log(this.name + " взял ресурс №" + i.ToString() + " у " + build.name);
								build.resources[i]--;
								this.resources[i]++;
							}
						}
					}
				}
			}
		//}else{																						//Если пора производить
		if(counter == turnsCount){	
			int[] resourcesSum = new int[9];														//Собираем промежуточный итог
			for(int i = 0; i < resources.Length; i++){
				resourcesSum[i] = resources[i] + resourcesClaimed[i];								//Складываем значения с заявленными, получаем результат
			}

			bool canProduce = true;
			for(int i = 0; i < resources.Length; i++){
				if(resourcesSum[i] < 0)
					canProduce = false;																//Если видим нехватку ресурсов, не разрешаем производить
			}

			if(canProduce){																			//Если всё в порядке, то проводим изменение ресурсов
				for(int i = 0; i < resources.Length; i++){
					if(resourcesClaimed[i] > 0)
						resources[i] = resourcesSum[i];
					else resources[i] = 0;
				}
			}
			else
				hp--;																				//Иначе уменьшаем кол-во хилпоинтов и продолжаем сбор материала

			if(hp <= 0)
				Destroy(this.gameObject);

			counter = -1;																			//Обнуляем счетчик
		}
		counter++;
		age++;
	}

	// Use this for initialization
	void Start () {
		Main.allBuildingsTr.Add(this.transform);
		Main.allBuildings.Add(this);
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.25f);
		Gizmos.DrawSphere(this.transform.position, range);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
