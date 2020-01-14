using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botonMaquinaExpendedora : MonoBehaviour {
    public int idboton;
    public PanelMaquinaExpendedora managerTecladoMaquina;
	// Use this for initialization
	void Start () {
        managerTecladoMaquina = transform.parent.gameObject.GetComponent<PanelMaquinaExpendedora>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnviarId()
    {
        print("se ha enviado el id"+ idboton);
        managerTecladoMaquina.RecibirDigito(idboton);
    }
}
