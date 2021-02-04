using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour{
	public Unit target;
	public float speed;
	// Start is called before the first frame update
	void Start(){
		
	}

	// Update is called once per frame
	void Update(){
		Vector3 targetPos = new Vector3(target.transform.position.x, 0f, target.transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
	}
}
