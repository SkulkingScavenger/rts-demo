using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select(){
    	Renderer r;
    	r = gameObject.GetComponent<Renderer>();
    	r.material.color = new Color(1f,1f,1f,1f);
    }
}
