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

	// Start is called before the first frame update
	void Start(){
		canvas = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Canvas"));
		text = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Text"));
		text.transform.SetParent(canvas.transform);
		text.transform.position = new Vector2(0,0);

	}

	// Update is called once per frame
	void Update(){
		text.GetComponent<Text>().text = Input.GetAxis("Mouse X").ToString();
		if(isSelecting){

		}else{
			if(Input.GetAxis("Left Mouse") > 0){
				float x = Input.GetAxis("Mouse X");
				float y = Input.GetAxis("Mouse Y");
				selectionStart = new Vector2(x,y);
			}
			if(Input.GetAxis("Right Mouse") > 0){
				//cancel stuff and order stuff
			}
		}
	}
}
