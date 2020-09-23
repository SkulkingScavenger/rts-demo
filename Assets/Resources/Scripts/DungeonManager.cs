using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {
	public List<DungeonData> dungeonPrototypes = new List<DungeonData>();
	public void Start(){
		DungeonData dd;
		
		//Create Dungeon 1
		dd = new DungeonData();
		dd.Update = (DungeonData self) => {
			Debug.Log(self.tilesetId);
		};
		dungeonPrototypes.Add(dd);

		//Create Dungeon 2
		dd = new DungeonData();
		dd.Update = (DungeonData self) => {
			//run triggers
		};
		dungeonPrototypes.Add(dd);

	}
}