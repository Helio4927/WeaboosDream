using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarSala : MonoBehaviour {

    public int numsala;
    public GameObject PosPlayer;
    public bool puerta;
    public bool clampCamera;
    public int tamanoCamara;
    public GameObject currentEscenario;
    public bool puedePasar;
    public float escalaPlayer;
    public bool cambioCancion;
    public int siguienteCancion;
	// Use this for initialization
	void Start () {
        if (currentEscenario == null)
        {
            if (!transform.parent.name.Contains("Suelo3D"))
            {
                currentEscenario = transform.parent.gameObject;
            }
            else
            {
                currentEscenario = transform.parent.gameObject.transform.parent.gameObject;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ajustarSizeCamara()
    {
          GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize = tamanoCamara;
    }
    
    public void CambiarEscenario()
    {
        if (cambioCancion)
        {
            FindObjectOfType<ManagerSoundtrack>().PlaySoundtrackSong(siguienteCancion);
        }
          GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().enabled = false;
      //    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().enabled = false;
          GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DClampControl>().enabled = false;
        Invoke("ajustarSizeCamara", 0.4f);
        for (int i = 0; i < FindObjectOfType<CambioDeZona>().escenarios.Length -1; i++)
        {
            for (int q = 0; q < FindObjectOfType<CambioDeZona>().escenarios[i].transform.childCount; q++)
            {
                if (FindObjectOfType<CambioDeZona>().escenarios[i].transform.GetChild
                    (q).tag.Equals("PosEntradaPlayer"))
                {
                    if (!FindObjectOfType<CambioDeZona>().escenarios[i].transform.GetChild(q) == PosPlayer)
                    {
                        FindObjectOfType<CambioDeZona>().escenarios[i].transform.GetChild(q).gameObject.SetActive(false);
                    }
                }
            }      
        }
      
        StartCoroutine(FindObjectOfType<CambioDeZona>().CambioDeNivel(numsala, PosPlayer.transform, clampCamera));
        //FindObjectOfType<CambioDeZona>().CambioDeNivel(numsala,PosPlayer.transform);
        FindObjectOfType<Player>().transform.GetChild(0).gameObject.transform.localScale = new Vector3(escalaPlayer, escalaPlayer, escalaPlayer);
    }
    private void OnTriggerEnter(Collider other)
    {
        //print("entra en el OnTriggerEnter");
        if (other.gameObject.tag.Equals("Player") && !puerta)
        {
            puedePasar = true;
            foreach (Transform tempTransform in currentEscenario.transform)
            {
                if (tempTransform.tag.Equals("Enemy"))
                {
                    puedePasar = false;
                }          
            }
                if (puedePasar)
                {
                   GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Animator>().enabled = false;
                    CambiarEscenario();
                }
          
        }
    }
}
