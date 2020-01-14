using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText; //nombre del que habla
	public Text dialogueText; //mensaje a mostrar
    public GameObject portraitImage;
	public Animator animator; //animator del panel de dialogos
    public int numeroAnimRetratos;
    public Animator animatorRetratos;
	public Queue<string> sentences;
    public string diagKey; //nombre del dialogo por si hace falta referenciarlo
    public bool converActiva;
    public DialogueTrigger dialogueTriggerRef;
    public int contadorFrases;
    public inventoryManager inventoryMan;
    public float letterPause = 0.05f;
    public AudioClip[] voiceSound;
    public AudioClip activeVoice;
    public AudioSource voiceSource;
    public GameObject botonFraseOculto;
    public bool escribiendo;
    public bool enemigoLevantado;
    public GameManager gameMan;

    // Use this for initialization
    void Start () {
		sentences = new Queue<string>();
        gameMan = FindObjectOfType<GameManager>();

    }
    void Update ()
    {
        if (converActiva && Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

	public void StartDialogue (Dialogue dialogue)
	{
        FindObjectOfType<GameManager>()._panelOpen = true;
        converActiva = true;
       //Time.timeScale = 0f; //Paramos el juego mientras hablamos

        animator.SetBool("IsOpen", true); //El panel de dialogos está abierto

        nameText.text = dialogue.name;
        diagKey = dialogue.diagKey;
        portraitImage.GetComponent<Image>().sprite = dialogue.portrait;
        numeroAnimRetratos = dialogue.numPortrait;
        AsignarRetrato(numeroAnimRetratos);




            sentences.Clear();
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        contadorFrases = sentences.Count;
        dialogueText.text = "";
        Invoke("DisplayNextSentence",1.5f);
        
	}
    public void AsignarRetrato(int idRetrato)

    {
        if (gameMan.escenaIngles)
        {
            switch (idRetrato)
            {

                case 0://Si habla la máscara LOCA
                    animatorRetratos.Play("RetratoMascaraAnim");
                    numeroAnimRetratos = 0;
                    activeVoice = voiceSound[0];
                    nameText.text = "Mask";

                    break;
                case 1: //Si habla el Weeb
                    animatorRetratos.Play("RetratoWeebAnim");
                    numeroAnimRetratos = 1;
                    activeVoice = voiceSound[1];
                    nameText.text = "Weeaboo";

                    break;
                case 2: //Si habla la máscara Normal
                    animatorRetratos.Play("RetratoMascaraNormalAnim");
                    numeroAnimRetratos = 2;
                    activeVoice = voiceSound[2];
                    nameText.text = "Mask";
                    break;
                case 3: //Si habla el buzo
                    animatorRetratos.Play("RetratoBuzoAnim");
                    numeroAnimRetratos = 3;
                    activeVoice = voiceSound[3];
                    nameText.text = "Diver";
                    break;

                case 4: //Si habla el Caballo
                    animatorRetratos.Play("RetratoHorseAnim");
                    numeroAnimRetratos = 4;
                    activeVoice = voiceSound[4];
                    nameText.text = "Pink Horse";
                    break;

                default:
                    break;
            }
        }
        else
        {
            switch (idRetrato)
            {

                case 0://Si habla la máscara LOCA
                    animatorRetratos.Play("RetratoMascaraAnim");
                    numeroAnimRetratos = 0;
                    activeVoice = voiceSound[0];
                    nameText.text = "Mascara";

                    break;
                case 1: //Si habla el Weeb
                    animatorRetratos.Play("RetratoWeebAnim");
                    numeroAnimRetratos = 1;
                    activeVoice = voiceSound[1];
                    nameText.text = "Weeaboo";

                    break;
                case 2: //Si habla la máscara Normal
                    animatorRetratos.Play("RetratoMascaraNormalAnim");
                    numeroAnimRetratos = 2;
                    activeVoice = voiceSound[2];
                    nameText.text = "Mascara";
                    break;
                case 3: //Si habla el buzo
                    animatorRetratos.Play("RetratoBuzoAnim");
                    numeroAnimRetratos = 3;
                    activeVoice = voiceSound[3];
                    nameText.text = "Buzo";
                    break;

                case 4: //Si habla el Caballo
                    animatorRetratos.Play("RetratoHorseAnim");
                    numeroAnimRetratos = 4;
                    activeVoice = voiceSound[4];
                    nameText.text = "Pink Horse";
                    break;

                default:
                    break;
            }
        }
        
    }

	public void DisplayNextSentence ()
	{
   
        if (escribiendo)
        {
            escribiendo = false;
        }
        else
        {

            if (sentences.Count == 0) //Si no hay más frases que mostrar, terminamos
            {
                if (diagKey == "DialogoBuzo")
                {
                    Invoke("EndDialogueBuzo1", 0.5f);
                }
                if (diagKey == "instrucciones")
                {
                    if (!enemigoLevantado)
                    {
                        enemigoLevantado = true;
                        FindObjectOfType<GameManager>().InstanciarPrimerEnemigo();
                    }
                 
                }
                EndDialogue();
                converActiva = false;

                return;
            }
            string sentence = sentences.Dequeue();
            botonFraseOculto.SetActive(false);

            //
            if (dialogueTriggerRef.idFraseRetrato.Length > 1)
            {
                if (dialogueTriggerRef.idFraseRetrato[sentences.Count] != numeroAnimRetratos)
                {
                    AsignarRetrato(dialogueTriggerRef.idFraseRetrato[sentences.Count]);
                }
            }

            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
	
	}

	IEnumerator TypeSentence (string sentence) //Con esto escribimos la frase poco a poco (si no recuerdo mal)
	{
        voiceSource.clip = activeVoice;
        dialogueText.text = "";
        botonFraseOculto.SetActive(true);
        escribiendo = true;
        foreach (char letter in sentence.ToCharArray())
            {
            if (escribiendo)
            {
                dialogueText.text += letter;
                voiceSource.Play();
                yield return new WaitForSeconds(letterPause);
            }
            else
            {
                dialogueText.text = sentence;
            } 
                
            }

        escribiendo = false;

       
    }

	public void EndDialogue()
	{
        if (diagKey == "diagFoto")
        {
            FindObjectOfType<InfoExam>().MostrarCogerObjetoFoto();
        }
        FindObjectOfType<GameManager>()._panelOpen = false;
        Time.timeScale = 1f;
        animator.SetBool("IsOpen", false);
        botonFraseOculto.SetActive(false);


        //FindObjectOfType<PlayerController>().DiagActive = false;

    }

    public void EndDialogueBuzo1()
    {
        inventoryMan.ColocarPanelExaminarMundoModoAceptar();
        inventoryMan.examinarWorldPanel.SetActive(true);
        inventoryMan.examinarWorldPanel.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("AuxObjetoTrozoCurry").GetComponent<infoItem>().imagenAMostrarCuandoExaminamos;
        if (gameMan.escenaIngles)
        {
            inventoryMan.examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "You got a curry envelope";
        }
        else
        {
            inventoryMan.examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "Has cogido envoltorio de Curry";
        }

        inventoryMan.examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        inventoryMan.examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(EndDIalogueBuzo2);
        inventoryMan.examinarWorldPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        FindObjectOfType<TriggerCinematica>().llamarCinematica(3);
    }

    public void EndDIalogueBuzo2()
    {
        inventoryMan.ColocarPanelExaminarMundoModoAceptar();
        inventoryMan.examinarWorldPanel.SetActive(true);
        inventoryMan.examinarWorldPanel.transform.GetChild(0).GetComponent<Image>().sprite = GameObject.Find("AuxObjetoMoneda").GetComponent<infoItem>().imagenAMostrarCuandoExaminamos;
        if (gameMan.escenaIngles)
        {
            inventoryMan.examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "You got a coin";
        }
        else
        {
            inventoryMan.examinarWorldPanel.transform.GetChild(1).GetComponent<Text>().text = "Has cogido moneda";
        }
        
        inventoryMan.examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
        inventoryMan.examinarWorldPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(AddItemsBuzo);
        inventoryMan.examinarWorldPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();

       
    
    }

    public void AddItemsBuzo()
    {
        //gameManager.managerInventario.gameObject.SetActive(true);
        inventoryMan.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //inventoryMan.AddItem(GameObject.Find("AuxObjetoTrozoCurry").GetComponent<infoItem>());
        //inventoryMan.AddItem(GameObject.Find("AuxObjetoMoneda").GetComponent<infoItem>());
        inventoryMan.AddItemManual(3);
        inventoryMan.AddItemManual(4);
        inventoryMan.examinarWorldPanel.SetActive(false);
        inventoryMan.ColocarPanelExaminarMundoModoSiNo();
        FindObjectOfType<GameManager>().monedaBuzo = true;

        if (FindObjectOfType<GameManager>().puzzleTerminado)
        {
            GameObject.Find("AreaInteraccionMaquinaExp").GetComponent<InfoExam>().idExaminar = 4;
        }
        //FindObjectOfType<CambiosEstadoComandante>().FreezePlayer();
        Invoke("InvokeRelease", 2f);
   

    }
    
    public void InvokeRelease()
    {
        FindObjectOfType<GameManager>()._panelOpen = false;
    }
}

