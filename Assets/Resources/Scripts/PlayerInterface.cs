using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour{
	bool isSelecting = false;
	UnderCursorDisplay cursorHandler;
	public Player player;
	Vector2 selectionStart;
	Vector2 selectionEnd;
	GameObject canvas;
	GameObject text;
	GameObject selectionImage;
	GameObject gameManager;
	public List<Unit> selectedUnits = new List<Unit>();
	

	// Start is called before the first frame update
	void Start(){
		gameManager = GameObject.FindGameObjectWithTag("GameController");
		canvas = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Canvas"));
		text = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Text"));
		text.transform.SetParent(canvas.transform);
		text.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
		selectionImage = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/selectionImage"));
		selectionImage.transform.SetParent(canvas.transform);
		selectionImage.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);

		GameObject.FindGameObjectWithTag("Canvas").GetComponent<UserInterface>().Init(this);
		cursorHandler = GameObject.FindGameObjectWithTag("UnderCursorDisplay").GetComponent<UnderCursorDisplay>();
	}

	// Update is called once per frame
	void Update(){
		text.GetComponent<Text>().text = Input.mousePosition.x.ToString();
		if(isSelecting){
			selectionImage.GetComponent<Image>().color = new Color(0,1f,0,0.5f);
			float width = selectionStart.x - Input.mousePosition.x;
			float height = selectionStart.y - Input.mousePosition.y;
			bool flipX = width <= 0;
			bool flipY = height <= 0;
			float x = selectionStart.x - width/2;
			float y = selectionStart.y - height/2;
			RectTransform rt = selectionImage.GetComponent<RectTransform>();
			if(flipX){
				width *= -1; 
				x = selectionStart.x + width/2;
			}
			if(flipY){
				height *= -1; 
				y = selectionStart.y + height/2;
			}
			rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,width);
			rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,height);
			rt.position = new Vector3(x, y, 0);
			if(Input.GetMouseButtonUp(0)){
				isSelecting = false;
				SelectUnits(width, height, x, y);
			}else if(Input.GetAxis("Right Mouse") > 0){
				isSelecting = false;
			}
		}else{
			selectionImage.GetComponent<Image>().color = new Color(0,1f,0,0f);
			if(Input.GetAxis("Left Mouse") > 0){
				float x = Input.mousePosition.x;
				float y = Input.mousePosition.y;
				selectionStart = new Vector2(x,y);
				isSelecting = true;
			}
			if(Input.GetAxis("Right Mouse") > 0){
				//cancel stuff and order stuff
				Unit clickedUnit;
				clickedUnit = GetClickedUnit();
				if(clickedUnit == null){
					issueMoveCommand();
				}else{
					issueAttackCommand(clickedUnit);
				}
			}
		}


		setUnitVisibility();
	}

	Vector3 GetMouseCoordinates(){
		Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		return camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15.6f));
	}

	void SelectUnits(float width, float height, float x, float y){
		bool atLeastOneUnitInSelection = false;
		Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		List<Unit> allUnits = gameManager.GetComponent<GameManager>().unitList;
		GameObject go;
		Vector3 pos;
		Unit unit;
		float xMin, xMax, yMin, yMax;
		for(int i=0; i<player.ownedUnits.Count; i++){
			go = player.ownedUnits[i].gameObject;
			pos = go.transform.position;
			pos = camera.WorldToScreenPoint(pos);
			xMin = x - width/2;
			xMax = x + width/2;
			yMin = y - height/2;
			yMax = y + height/2;
			if(pos.x >= xMin && pos.x <= xMax && pos.y >= yMin && pos.y <= yMax){
				if(!atLeastOneUnitInSelection){
					DeselectAll();
					atLeastOneUnitInSelection = true;
				}
				unit = go.GetComponent<Unit>();
				selectedUnits.Add(unit);
				unit.Select();
			}
		}
	}

	Unit GetClickedUnit(){
		Unit clickedUnit = null;
		List<Unit> allUnits = gameManager.GetComponent<GameManager>().unitList;
		List<Unit> clickedUnits = new List<Unit>();
		Vector3 mouseCoords = GetMouseCoordinates();
		for (int i=0;i<allUnits.Count;i++){
			Unit unit = allUnits[i];
			Vector3 unitPos = new Vector3(unit.transform.position.x, 0f, unit.transform.position.z);
			Vector3 mousePos = new Vector3(mouseCoords.x, 0f, mouseCoords.z);
			float dist = Vector3.Distance(unitPos, mousePos);
			if(dist <= unit.collisionRadius){
				//clickedUnits.Add(unit);
				clickedUnit = unit;
				Debug.Log(clickedUnit.GetInstanceID());
				break;
			}
		}
		return clickedUnit;
	}

	void SelectOne(){

	}

	void DeselectAll(){
		for(int i=0;i<selectedUnits.Count;i++){
			selectedUnits[i].Deselect();
		}
		selectedUnits.Clear();
	}

	void issueAttackCommand(Unit target){
		for(int i=0;i<selectedUnits.Count;i++){
			AttackCommand command = new AttackCommand();
			command.target = target;
			selectedUnits[i].currentCommand = command;
		}
	}

	void issueMoveCommand(){
		Vector3 mouseCoords = GetMouseCoordinates();
		for(int i=0;i<selectedUnits.Count;i++){
			MoveCommand command = new MoveCommand();
			command.target = new Vector2(mouseCoords.x, mouseCoords.z);
			selectedUnits[i].currentCommand = command;
		}
	}

	void setUnitVisibility(){
		List<Unit> allUnits = gameManager.GetComponent<GameManager>().unitList;
		Unit unit;
		Unit observer;
		for(int i=0;i<allUnits.Count;i++){
			unit = allUnits[i];
			unit.visible = false;
		}

		//TODO: replace ownedunits with 
		for(int i=0;i<player.ownedUnits.Count;i++){
			unit = player.ownedUnits[i];
			unit.visible = true;
		}

		for(int i=0;i<allUnits.Count;i++){
			unit = allUnits[i];
			for(int j=0;j<player.ownedUnits.Count;j++){
				observer = player.ownedUnits[j];
				if(Vector3.Distance(unit.transform.position, observer.transform.position) <= observer.visionRadius){
					if (unitHasLineOfSight(observer, unit)){
						unit.visible = true;
					}
					break;
				}
			}
		}

	}

	bool unitHasLineOfSight (Unit observer, Unit observed){
		int layerMask = 1 << 8;
		layerMask = ~layerMask;
		RaycastHit hit;
		Vector3 direction = (observer.transform.position - observed.transform.position).normalized;
		if (Physics.Raycast(observer.transform.position, direction, observer.visionRadius, layerMask)){
			return true;
		}
		return false;
	}
}
