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
        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.GetComponent<Camera2DClampControl>().enabled = false;
        mainCamera.GetComponent<Animator>().enabled = true;

        if (numCinematica == 0)
        {
            print("llamamos a CamaraPrimerEncuentro PinkHorse");
            
            var pinkHorse = GameObject.Find("PinkHorseApuñalando");

            if (pinkHorse)
            {
                pinkHorse.GetComponent<AudioSource>().volume = 0.13f;
            }
            else
            {
                Debug.LogError("No se encontro PinkHorse");
            }

            
            mainCamera.GetComponent<Animator>().Play("CamaraPrimerEncuentro PinkHorse");
            
        }
        else if (numCinematica == 1)
        {
            print("llamamos a CamaraPrimerEncuentro  encuentrobuzo");              
            mainCamera.GetComponent<Animator>().Play("CamaraEncuentroBuzo");
        }
        else if (numCinematica == 2)
        {
            print("llamamos a  inicio conversacion buzo");             
            mainCamera.GetComponent<Animator>().Play("camaraInicioConversacionBuzo");
        }
        else if (numCinematica == 3)
        {
            print("llamamos a CamaraDejaEncuentroBuzo");              
            mainCamera.GetComponent<Animator>().Play("CamaraDejaEncuentroBuzo");
            FindObjectOfType<CursorManager>().reactivarCursor();
        }
        else if (numCinematica == 4)
        {
            print("llamamos a camaraloki");
            mainCamera.gameObject.GetComponent<auxiliarCamaraEvents>().EjecutarAnimacionPesadillas();              
            mainCamera.GetComponent<Animator>().Play("HabitacionPerturbadora");
            GameObject.Find("PaqueteComandante 2(Alcantarilla)").transform.GetChild(1).gameObject.SetActive(true);
        }
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
