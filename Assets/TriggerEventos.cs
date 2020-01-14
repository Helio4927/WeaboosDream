using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventos : MonoBehaviour {


    public bool yaHecho;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //if (!yaHecho)
        //{
        //    FindObjectOfType<GameManager>().InstanciarPrimerEnemigo();
        //    yaHecho = true;
        //}
        
    }
}
