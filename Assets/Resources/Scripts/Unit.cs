using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour{
	public UnitCommand currentCommand = null;
	[HideInInspector]
	public string name;
	public float speed = 5f;
	public bool visible = false;
	public float visionRadius = 5f;
	public List<Texture2D> frames = new List<Texture2D>();
	public int framesPerSecond = 10;
	public float collisionRadius = 1f;
	public float attackRange = 1f;
	public bool isMelee = true;
	public float projectileSpeed = 3f;
	UnityEngine.AI.NavMeshAgent agent;

	Renderer scr;
	// Start is called before the first frame update
	void Start(){
		GameObject selectionCircle = transform.Find("SelectionCircle").gameObject;
		scr = selectionCircle.GetComponent<Renderer>();
		scr.enabled = false;

		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = transform.position; 

		// Sprite[] sprites = Resources.LoadAll<Sprite>("Images/bumble");
		// for(int i=0;i<sprites.Length;i++){
		// 	Texture2D tex = new Texture2D(64, 64);
		// 	tex.SetPixels(sprites[i].texture.GetPixels(64*i,0,64,64,0));
		// 	tex.Apply();
		// 	frames.Add(tex);
		// }
	}

	// Update is called once per frame
	void Update(){
		//int index = (int)((Time.time * framesPerSecond) % frames.Count);
		//gameObject.GetComponent<Renderer>().material.mainTexture = frames[index];
		if(currentCommand!=null){
			executeCommand();
		}
		gameObject.GetComponent<Renderer>().material.color = new Color (1f,0f,0f,1f);
		if(visible){
			gameObject.GetComponent<Renderer>().material.color = new Color (1f,1f,1f,1f);
		}else{
			gameObject.GetComponent<Renderer>().material.color = new Color (0f,1f,1f,0f);
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
			case Commands.ATTACKTARGET: 
				AttackTarget();
				break;
			default:
				break;
		}
	}

	public void MoveTarget(){
		MoveCommand command = (MoveCommand)currentCommand;
		Vector2 targ = command.target;
		agent.destination = new Vector3(targ.x,0f,targ.y);
		//Vector2 pos = new Vector2(transform.position.x,transform.position.z);
		// if(Vector2.Distance(targ, pos) < speed * Time.deltaTime){
		// 	transform.position = new Vector3(targ.x, transform.position.y, targ.y);
		// 	currentCommand = null;
		// }else{
		// 	CharacterController cc = gameObject.GetComponent<CharacterController>();
		// 	targ = Vector2.MoveTowards(pos,targ,1);
		// 	pos = targ - pos;
		// 	//transform.position = new Vector3(pos.x, transform.position.y, pos.y);
		// 	Vector3 move = new Vector3(pos.x,0f,pos.y);
		// 	move.Normalize();
		// 	cc.Move(move * speed * Time.deltaTime);
		// }
	}

	public void AttackTarget(){
		AttackCommand command = (AttackCommand)currentCommand;
		Vector3 targetPos = new Vector3(command.target.transform.position.x, 0f, command.target.transform.position.y);
		float dist = Vector3.Distance(targetPos, transform.position);
		Debug.Log(dist);
		if(dist > attackRange){
			agent.destination = targetPos;
		}else{
			if(isMelee){
				//punch
			}else{
				GameObject go;
    			go = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Projectile"));
    			go.transform.position = transform.position;
    			Projectile projectile = go.GetComponent<Projectile>();
    			projectile.target = command.target;
    			projectile.speed = projectileSpeed;
    			currentCommand = null;
    			agent.destination = transform.position;
			}
		}
		

	}

}
