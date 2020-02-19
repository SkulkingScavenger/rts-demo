using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour{

	public List<Unit> unitList = new List<Unit>();

    // Start is called before the first frame update
    void Start()
    {
    	CreateUnit(new Vector3(0,1,0));
    	CreateUnit(new Vector3(2,1,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateUnit(Vector3 pos){
    	GameObject go;
    	go = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Unit"));
    	go.transform.position = pos;
    	unitList.Add(go.GetComponent<Unit>());
    	return go;
    }

    public void Delete(Unit unit){

    }
}
