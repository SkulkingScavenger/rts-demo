using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrototypeManager : MonoBehaviour{
	public List<ItemPrototype> prototypes = new List<ItemPrototype>();
	//public List<StructurePrototype> structurePrototypes = new List<StructurePrototype>();
	public static ItemPrototypeManager Instance { get; private set; }

	void Awake (){
		//ensure uniqueness
		if(Instance != null && Instance != this){
			Destroy(gameObject);
		}
		Instance = this;
		DontDestroyOnLoad(transform.gameObject);

		DefinePrototypes();
	}

	void DefinePrototypes(){
		ItemPrototype bumbel;


		bumbel = new ItemPrototype();
		bumbel.name = "Oil";
		bumbel.iconIndex = 0;
		bumbel.maxStackCount = 10;
		bumbel.craftable = true;
		bumbel.consumptionBehaviour = ConsumptionBehaviour.consumable;
		bumbel.Use = (Player p, Inventory container, int index) => {
			Debug.Log(p.backpack.maxSize);
		};
		prototypes.Add(bumbel);

		bumbel = new ItemPrototype();
		bumbel.name = "Wood";
		bumbel.iconIndex = 1;
		bumbel.maxStackCount = 10;
		bumbel.craftable = true;
		bumbel.consumptionBehaviour = ConsumptionBehaviour.consumable;
		bumbel.Use = (Player p, Inventory container, int index) => {
			Debug.Log("USED WOOD");
		};
		prototypes.Add(bumbel);

		// bumbel = new ItemPrototype();
		// bumbel.name = "Campfire Schematic";
		// bumbel.iconIndex = 6;
		// bumbel.maxStackCount = 10;
		// bumbel.craftable = true;
		// bumbel.consumptionBehaviour = ConsumptionBehaviour.consumable;
		// bumbel.Use = (Player p, Inventory container, int index) => {
		// 	UnderCursorDisplay underCursorDisplay = GameObject.FindGameObjectWithTag("UnderCursorDisplay").GetComponent<UnderCursorDisplay>();
		// 	underCursorDisplay.itemUsedFromInventory = container;
		// 	underCursorDisplay.itemUsedFromIndex = index;
		// 	underCursorDisplay.structure = structurePrototypes[0];
		// 	container.Get(index).inUse = true;
		// };
		// bumbel.Callback = (Player p, Inventory container, int index, bool successFlag) => {
		// 	UnderCursorDisplay cursorHandler = GameObject.FindGameObjectWithTag("UnderCursorDisplay").GetComponent<UnderCursorDisplay>();
		// 	container.Get(index).inUse = false;
		// 	cursorHandler.itemUsedFromInventory = null;
		// 	cursorHandler.itemUsedFromIndex = -1;
		// 	if(successFlag){
		// 		GameObject construction = Instantiate(Resources.Load<GameObject>("Prefabs/firepit"));
		// 		Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		// 		Vector3 mouseCoordinates = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
		// 		mouseCoordinates.x = Mathf.Round(mouseCoordinates.x*2)/2f;
		// 		mouseCoordinates.y = Mathf.Round(mouseCoordinates.y*2)/2f;
		// 		construction.transform.position = new Vector3(mouseCoordinates.x, mouseCoordinates.y,-0.5f);
		// 		cursorHandler.structure = null;

		// 		container.Get(index).Decrement(container,index);
		// 	}else{
		// 		cursorHandler.structure = null;
		// 	}

		// };
		// prototypes.Add(bumbel);




		// StructurePrototype structurePrototype;
		// structurePrototype = new StructurePrototype();
		// structurePrototype.sprite = Resources.Load<Sprite>("Sprites/firepit");
		// structurePrototype.size = 2;
		// structurePrototype.name = "firepit";
		// structurePrototypes.Add(structurePrototype);

	}

	public void useBuildingSchematic(Player p, Item item){
		Debug.Log("USED OIL");
	}

}

