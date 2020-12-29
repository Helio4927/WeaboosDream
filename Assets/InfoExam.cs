using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoExam : MonoBehaviour {

    public string textoExamWorld;
    public GameObject panelExamWorld;
    public GameObject botonSi;
    public GameObject botonNo;
    public GameObject botonCerrar;
    public GameObject puertaObjetivo;
    public Text currentText;
    public bool ejecutaEspecial;
    public bool cadaverExam;
    public int idExaminar;
    public GameManager gameManager;
    public GameObject managerInventario;
    public GameObject currentEscenario;
    public bool monedaUsada;
    public bool puedeExaminar;
    public bool tutorialPuertaMostrado;
    public GameManager gameMan;
	// Use this for initialization

	void Start () {


        gameMan = FindObjectOfType<GameManager>();

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

    public void Ejecutar()
    {
        if (!FindObjectOfType<GameManager>().interacionBloqueada)
        {
            puedeExaminar = true;
            foreach (Transform tempTransform in currentEscenario.transform)
            {
                if (tempTransform.tag.Equals("Enemy"))
                {
                    puedeExaminar = false;
                    break;
                }

            }

            if (puedeExaminar)
            {
                FindObjectOfType<GameManager>()._panelOpen = true;
                if (ejecutaEspecial)
                {
                        if (this.gameObject.name.Contains("AreaInteracion02 PuertaNegra") && FindObjectOfType<GameManager>().usandoRombo)
                        {
                            this.gameObject.GetComponent<InfoExam>().ejecutaEspecial = true;
                            this.gameObject.GetComponent<InfoExam>().idExaminar = 12;
                            StartCoroutine(FindObjectOfType<CambioDeZona>().CambioDeNivel(19, GameObject.Find("PosicionEntradaAlcantarilla1").transform, true));
                            gameManager._panelOpen = false;
                            gameManager.romboColocado.SetActive(true);
                            FindObjectOfType<ManagerSoundtrack>().PlaySoundtrackSong(1);
                            FindObjectOfType<SoundManager>().PlaySound(6);
                            managerInventario.GetComponent<inventoryManager>().RemoveItemManual(2);
                        }
                        else
                        {
                            EjecutarExamEspecial(idExaminar);
                        }
                 

                }
                else
                {
                
                        EjecutarExamSimple();
                        botonSi.SetActive(false);
                        botonNo.SetActive(false);
                        botonCerrar.SetActive(true);

                }
                FindObjectOfType<CursorManager>().QuitarTodasLasVariablesDeUsar();
            }
        }
    }

   
    public void AuxSonidoPuertaMetal()
    {
        FindObjectOfType<SoundManager>().PlaySound(5);
    }
    public void ClosePanel()
    {
        panelExamWorld.SetActive(false);
        FindObjectOfType<GameManager>()._panelOpen = false;
        FindObjectOfType<GameManager>().interacionBloqueada = false;
        FindObjectOfType<CursorManager>().CursorMouseOn(false);
    }
    public void destruirPuntoExam()
    {
        gameObject.SetActive(false);
    }



    public void EjecutarExamSimple()
    {
        FindObjectOfType<CursorManager>().CursorMouseOn(true);
        FindObjectOfType<GameManager>().interacionBloqueada = true;
        currentText.text = textoExamWorld;
        panelExamWorld.SetActive(true);
    }
    public void EjecutarExamEspecial(int idExam)
    {
        FindObjectOfType<CursorManager>().CursorMouseOn(true);
        if (!FindObjectOfType<GameManager>().interacionBloqueada)
        {
            FindObjectOfType<GameManager>().interacionBloqueada = true;


        switch (idExam)
        {
            case 0:
                currentText.text = textoExamWorld;
                panelExamWorld.SetActive(true);
                botonSi.SetActive(true);
                botonNo.SetActive(true);
                botonCerrar.SetActive(false);
                print("0");
                break;
            case 1: //Puerta a habitación con agujero
                currentText.text = textoExamWorld;
                panelExamWorld.SetActive(true);
                botonSi.SetActive(true);
                botonNo.SetActive(true);
                botonCerrar.SetActive(false);
                botonSi.GetComponent<Button>().onClick.RemoveAllListeners();
                botonSi.GetComponent<Button>().onClick.AddListener(puertaObjetivo.GetComponent<CambiarSala>().CambiarEscenario);
                    if (puertaObjetivo.name == "PuertaEscenario6")
                    {
                        botonSi.GetComponent<Button>().onClick.AddListener(AuxSonidoPuertaMetal);
                    }
                    botonSi.GetComponent<Button>().onClick.AddListener(ClosePanel);

                print("1");
                break;

            case 2: //Maquina expendedora SIN energía
                    if (gameMan.escenaIngles)
                    {
                        currentText.text = "It's off...";
                    }
                    else
                    {
                        currentText.text = "Está apagada...";
                    }
                   
            
                panelExamWorld.SetActive(true);
                botonSi.SetActive(false);
                botonNo.SetActive(false);
                botonCerrar.SetActive(true);
                print("2");
                break;

            case 3://Maquina expendedora CON energía y SIN moneda
                    if (gameMan.escenaIngles)
                    {
                        currentText.text = "It's on but I don't have change.";
                    }
                    else
                    {
                        currentText.text = "Está encendida pero no tengo suelto.";
                    }
             
            
                panelExamWorld.SetActive(true);
                botonSi.SetActive(false);
                botonNo.SetActive(false);
                botonCerrar.SetActive(true);
                print("3");
                break;

            case 4://Maquina expendedora CON energía y CON moneda
                if (!monedaUsada)
                {
                    if (gameManager.usandoMoneda)
                    {
                            if (gameMan.escenaIngles)
                            {
                                currentText.text = "It's on. Use coin?";
                            }
                            else
                            {
                                currentText.text = "Está encendida ¿Usar moneda?";
                            }
                        

                        panelExamWorld.SetActive(true);
                        botonSi.SetActive(true);
                        botonNo.SetActive(true);
                        botonCerrar.SetActive(false);
                        botonSi.GetComponent<Button>().onClick.RemoveAllListeners();
                        botonSi.GetComponent<Button>().onClick.AddListener(ClosePanel);
                            botonSi.GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<SoundManager>().PlaySound(3); });
                        botonSi.GetComponent<Button>().onClick.AddListener(EncenderInterfazCurry);


                    }
                    else
                    {
                            if (gameMan.escenaIngles)
                            {
                                currentText.text = "It's on, I should use the coin that diver gave me.";
                            }
                            else
                            {
                                currentText.text = "Ya funciona, debería usar la moneda que me dió el buzo.";
                            }
                            
                        panelExamWorld.SetActive(true);
                        botonSi.SetActive(false);
                        botonNo.SetActive(false);
                        botonCerrar.SetActive(true);
                        botonCerrar.GetComponent<Button>().onClick.RemoveAllListeners();
                        botonCerrar.GetComponent<Button>().onClick.AddListener(ClosePanel);
                    }
                   
                    //    managerInventario.GetComponent<inventoryManager>().InventoryStart();


                }
                else
                {                
                    EncenderInterfazCurry();

                }

                print("4");
                break;
            case 5://Puertas sin preguntar
           
                puertaObjetivo.GetComponent<CambiarSala>().CambiarEscenario();
                FindObjectOfType<GameManager>().ClosePanelBool();
               
                

                break;
            case 6://examinar cadaver
               
                if (!cadaverExam)
                { 
                    currentText.text = textoExamWorld;
                    panelExamWorld.SetActive(true);
                    botonSi.SetActive(true);
                    botonNo.SetActive(true);
                    botonSi.GetComponent<Button>().onClick.RemoveAllListeners();
                    botonSi.GetComponent<Button>().onClick.AddListener(ClosePanel);
                    botonSi.GetComponent<Button>().onClick.AddListener(MostrarCogerObjetoDeCadaver2);
                        botonCerrar.SetActive(false);
                    //managerInventario.GetComponent<inventoryManager>().InventoryStart();
                }
                else
                {
                        if (gameMan.escenaIngles)
                        {
                            currentText.text = "He doesn't have anything else.";
                        }
                        else
                        {
                            currentText.text = "No tiene nada más.";
                        }
                        
                    panelExamWorld.SetActive(true);
                    botonSi.SetActive(false);
                    botonNo.SetActive(false);
                    botonCerrar.SetActive(true);
                    botonCerrar.GetComponent<Button>().onClick.RemoveAllListeners();
                    botonCerrar.GetComponent<Button>().onClick.AddListener(ClosePanel);
                }

                break;

            case 7://Sacar la espada
                currentText.text = textoExamWorld;
                panelExamWorld.SetActive(true);
                botonSi.SetActive(true);
                botonNo.SetActive(true);
                botonCerrar.SetActive(false);
                botonSi.GetComponent<Button>().onClick.RemoveAllListeners();
                botonSi.GetComponent<Button>().onClick.AddListener(ClosePanel);           
                botonSi.GetComponent<Button>().onClick.AddListener(GameObject.Find("AuxDiagEspada").GetComponent<DialogueTrigger>().TriggerDialogue);
                break;
            case 8://Ácido

                if (gameManager.usandoCurry)
                {
                        if (gameMan.escenaIngles)
                        {
                            currentText.text = "Do you want to use the curry?";
                        }
                        else
                        {
                            currentText.text = "¿Quieres usar el curry?";
                        }

                        panelExamWorld.SetActive(true);
                    botonSi.SetActive(true);
                    botonNo.SetActive(true);
                    botonCerrar.SetActive(false);
                    botonSi.GetComponent<Button>().onClick.RemoveAllListeners();
                    botonSi.GetComponent<Button>().onClick.AddListener(FindObjectOfType<GameManager>().EventoCarne);
                    botonSi.GetComponent<Button>().onClick.AddListener(ClosePanel);
                }
                else
                {
                        if (gameMan.escenaIngles)
                        {
                            currentText.text = "I should use the curry";
                        }
                        else
                        {
                            currentText.text = "Debería usar el curry";
                        }
                        
                    panelExamWorld.SetActive(true);
                    botonSi.SetActive(false);
                    botonNo.SetActive(false);
                    botonCerrar.SetActive(true);
                    botonSi.GetComponent<Button>().onClick.RemoveAllListeners();
                    botonCerrar.GetComponent<Button>().onClick.AddListener(ClosePanel);
                }
       
            
                break;

            case 9:
            
                if (FindObjectOfType<GameManager>().puzzleTerminado)
                {
                        if (gameMan.escenaIngles)
                        {
                            currentText.text = "Better don't touch it, it's working now";
                        }
                        else
                        {
                            currentText.text = "Mejor no tocarlo, parece que ya funciona";
                        }
                        
                    botonSi.SetActive(false);
                    botonNo.SetActive(false);
                    botonCerrar.SetActive(true);
                    panelExamWorld.SetActive(true);
                }
                else
                {
                        if (gameMan.escenaIngles)
                        {
                            currentText.text = "Looks like an electric control panel. Use it?";
                        }
                        else
                        {
                            currentText.text = "Es un panel electrico ¿Manipular?";
                        }
                        
                    panelExamWorld.SetActive(true);
                    botonSi.SetActive(true);
                    botonNo.SetActive(true);
                    botonCerrar.SetActive(false);
                    botonSi.GetComponent<Button>().onClick.RemoveAllListeners();
                    botonSi.GetComponent<Button>().onClick.AddListener(ClosePanel);
                    botonSi.GetComponent<Button>().onClick.AddListener(EncenderInterfazTuberias);
                    botonNo.GetComponent<Button>().onClick.RemoveAllListeners();
                    botonNo.GetComponent<Button>().onClick.AddListener(ClosePanel);
                    //    managerInventario.GetComponent<inventoryManager>().InventoryStart();
                }
                break;

            case 10://agujero en el suelo
                    if (gameMan.escenaIngles)
                    {
                        currentText.text = "Jump into the hole?";
                    }
                    else
                    {
                        currentText.text = "¿Quieres saltar al agujero?";
                    }

                    
                    panelExamWorld.SetActive(true);
                    botonSi.SetActive(true);
                    botonNo.SetActive(true);
                    botonCerrar.SetActive(false);
                    botonSi.GetComponent<Button>().onClick.RemoveAllListeners();
                    botonSi.GetComponent<Button>().onClick.AddListener(ClosePanel);
                    botonSi.GetComponent<Button>().onClick.AddListener(activarCaidaWeaboos);
                    botonNo.GetComponent<Button>().onClick.RemoveAllListeners();
                    botonNo.GetComponent<Button>().onClick.AddListener(ClosePanel);
                break;

            case 11: //FotoChicaManga

                    currentText.text = textoExamWorld;
                    panelExamWorld.SetActive(true);
                    botonSi.SetActive(true);
                    botonNo.SetActive(true);
                    botonSi.GetComponent<Button>().onClick.RemoveAllListeners();
                    botonSi.GetComponent<Button>().onClick.AddListener(ClosePanel);
                    botonSi.GetComponent<Button>().onClick.AddListener(destruirPuntoExam);
                    botonSi.GetComponent<Button>().onClick.AddListener(() =>  GameObject.Find("TriggerConversacion FOTO").GetComponent<DialogueTrigger>().TriggerDialogue());
                   // botonSi.GetComponent<Button>().onClick.AddListener(MostrarCogerObjetoFoto);
                    botonCerrar.SetActive(false);


                break;
            case 12: //PuertaAlcacntarilla RomboUsado

                StartCoroutine(FindObjectOfType<CambioDeZona>().CambioDeNivel(19, GameObject.Find("PosicionEntradaAlcantarilla1").transform, true));

                break;
                case 13:
                    
                    currentText.text = textoExamWorld;
                    panelExamWorld.SetActive(true);
                    botonSi.SetActive(false);
                    botonNo.SetActive(false);
                    botonCerrar.SetActive(true);
                    botonCerrar.GetComponent<Button>().onClick.RemoveAllListeners();
                    botonCerrar.GetComponent<Button>().onClick.AddListener(ClosePanel);
                    botonCerrar.GetComponent<Button>().onClick.AddListener(dialogoPuerta);
                    print("case 13");

                    break;
                default:
                break;
        }
                   }
    }

    public void dialogoPuerta()
    {
        if (!tutorialPuertaMostrado)
        {
            GameObject.Find("TriggerConversacionPuertaNegra").GetComponent<DialogueTrigger>().DialogoEnTriggerEnter();
            tutorialPuertaMostrado = true;
        }
    }
    public void activarCaidaWeaboos()
    {
        FindObjectOfType<GameManager>().caidaWeaboos.SetActive(true);
        FindObjectOfType<GameManager>().caidaWeaboos.GetComponent<Animator>().Play("AnimacionCaidaPlayer");
        FindObjectOfType<GameManager>()._panelOpen = true;
        FindObjectOfType<Player>().transform.GetChild(0).gameObject.SetActive(false);
        Invoke("GameOverTextoEncender", 8);
        Invoke("InvokePanelGameOver", 12);
        if (GameObject.Find("INV").activeInHierarchy)
        {
            GameObject.Find("INV").SetActive(false);
        }
       
        
        
    }

    public void InvokePanelGameOver()
    {

        gameManager.panelGameOver.SetActive(true);
        FindObjectOfType<CursorManager>().CursorMouseOn(true);
    }
    public void GameOverTextoEncender()
    {
        GameObject.Find("TextGameOver").GetComponent<Text>().enabled = true;
    }
    public void AddItemManualDesdeExam(int itemNum)
    {
        managerInventario.GetComponent<inventoryManager>().AddItemManual(itemNum);
        if (itemNum == 1)
        {
            //GameObject.Find("AreaInteracion04Foto").SetActive(false);
        }
    }
    //public void MostrarCogerObjetoDeCadaver1()
    //{
    //    cadaverExam = true;
    //    managerInventario.GetComponent<inventoryManager>().ColocarPanelExaminarMundoModoAceptar();
    //    managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.SetActive(true);
    //    managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("Objeto Foto").GetComponent<infoItem>().imagenAMostrarCuandoExaminamos;
    //    managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "Has Cogido Foto";
    //    managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
    //    managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.currydsadaa.GetChild(2).GetComponent<Button>().onClick.AddListener(MostrarCogerObjetoDeCadaver2);
    //    managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
    //}
    public void MostrarCogerObjetoDeCadaver2()
    {
        FindObjectOfType<CursorManager>().CursorMouseOn(true);
        cadaverExam = true;
        GameObject.Find("AreaInteracion02 PuertaNegra").GetComponent<InfoExam>().ejecutaEspecial = true;
        managerInventario.GetComponent<inventoryManager>().ColocarPanelExaminarMundoModoAceptar();
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.SetActive(true);
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("Objeto Rombo").GetComponent<infoItem>().imagenAMostrarCuandoExaminamos;
        
        if (gameMan.escenaIngles)
        {
            managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "I got a Rhombus";
        }
        else
        {
            managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "Tengo Rombo";
        }
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => AddItemManualDesdeExam(2));
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(MostrarCogerObjetoDeCadaver3);
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
    }
    public void MostrarCogerObjetoDeCadaver3()
    {
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.SetActive(true);
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.GetComponent<AudioSource>().Play();
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("Objeto Carta").GetComponent<infoItem>().imagenAMostrarCuandoExaminamos;
        if (gameMan.escenaIngles)
        {
            managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "I got a Letter";
        }
        else
        {
            managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "Tengo Carta";
        }
        
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => AddItemManualDesdeExam(0));
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(AddItemsCadaver);
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
    }
    public void AddItemsCadaver()
    {
        gameManager.managerInventario.gameObject.SetActive(true);
        managerInventario.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        FindObjectOfType<CursorManager>().CursorMouseOn(true);
        //managerInventario.GetComponent<inventoryManager>().AddItem(GameObject.Find("Objeto Foto").GetComponent<infoItem>());
        //managerInventario.GetComponent<inventoryManager>().AddItem(GameObject.Find("Objeto Rombo").GetComponent<infoItem>());
        //managerInventario.GetComponent<inventoryManager>().AddItem(GameObject.Find("Objeto Carta").GetComponent<infoItem>());
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.SetActive(false);
        managerInventario.GetComponent<inventoryManager>().ColocarPanelExaminarMundoModoSiNo();


    }
    public void MostrarCogerObjetoFoto()
    {
        managerInventario.GetComponent<inventoryManager>().ColocarPanelExaminarMundoModoAceptar();
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.SetActive(true);
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("Objeto Foto").GetComponent<infoItem>().imagenAMostrarCuandoExaminamos;
        if (gameMan.escenaIngles)
        {
            managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "I got a Photo";
        }
        else
        {
            managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "Tengo Foto";
        }


       ;
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => AddItemManualDesdeExam(1));
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(AddItemsCadaver);
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void MostrarCogerObjetoCurry()
    {
        managerInventario.GetComponent<inventoryManager>().ColocarPanelExaminarMundoModoAceptar();
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.SetActive(true);
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("AuxObjetoPaqueteCurry").GetComponent<infoItem>().imagenAMostrarCuandoExaminamos;
        if (gameMan.escenaIngles)
        {
            managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "I got a Ready-Made Curry";
        }
        else
        {
            managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "Has recogido Curry Instantáneo";
        }
       
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(AddCurry);
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();

    }
    public void AddCurry()
    {
        FindObjectOfType<GameManager>().PaqueteCadaveres.SetActive(true);
        FindObjectOfType<GameManager>().dialogoPostCurry.SetActive(true);
        managerInventario.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        FindObjectOfType<CursorManager>().CursorMouseOn(true);
        //FindObjectOfType<inventoryManager>().RemoveItem("monedaBuzo");
        FindObjectOfType<inventoryManager>().RemoveItemManual(3);
       // managerInventario.GetComponent<inventoryManager>().AddItem(GameObject.Find("AuxObjetoPaqueteCurry").GetComponent<infoItem>());
        managerInventario.GetComponent<inventoryManager>().AddItemManual(5);
        GameObject.Find("AreaInteracion02 CarneBloqueada").GetComponent<InfoExam>().idExaminar = 8;
        GameObject.Find("AreaInteracion02 CarneBloqueada").GetComponent<InfoExam>().ejecutaEspecial = true;
        managerInventario.GetComponent<inventoryManager>().examinarWorldPanel.SetActive(false);
        managerInventario.GetComponent<inventoryManager>().ColocarPanelExaminarMundoModoSiNo();
        GameObject.Find("AreaInteraccionMaquinaExp").GetComponent<InfoExam>().idExaminar = 3;
        GameObject.Find("buzo").SetActive(false);
        foreach (CambiarSala item in FindObjectOfType<GameManager>().objetosConCambiarSala)
        {
            item.cambioCancion = false;
        }

        GameObject.Find("CambioVueltaA15 Salida").GetComponent<CambiarSala>().cambioCancion = true;

        FindObjectOfType<CambiosEstadoComandante>().FreezePlayer();


    }

    public void EncenderInterfazCurry()
    {
        FindObjectOfType<GameManager>().interfazCurry.SetActive(true);
        FindObjectOfType<GameManager>()._panelOpen = true;
        FindObjectOfType<CursorManager>().CursorMouseOn(true);
        monedaUsada = true;
    }
    public void EncenderInterfazTuberias()
    {
        FindObjectOfType<GameManager>().interfazPuzleTuberias.SetActive(true);
        FindObjectOfType<GameManager>()._panelOpen = true;
        FindObjectOfType<CursorManager>().CursorMouseOn(true);
    }
}
