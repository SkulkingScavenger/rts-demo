using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumptionBehaviour {consumable, nonconsumable, reloadable, charged};

public class Inventory {
	public int maxSize;
	public List<Item> contents = new List<Item>();

	public Inventory(int max){
		maxSize = max;
		for(int i=0;i<maxSize;i++){
			contents.Add(null);
		}
	}

	public Item Add(Item item){
		Item output = null;
		List<int> stackIndices = FindById(item.itemId);
		for(int i=0;i<stackIndices.Count; i++){
			Get(i).stackCount += item.stackCount;
			if(Get(i).stackCount > item.Prototype().maxStackCount){
				item.stackCount = Get(i).stackCount - item.Prototype().maxStackCount;
				Get(i).stackCount = item.Prototype().maxStackCount;
			}else{
				item.stackCount = 0;
			}
			if(item.stackCount == 0){
				break;
			}
		}
		if(item.stackCount != 0){
			output = item;
			for(int i=0;i<maxSize;i++){
				if(Get(i) == null){
					contents[i] = item;
					output = null;
					break;
				}
			}
		}
		return output;
	}

	public Item Add(Item item, int index){
		Item output;
		if(item == null){
			output = contents[index];
			contents[index] = null;
		}else{
			if(contents[index] != null){
				if(contents[index].itemId == item.itemId){
					contents[index].stackCount += item.stackCount;
					if(contents[index].stackCount > contents[index].Prototype().maxStackCount){
						output = new Item();
						output.itemId = item.itemId;
						output.stackCount = contents[index].stackCount - contents[index].Prototype().maxStackCount;
						contents[index].stackCount = contents[index].Prototype().maxStackCount;
					}else{
						output = null;
					}
				}else{
					output = contents[index];
					contents[index] = item;
				}
			}else{
				contents[index] = item;
				output = null;
			}
		}
		
		return output;
	}

	public Item Get(int index){
		return contents[index];
	}


	public Item this[int index]{
		get{
			return Get(index);
		}
		set{
			Add(value,index);
		}
	}



	List<int> FindById(int id){
		List<int> output = new List<int>();
		for(int i=0;i<maxSize;i++){
			if(Get(i) != null && Get(i).itemId == id){
				output.Add(i);
			}
		}
		return output;
	}
}

public class Item {
	public int itemId = 0;
	public int stackCount = 1;
	public int ammunitionId = 0;
	public bool inUse = false;


	public ItemPrototype Prototype(){
		return ItemPrototypeManager.Instance.prototypes[itemId];
	}

	// public void UseOnObject(Player p, Item item){
	// 	Prototype().UseItem(p,item);
	// }

	public void UseFromInventory(Player p, Inventory container, int index){
		Prototype().Use(p,container,index);
	}

	public void Callback(Player p, Inventory inventory, int index, bool success){
		Prototype().Callback(p,inventory,index,success);
	}

	public void Decrement(Inventory container, int index){
		switch(Prototype().consumptionBehaviour){
		case ConsumptionBehaviour.consumable:
			stackCount--;
			if(stackCount == 0){
				container.Add(null, index);
			}
			break;
		case ConsumptionBehaviour.nonconsumable:
			break;
		case ConsumptionBehaviour.reloadable:
			stackCount--;
			break;
		case ConsumptionBehaviour.charged:
			stackCount--;
			if(stackCount == 0){
				container.Add(null, index);
			}
			break;
		}
	}
}

public class ItemPrototype {
	public int maxStackCount = 1;
	public int iconIndex = 0;
	public string name = "";
	public bool craftable = false;
	public ConsumptionBehaviour consumptionBehaviour = ConsumptionBehaviour.nonconsumable;

	public delegate void DelegateFunc(Player p, Inventory inventory, int index);
	public delegate void DelegateCallbackFunc(Player p, Inventory inventory, int index, bool success);

	public DelegateFunc Use;
	public DelegateCallbackFunc Callback = null;
}