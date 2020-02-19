using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour{
	public UnitCommand currentCommand = null;
	public float speed = .5f;
	public List<Texture2D> frames = new List<Texture2D>();
	public int framesPerSecond = 10;

	Renderer scr;
	// Start is called before the first frame update
	void Start(){
		GameObject selectionCircle = transform.Find("SelectionCircle").gameObject;
		scr = selectionCircle.GetComponent<Renderer>();
		scr.enabled = false;
		Sprite[] sprites = Resources.LoadAll<Sprite>("Images/bumble");
		for(int i=0;i<sprites.Length;i++){
			Texture2D tex = new Texture2D(64, 64);
			tex.SetPixels(sprites[i].texture.GetPixels(64*i,0,64,64,0));
			tex.Apply();
			frames.Add(tex);
		}
	}

	// Update is called once per frame
	void Update(){
		int index = (int)((Time.time * framesPerSecond) % frames.Count);
		gameObject.GetComponent<Renderer>().material.mainTexture = frames[index];
		if(currentCommand!=null){
			executeCommand();
		}
	}

	public void Select(){
		scr.enabled = true;	
	}

	public void Deselect(){
		scr.enabled = false;	
	}

	public void executeCommand(){
		switch(currentCommand.id){
			case Commands.MOVETARGET: 
				MoveTarget();
				break;
			default:
				break;
		}
	}

	public void MoveTarget(){
		MoveCommand command = (MoveCommand)currentCommand;
		Vector2 targ = command.target;
		Vector2 pos = new Vector2(transform.position.x,transform.position.z);
		if(Vector2.Distance(targ, pos) < speed * Time.deltaTime){
			transform.position = new Vector3(targ.x, transform.position.y, targ.y);
			currentCommand = null;
		}else{
			CharacterController cc = gameObject.GetComponent<CharacterController>();
			targ = Vector2.MoveTowards(pos,targ,speed*Time.deltaTime);
			pos = targ - pos;
			//transform.position = new Vector3(pos.x, transform.position.y, pos.y);
			Vector3 move = new Vector3(pos.x,0f,pos.y);
			move.Normalize();
			cc.Move(move * speed * Time.deltaTime);
		}
	}
}
