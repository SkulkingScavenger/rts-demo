using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour{

	public List<Unit> unitList = new List<Unit>();
    public Player[] players = new Player[16];

    // Start is called before the first frame update
    void Start()
    {
        GameObject go;
        go = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/PlayerInterface"));
        players[0] = new Player();
        go.GetComponent<PlayerInterface>().player = players[0];

        players[1] = new Player();

    	CreateUnit(new Vector3(0,1,0),0,0);
    	CreateUnit(new Vector3(2,1,0),0,0);

        CreateUnit(new Vector3(2,2,0),0,1);
        CreateUnit(new Vector3(2,3,0),0,1);
        CreateUnit(new Vector3(2,4,0),0,1);
        CreateUnit(new Vector3(2,5,0),0,1);
        CreateUnit(new Vector3(2,6,0),0,1);
        CreateUnit(new Vector3(2,7,0),0,1);
        CreateUnit(new Vector3(2,8,0),0,1);
        CreateUnit(new Vector3(2,9,0),0,1);
    }

    // Update is called once per frame
    void Update(){
        
    }

    public GameObject CreateUnit(Vector3 pos, int unitTypeId, int owningPlayer){
    	GameObject go;
    	go = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Unit"));
    	go.transform.position = pos;
    	unitList.Add(go.GetComponent<Unit>());

        UnitData unitData = UnitManager.Instance.unitData[unitTypeId];
        go.GetComponent<Unit>().speed = unitData.speed;
        go.GetComponent<Unit>().visionRadius = unitData.visionRadius;

        players[owningPlayer].ownedUnits.Add(go.GetComponent<Unit>());


    	return go;
    }

    public void Delete(Unit unit){

    }
}
