using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour{
	public List<UnitData> unitData = new List<UnitData>();
	public static bool isInitialized = false;

	public static UnitManager Instance { get; private set; }

	public void Awake(){
		if(Instance != null && Instance != this){
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		RegisterUnits();
		isInitialized = true;
	}

	private void RegisterUnits(){
		UnitData unitDatum = new UnitData();
		unitDatum.id = 0;
		unitDatum.speed = 5f;
		unitDatum.maxHitpoints = 50;
		unitDatum.visionRadius = 5f;
		unitDatum.modelFilePath = "blarg";
		unitData.Add(unitDatum);
	}
}
