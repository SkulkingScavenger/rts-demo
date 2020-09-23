using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonData{
	public int id;
	public delegate void DelegateFunc(DungeonData self);
	public DelegateFunc Update;
	public int tilesetId;

	public DungeonData(){

	}	

}