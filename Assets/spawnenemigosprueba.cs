using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnenemigosprueba : MonoBehaviour {

    public bool haSpawneado;
    public GameObject prefabEnemigo;
    public float counter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!haSpawneado && Input.GetKeyDown("space"))
        {
            haSpawneado = true;
            Instantiate(prefabEnemigo,transform.position,Quaternion.identity,this.gameObject.transform);
            counter = 0;
        }
        counter += Time.deltaTime;
        if (counter >= 2)
        {
            haSpawneado = false;
        }
	}
}
