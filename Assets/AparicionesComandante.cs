using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparicionesComandante : MonoBehaviour {

    public Animator _animEvento;
    public Animator _animComander;
    public GameManager gameManagerRef;
    public int numeroAparicion;
    // Use this for initialization
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AparicionComander(int numAparicion)
    {
        switch (numAparicion)
        {
            case 0:
                _animEvento.Play("ComandanteMovimientoEvento1");
                Invoke("DestruirEsteTrigger", 10);
                print("primera aparicion Comandante");
                break;
            case 1:
                _animEvento.Play("ComandanteIdleEvento2");//AQUIII
                Invoke("DestruirEsteTrigger", 7);
                print("segunda aparicion Comandante");
                break;
            case 2:
                print("tercera aparicion Comandante");
                break;
            case 3:
                print("cuarta aparicion Comandante");
                break;
            case 4:
                print("quinta aparicion Comandante");
                break;
            default:
                break;
        }

       
    }
    public void DestruirEsteTrigger()
    {
        FindObjectOfType<GameManager>()._panelOpen = false;
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            AparicionComander(numeroAparicion);
        }
    }
}
