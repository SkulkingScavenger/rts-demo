using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnderCursorDisplay : MonoBehaviour {
	public Inventory heldItemSlot = new Inventory(1);
	public Inventory itemUsedFromInventory = null;
	public int itemUsedFromIndex = -1;
	public Item heldItem { get{ return heldItemSlot[0]; } set{ heldItemSlot.Add(value,0); } }
	Image heldItemImage;
	Sprite[] itemIcons;
	Sprite nullIcon;
	GameObject root;
	Camera cam;
	public bool overUI = false;

	// public StructurePrototype structure = null;
	// StructurePrototype structurePrevious = null;
	// GameObject constructionPreview = null;
	// float constructionPreviewCellWidth = (1/128f)*64.0f;

	void Awake(){
		itemIcons = Resources.LoadAll<Sprite>("Sprites/InventoryIcons");
		heldItemImage = transform.Find("HeldItemImage").gameObject.GetComponent<Image>();
		nullIcon = Resources.Load<Sprite>("Sprites/ui_null_icon");
		root = GameObject.FindGameObjectWithTag("Canvas");
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update(){
		transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		transform.SetAsLastSibling();
		if(heldItem != null){
			heldItemImage.sprite = itemIcons[heldItem.Prototype().iconIndex];
			transform.Find("HeldItemImage").localPosition = new Vector3(0,0,0);
		}else{
			heldItemImage.sprite = nullIcon;
			transform.Find("HeldItemImage").localPosition = new Vector3(999,999,0);
		}
		overUI = CheckUICollision();

		// if(structure != structurePrevious){
		// 	DisplayConstructionPreview();
		// }
		// structurePrevious = structure;
		// if(constructionPreview != null){
		// 	Vector3 mouseCoordinates = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
		// 	mouseCoordinates.x = Mathf.Round(mouseCoordinates.x*2)/2f;
		// 	mouseCoordinates.y = Mathf.Round(mouseCoordinates.y*2)/2f;
		// 	constructionPreview.transform.position = new Vector3(mouseCoordinates.x, mouseCoordinates.y,-8);
		// }
	}

	bool CheckUICollision(){
		GraphicRaycaster gr = root.GetComponent<GraphicRaycaster>();
		PointerEventData ped = new PointerEventData(null);
		ped.position = Input.mousePosition;
		List<RaycastResult> results = new List<RaycastResult>();
		gr.Raycast(ped, results);

		return results.Count > 0;
	}

	// public void DisplayConstructionPreview(){
	// 	if(constructionPreview != null){
	// 		Destroy(constructionPreview);
	// 		constructionPreview = null;
	// 	}
	// 	if(structure != null){
	// 		constructionPreview = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ConstructionPreview"));
	// 		constructionPreview.transform.Find("BuildingSprite").gameObject.GetComponent<SpriteRenderer>().sprite = structure.sprite;
	// 		GameObject gridCell = constructionPreview.transform.Find("GridCell").gameObject;
	// 		GameObject bumbel;
	// 		for(int i=0;i<structure.size;i++){
	// 			for(int j=0;j<structure.size;j++){
	// 				bumbel = Instantiate(gridCell);
	// 				bumbel.transform.SetParent(constructionPreview.transform);
	// 				float w = (structure.size-1)*-0.5f;
	// 				bumbel.transform.localPosition = new Vector3((w + i)*constructionPreviewCellWidth, (w + j)*constructionPreviewCellWidth,0f);
	// 			}
	// 		}
	// 		Destroy(gridCell);
	// 	}
	// }
}