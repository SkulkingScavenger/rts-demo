using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour{
	bool isSelecting = false;
	Vector2 selectionStart;
	Vector2 selectionEnd;
	GameObject canvas;
	GameObject text;
	GameObject selectionImage;
	GameObject gameManager;

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
			}
		}
	}

	Vector3 GetMouseCoordinates(){
		Camera camera = GameObject.FindGameObjectWithTag("camera").GetComponent<Camera>();
		return camera.ScreenToViewportPoint(Input.mousePosition);
	}

	void SelectUnits(float width, float height, float x, float y){
		Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		List<Unit> allUnits = gameManager.GetComponent<GameManager>().unitList;
		GameObject go;
		Vector3 pos;
		float xMin, xMax, yMin, yMax;
		for(int i=0; i<allUnits.Count; i++){
			go = allUnits[i].gameObject;
			pos = go.transform.position;
			pos = camera.WorldToScreenPoint(pos);
			xMin = x - width/2;
			xMax = x + width/2;
			yMin = y - height/2;
			yMax = y + height/2;
			if(pos.x >= xMin && pos.x <= xMax && pos.y >= yMin && pos.y <= yMax){
				go.GetComponent<Unit>().Select();
			}
		}
	}
}
