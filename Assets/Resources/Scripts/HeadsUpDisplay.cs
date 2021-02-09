using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsUpDisplay : MonoBehaviour{
	public bool isInventoryShown = false;
	float inputPrevious = 0;
	float input = 0;
	int inventoryGridWidth = 6;
	int slotWidth = 60; 
	GameObject inventoryOverlay;

	public GameObject underCursorDisplay;
	public Camera mainCamera;

	public Player player = null;
	//public Inventory externalInventory = null;

	public Sprite[] itemIcons;
	public Sprite nullIcon;

	Vector3 quickSlotPosition;
	Vector3 packPosition;
	Vector3 weaponSlotPosition;
	Vector3 lightSlotPosition;
	Vector3 trinketSlotPosition;
	Vector3 externalInventoryPanelPosition;

	void Awake(){
		itemIcons = Resources.LoadAll<Sprite>("Sprites/InventoryIcons");
		nullIcon = Resources.Load<Sprite>("Sprites/ui_null_icon");
		underCursorDisplay = transform.Find("UnderCursorDisplay").gameObject;

		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		float leftBound = -1*mainCamera.pixelWidth/2;
		quickSlotPosition = new Vector3(leftBound+32,-176, 0);
		packPosition = new Vector3(leftBound+32,-96, 0);
		weaponSlotPosition = new Vector3(leftBound+32,128, 0);
		lightSlotPosition = new Vector3(leftBound+32+60,128, 0);
		trinketSlotPosition = new Vector3(leftBound+32,48, 0);
		externalInventoryPanelPosition = new Vector3(32,-96, 0);

	}

	// Update is called once per frame
	void Update(){
		inputPrevious = input;
		input = Input.GetAxis("Inventory");
		if (input != 0 && inputPrevious == 0){
			ToggleInventory();
		}
	}

	public void Init(Player p){
		player = p;

		GameObject quickSlotBar = Instantiate(Resources.Load<GameObject>("Prefabs/UI/InventoryPanel"));
		quickSlotBar.transform.SetParent(transform,false);
		quickSlotBar.transform.localPosition = quickSlotPosition;
		for(int i = 0; i < player.quickSlot.maxSize; i++){
			GameObject bumbel = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemSlot"));
			bumbel.transform.SetParent(quickSlotBar.transform,false);
			bumbel.transform.localPosition = new Vector3(i*slotWidth, 0, 0);
			bumbel.GetComponent<ItemSlot>().root = this;
			bumbel.GetComponent<ItemSlot>().targetInventory = player.quickSlot;
			bumbel.GetComponent<ItemSlot>().index = i;
		}
	}

	public void ToggleInventory(){
		if (isInventoryShown){
			Destroy(inventoryOverlay);
		}else{
			inventoryOverlay = Instantiate(Resources.Load<GameObject>("Prefabs/UI/InventoryPanel"));
			inventoryOverlay.transform.SetParent(transform,false);
			inventoryOverlay.transform.localPosition = new Vector3(0,0,0);

			//Create Pack Panel
			GameObject packMenu = Instantiate(Resources.Load<GameObject>("Prefabs/UI/InventoryPanel"));
			packMenu.transform.SetParent(inventoryOverlay.transform,false);
			packMenu.transform.localPosition = packPosition;
			for(int i = 0; i < player.backpack.maxSize;i++){
				GameObject bumbel = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemSlot"));
				bumbel.transform.SetParent(packMenu.transform,false);
				bumbel.transform.localPosition = new Vector3((i%inventoryGridWidth)*slotWidth, (i/inventoryGridWidth)*slotWidth, 0);
				bumbel.GetComponent<ItemSlot>().root = this;
				bumbel.GetComponent<ItemSlot>().targetInventory = player.backpack;
				bumbel.GetComponent<ItemSlot>().index = i;
			}

			//Create Weapon Slot
			GameObject weaponSlot = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemSlot"));
			weaponSlot.GetComponent<Image>().sprite = Instantiate(Resources.Load<Sprite>("Sprites/weapon_slot"));
			weaponSlot.transform.SetParent(inventoryOverlay.transform,false);
			weaponSlot.transform.localPosition = weaponSlotPosition;
			weaponSlot.GetComponent<ItemSlot>().root = this;
			weaponSlot.GetComponent<ItemSlot>().targetInventory = player.weaponSlot;

			//Create Light Slot
			GameObject lightSlot = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemSlot"));
			lightSlot.GetComponent<Image>().sprite = Instantiate(Resources.Load<Sprite>("Sprites/light_slot"));
			lightSlot.transform.SetParent(inventoryOverlay.transform,false);
			lightSlot.transform.localPosition = lightSlotPosition;
			lightSlot.GetComponent<ItemSlot>().root = this;
			lightSlot.GetComponent<ItemSlot>().targetInventory = player.lightSlot;

			//Create Trinket Slots
			GameObject trinketMenu = Instantiate(Resources.Load<GameObject>("Prefabs/UI/InventoryPanel"));
			trinketMenu.transform.SetParent(inventoryOverlay.transform,false);
			trinketMenu.transform.localPosition = trinketSlotPosition;
			for(int i=0;i<player.trinketSlot.maxSize;i++){
				GameObject bumbel = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemSlot"));
				bumbel.transform.SetParent(trinketMenu.transform,false);
				bumbel.transform.localPosition = new Vector3((i%inventoryGridWidth)*slotWidth, (i/inventoryGridWidth)*slotWidth, 0);
				bumbel.GetComponent<ItemSlot>().root = this;
				bumbel.GetComponent<ItemSlot>().targetInventory = player.trinketSlot;
				bumbel.GetComponent<ItemSlot>().index = i;
			}

			if(externalInventory != null){
				GameObject externalInventoryPanel = Instantiate(Resources.Load<GameObject>("Prefabs/UI/InventoryPanel"));
				externalInventoryPanel.transform.SetParent(inventoryOverlay.transform,false);
				externalInventoryPanel.transform.localPosition = externalInventoryPanelPosition;
				for(int i = 0; i < externalInventory.maxSize; i++){
					GameObject bumbel = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemSlot"));
					bumbel.transform.SetParent(externalInventoryPanel.transform,false);
					bumbel.transform.localPosition = new Vector3((i%inventoryGridWidth)*slotWidth, (i/inventoryGridWidth)*slotWidth, 0);
				}
			}
		}
		isInventoryShown = !isInventoryShown;
	}
}
