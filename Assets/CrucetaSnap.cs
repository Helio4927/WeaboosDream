using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrucetaSnap : MonoBehaviour {

    public Animator _animCruceta;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        _animCruceta.gameObject.SetActive(true);
        FindObjectOfType<CursorManager>().SetPuntoRojoTextura(true);
    }

    private void OnMouseExit()
    {
        _animCruceta.gameObject.SetActive(false);
        FindObjectOfType<CursorManager>().SetPuntoRojoTextura(false);
    }
}
