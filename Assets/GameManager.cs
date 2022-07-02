using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    public CambioDeZona managerCambioZona;
    public GameObject player3D;
    public DialogueManager managerDialogue;
    public DialogueTrigger [] dialogueTriggersEnEsecena;
    public GameObject prefabEnemigo;
    public GameObject interfazCurry;
    public GameObject interfazPuzleTuberias;
    public inventoryManager managerInventario;
    public int numAparicionComandante;
    public bool _panelOpen;
    public bool cadaverExaminado;
    public CambiarSala[] objetosConCambiarSala;

    public GameObject PaqueteCarne;
    public GameObject PaqueteCarneAbierta;
    public GameObject PaqueteCadaveres;
    public GameObject sobranteRosa;

    public GameObject luzMaquinaExp;
    public GameObject cubreLED;

    public GameObject panelHerido;
    public GameObject panelGameOver;

    public GameObject romboColocado;

    public bool monedaBuzo;
    public bool puzzleTerminado;
    public bool usandoRombo;
    public bool usandoMoneda;
    public bool usandoCurry;
    public GameObject caidaWeaboos;

    public bool enMenuPrincipal;
    public int numeroEscenarioActual;

    public bool interacionBloqueada;
    public float tiempoDeTardanza;

    public Text textoCronometro;
    public GameObject dialogoPostCurry;
    public bool instrucionesExpendedora;
    Scene m_Scene;
    string sceneName;
    public bool escenaIngles;

    // Use this for initialization
    void Start () {
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        objetosConCambiarSala = FindObjectsOfType<CambiarSala>();

        if (sceneName == "Escena Principal ENG")
        {
            escenaIngles = true;
        }
                  }

    public void CheckSalirTecladoMaquinaExpendedora()
    {
        if (!instrucionesExpendedora)
        {
            GameObject.Find("TriggerConversacionExpendedora").GetComponent<DialogueTrigger>().DialogoEnTriggerEnter();
            instrucionesExpendedora = true;
        }
    }


    public void UpdateCronometro()
    {

        if (escenaIngles)
        {
            textoCronometro.text = " Took you " + (Time.timeSinceLevelLoad / 60).ToString("F2") + " minutes";
        }
        else
        {
            textoCronometro.text = " Has Tardado " + (Time.timeSinceLevelLoad / 60).ToString("F2") + " Minutos";
        }
       
    }
    public void ComprobacionesObjetoClave(GameObject objetoAComprobar)
    {
        if (usandoRombo)
        {
            print(objetoAComprobar.name);
            if (objetoAComprobar.name != "AreaInteracion02 PuertaNegra")
            {
                FindObjectOfType<CursorManager>().setNormalTexture();
                usandoRombo = false;
            }
        }
        if (usandoCurry)
        {
            if (objetoAComprobar.name != "AreaInteracion02 CarneBloqueada")
            {
                FindObjectOfType<CursorManager>().setNormalTexture();
                usandoCurry = false;
            }
        }
        if (usandoMoneda)
        {
            if (objetoAComprobar.name != "AreaInteraccionMaquinaExp")
            {
                FindObjectOfType<CursorManager>().setNormalTexture();
                usandoMoneda = false;
            }
        }

    }

  
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyUp(KeyCode.O))
        {
            GameObject instanciaEnemigoAux = Instantiate(prefabEnemigo, GameObject.Find("Player3D").transform.GetChild(2).position, Quaternion.identity, GameObject.Find("EscenarioCueva2").transform);
            instanciaEnemigoAux.name = "Enemigoinstanciadopruebas";
            GameObject.Find("Player3D").transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("Tuberia", true);            
        }
	}

    public void RecargarEscena()
    {
        if (escenaIngles)
        {
            SceneManager.LoadScene(4);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
        
    }

    public void VolverEscenaMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void TerminarPuzleTuberias()
    {
        print("terminarPuzleTuberias");
        puzzleTerminado = true;
        if (monedaBuzo)
        {
            GameObject.Find("AreaInteraccionMaquinaExp").GetComponent<InfoExam>().idExaminar = 4;
        }
        else
        {
            GameObject.Find("AreaInteraccionMaquinaExp").GetComponent<InfoExam>().idExaminar = 3;
        }
        GameObject.Find("PrefabPuzzleTuberias").SetActive(false);
        cubreLED.SetActive(false);
        luzMaquinaExp.SetActive(true);
        ClosePanelBool();
        FindObjectOfType<CursorManager>().CursorMouseOn(false);
    }
    public void ClosePanelBool()
    {
        _panelOpen = false;
    }
    public void LlamarDialogo1()
    {
        dialogueTriggersEnEsecena[0].TriggerDialogue();
    }

    public void InstanciarPrimerEnemigo()
    {         
        GameObject.Find("PinkHorseApuñalandoCadaverPadre").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("PinkHorseApuñalandoCadaverPadre").transform.GetChild(1).gameObject.SetActive(true);
        GameObject instanciaEnemigoAux = Instantiate(prefabEnemigo, GameObject.Find("PinkHorseApuñalandoCadaverPadre").transform.GetChild(2).position, Quaternion.identity, GameObject.Find("EscenarioCueva4").transform);
        instanciaEnemigoAux.name = "EnemigoEspecial";
     
    }

    public void EventoCarne()
    {
        //Panel Fade>Cambio de paquetes>Fade Fute
        FindObjectOfType<AuxPanelFade>().PanelFadeCarne();
        managerInventario.GetComponent<inventoryManager>().RemoveItemManual(5);


    }

    public void CambioPaquetesCarne()
    {
        PaqueteCarne.SetActive(false);
        PaqueteCarneAbierta.SetActive(true);
        GameObject.Find("Cueva8").SetActive(false);
        GameObject.Find("AreaInteracion02 CarneBloqueada").SetActive(false);
        
    }
  
}
