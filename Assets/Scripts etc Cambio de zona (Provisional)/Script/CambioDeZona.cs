using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CambioDeZona : MonoBehaviour {
    public GameObject player3D;
    public GameObject puntoDestinoPlayer;
    public GameObject[] escenarios;
    public Animator animFade;
    private Transform posicionCamaraEnEscenario;
    private Transform posicionPlayerEnEscenario;
    public int numeroEscenaDesdeTeclaSpace;
    public CameraShake camShake;
    // Use this for initialization
    void Start () {
        animFade = GameObject.Find("PanelFade").GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
       
     
    }

    public IEnumerator CambioDeNivel(int numeroEscenario,Transform posPlayerNew, bool clampCamera)
    {
        animFade.Play("FadeOut");
        FindObjectOfType<CursorManager>().CursorMouseOn(false);
        FindObjectOfType<GameManager>().numeroEscenarioActual = numeroEscenario;


        yield return new WaitForSeconds(0.5f);

                for (int i = 0; i < escenarios[numeroEscenario].transform.childCount; i++)
        {
            if (escenarios[numeroEscenario].transform.GetChild(i).gameObject.tag.Equals("PosCamaraEscenario"))
            {
                posicionCamaraEnEscenario = escenarios[numeroEscenario].transform.GetChild(i).transform;
            }
        }
        //PosCamaraEscenario
        if (clampCamera)
        {
            //enciende el script y pasa el background
            for (int i = 0; i < escenarios[numeroEscenario].transform.childCount; i++)
            {
                if (escenarios[numeroEscenario].transform.GetChild(i).gameObject.tag.Equals("FondoClamp"))
                {
                      GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DClampControl>().background = escenarios[numeroEscenario].transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                      GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DClampControl>().enabled = true;
                    //  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().enabled = true;
                    camShake.currentBackground = escenarios[numeroEscenario].transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                }        
            }
        }
        else
        {
            for (int i = 0; i < escenarios[numeroEscenario].transform.childCount; i++)
            {
                if (escenarios[numeroEscenario].transform.GetChild(i).gameObject.name.Equals("fondo"))
                {
                      GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DClampControl>().background = escenarios[numeroEscenario].transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
                      GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DClampControl>().enabled = true;
                  
                }
            }
            Invoke("Auxiliar", 0.3f);
        }

          GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(posicionCamaraEnEscenario.position.x, posicionCamaraEnEscenario.position.y,   GameObject.FindGameObjectWithTag("MainCamera").transform.position.z);

        posicionPlayerEnEscenario = posPlayerNew;



        player3D.GetComponent<NavMeshAgent>().enabled = false;
        player3D.GetComponent<CapsuleCollider>().enabled = false;
        //player3D.transform.position = new Vector3(posicionPlayerEnEscenario.position.x, posicionPlayerEnEscenario.position.y, player3D.transform.position.z);
        player3D.GetComponent<NavMeshAgent>().Warp(new Vector3(posicionPlayerEnEscenario.position.x, posicionPlayerEnEscenario.position.y, posicionPlayerEnEscenario.position.z));
        player3D.GetComponent<CapsuleCollider>().enabled = true;
        player3D.GetComponent<NavMeshAgent>().enabled = true;
        if (numeroEscenario == 19)
        {
            FindObjectOfType<GameManager>()._panelOpen = false; 
        }
        FindObjectOfType<inventoryManager>().currentScenario = escenarios[numeroEscenario].transform;
    }

    public void Auxiliar()
    {
        //  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().enabled = false;
          GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DClampControl>().enabled = false;
        //  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().enabled = true;

    }

    public void CambioDeNivel1(int numeroEscenario, Transform posPlayerNew)
    {     
        for (int i = 0; i < escenarios[numeroEscenario].transform.childCount; i++)
        {
            if (escenarios[numeroEscenario].transform.GetChild(i).gameObject.tag.Equals("PosCamaraEscenario"))
            {
                posicionCamaraEnEscenario = escenarios[numeroEscenario].transform.GetChild(i).transform;
            }
        }          
        //PosCamaraEscenario
          GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(posicionCamaraEnEscenario.position.x, posicionCamaraEnEscenario.position.y,   GameObject.FindGameObjectWithTag("MainCamera").transform.position.z);

        for (int i = 0; i < escenarios[numeroEscenario].transform.childCount; i++)
        {
            if (escenarios[numeroEscenario].transform.GetChild(i).gameObject.tag.Equals("PosEntradaPlayer"))
            {
                posicionPlayerEnEscenario = escenarios[numeroEscenario].transform.GetChild(i).transform;
            }
        }
      
        player3D.GetComponent<NavMeshAgent>().enabled = false;
        player3D.GetComponent<CapsuleCollider>().enabled = false;
        player3D.transform.position = new Vector3(posicionPlayerEnEscenario.position.x, posicionPlayerEnEscenario.position.y, player3D.transform.position.z);
        player3D.GetComponent<CapsuleCollider>().enabled = true;
        player3D.GetComponent<NavMeshAgent>().enabled = true;
        
    } 
}
