using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler {
	public int index = 0;
	public HeadsUpDisplay root = null;
	public Inventory targetInventory = null;
	public string stackCountText = "";
	GameObject imgObj;
	GameObject textObj;

	void Awake(){
		imgObj = transform.Find("Image").gameObject;
		textObj = transform.Find("Text").gameObject;
	}

	void Update(){
		if(targetInventory.Get(index) != null){
			ItemPrototype proto = targetInventory.Get(index).Prototype();
			imgObj.GetComponent<Image>().sprite = root.itemIcons[proto.iconIndex];
			if(targetInventory.Get(index).Prototype().consumptionBehaviour != ConsumptionBehaviour.nonconsumable || targetInventory.Get(index).stackCount > 1){
				textObj.GetComponent<Text>().text = targetInventory.Get(index).stackCount.ToString();
			}
		}else{
			imgObj.GetComponent<Image>().sprite = root.nullIcon;
			textObj.GetComponent<Text>().text = "";
		}
	}

	public void OnPointerClick(PointerEventData eventData){
		UnderCursorDisplay cursorHandler = root.underCursorDisplay.GetComponent<UnderCursorDisplay>();
		if (eventData.button == PointerEventData.InputButton.Left){
			if(targetInventory.Get(index) != null){
				if(cursorHandler.heldItem == null){
					cursorHandler.heldItem = targetInventory.Get(index);
					targetInventory.Add(null,index);
				}else{
					cursorHandler.heldItem = targetInventory.Add(cursorHandler.heldItem,index);
				}
			}else{
				if(cursorHandler.heldItem != null){
					cursorHandler.heldItem = targetInventory.Add(cursorHandler.heldItem,index);
				}
			}
		}else if (eventData.button == PointerEventData.InputButton.Right){
			if(cursorHandler.heldItem != null){
				if(root.isInventoryShown){
					root.ToggleInventory();
				}
			}else{
				if(root.isInventoryShown){
					root.ToggleInventory();
				}
				if(targetInventory.Get(index) != null){
					targetInventory.Get(index).UseFromInventory(root.player,targetInventory,index);
				}
			}
		}
	}
}