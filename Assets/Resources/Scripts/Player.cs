using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
	//bool[] isAllied = new bool[16];
	//bool[] isSharingVision = new bool[16];
	bool[] isSharedVisionEnabled = new bool[16];
	public List<Unit> ownedUnits = new List<Unit>();

	//Inventory Data
	public Inventory backpack = new Inventory(12);
	public Inventory quickSlot = new Inventory(6);
	public Inventory weaponSlot = new Inventory(1);
	public Inventory lightSlot = new Inventory(1);
	public Inventory trinketSlot = new Inventory(4);
}

