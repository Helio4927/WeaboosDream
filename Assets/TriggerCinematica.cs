using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCinematica : MonoBehaviour {
    public int idCinematica;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            llamarCinematica(idCinematica);
        }
    }

    public void llamarCinematica(int numCinematica)
    {
        if (numCinematica == 0)
        {
            print("llamamos a CamaraPrimerEncuentro PinkHorse");
            GameObject.Find("PinkHorseApuñalando").GetComponent<AudioSource>().volume = 0.13f;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DClampControl>().enabled = false;
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().enabled = true;
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("CamaraPrimerEncuentro PinkHorse");
            
        }
        else if (numCinematica == 1)
        {
            print("llamamos a CamaraPrimerEncuentro  encuentrobuzo");
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DClampControl>().enabled = false;
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().enabled = true;
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("CamaraEncuentroBuzo");
        }
        else if (numCinematica == 2)
        {
            print("llamamos a  inicio conversacion buzo");
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DClampControl>().enabled = false;
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().enabled = true;
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("camaraInicioConversacionBuzo");
        }
        else if (numCinematica == 3)
        {
            print("llamamos a CamaraDejaEncuentroBuzo");
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DClampControl>().enabled = false;
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().enabled = true;
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("CamaraDejaEncuentroBuzo");
             FindObjectOfType<CursorManager>().reactivarCursor();
        }
        else if (numCinematica == 4)
        {
            print("llamamos a camaraloki");
              GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<auxiliarCamaraEvents>().EjecutarAnimacionPesadillas();
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DClampControl>().enabled = false;
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().enabled = true;
              GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("HabitacionPerturbadora");
            GameObject.Find("PaqueteComandante 2(Alcantarilla)").transform.GetChild(1).gameObject.SetActive(true);
        }
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
