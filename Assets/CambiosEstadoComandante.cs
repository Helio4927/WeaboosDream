using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiosEstadoComandante : MonoBehaviour {

    public Animator _animComandante;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CambiarComandante(int numAnimacion)
    {
        switch (numAnimacion)
        {
            case 0:
                _animComandante.Play("CommanderGesto");
                Invoke("SpawnVerde", 1f);
                break;

            case 1:
                _animComandante.Play("CommanderIdle");
                break;
            case 2:
                FreezePlayer();
        
                _animComandante.Play("CommanderWalk");
                break;

            default:
                break;
        }
      
    }
    public void FreezePlayer()
    {
        print("freezePlayer");
        FindObjectOfType<GameManager>()._panelOpen = true;
    }

    public void UnfreezePlayer()
    {
        print("UnfreezePlayer");
        FindObjectOfType<GameManager>()._panelOpen = false;
    }

    public void SpawnVerde()
    {

        FindObjectOfType<ProvisionaSpawnEnemigoParaPruebas>().instanciarEnemigo();
    }
}
