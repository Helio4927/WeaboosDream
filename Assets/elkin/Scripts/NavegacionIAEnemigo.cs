using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavegacionIAEnemigo : MonoBehaviour {

    public Transform playerSeguir;
	// Use this for initialization
	void Start () {
        InvokeRepeating("SeguirObjetivo", 0.1f,0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SeguirObjetivo()
    {
        GetComponent<NavMeshAgent>().SetDestination(playerSeguir.position);
    }
}
