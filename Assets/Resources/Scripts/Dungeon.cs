using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour{

	public DungeonData dungeonData;

	public void Start(){

	}

	public void Init(){
		
	}

	public void Update(){
		dungeonData.Update(dungeonData);
	}
}