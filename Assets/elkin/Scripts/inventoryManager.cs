using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class inventoryManager : MonoBehaviour {

    public GameObject padreObjetos;
    public GameObject currentItem;
    public GameObject botonUsar;
    public GameObject botonEquipar;
    public GameObject panelExaminar;
    public GameObject panelEquipar;
    public GameObject topeIzquierda;
    public GameObject topeDerecha;
    public GameObject itemVacioAllenar;
    private GameObject auxiliarNuevoObjeto;
    public GameObject objetoLetrasMarcaCurry;
    public GameObject panelBlockUsar;
    public GameObject panelBlockExam;

    public List<GameObject> itemSlots = new List<GameObject>();

    public Image imagenDeExaminar;
    public Image imagenDeEquipar;

    public Text textoEquipar;
    public Text textoExaminar;

    public bool ultimoMovidoDerecha;
    public bool candadoMovimientoInventario;
    
    public int numeroSlotRemovido;

    public GameObject examinarWorldPanel;
    public GameObject worldInfoItemActual;
    public Animator _animPlayer;
    public GameObject botonInventarioPantalla;
    public GameObject botonUsarInv;
    public GameObject botonExaminarInv;
    public string currentId;
    public Animator statusAnimator;
    public Transform currentScenario;
    // public GameObject[] objetosInventario;
    public List<GameObject> itemsImages = new List<GameObject>();
    public bool inventoryOpen;
    public SoundManager soundManager;
    public GameManager gameMan;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    for (int i = 0; i < itemSlots.Count; i++)
        //    {
        //        print(itemSlots[i].name);
        //    }
        //}
    }
    public void Start()
    {
        gameMan = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
        InvokeRepeating("CheckINVButtonForEnemies",1,0.5f);
    }
    public void SetBoolInventoryOpen(bool Bool)
    {
        inventoryOpen = Bool;
    }
    public void CheckINVButtonForEnemies()
    {

        foreach (Transform child in currentScenario)
        {
            if (child.gameObject.tag.Equals("Enemy"))
            {
                botonInventarioPantalla.SetActive(false);
                return;
            }
            else
            {
                if (!inventoryOpen)
                {
                    botonInventarioPantalla.SetActive(true);
                    
                }
                else
                {
                    botonInventarioPantalla.SetActive(false);
                 
                }

                
            }
        }  
    }

    public void ColocarPanelExaminarMundoModoAceptar()
    {
        FindObjectOfType<CursorManager>().CursorMouseOn(true);
        FindObjectOfType<GameManager>()._panelOpen = true;
        examinarWorldPanel.transform.GetChild(3).gameObject.SetActive(false);
        if (gameMan.escenaIngles)
        {
            examinarWorldPanel.transform.GetChild(2).GetComponentInChildren<Text>().text = "Ok";
        }
        else
        {
            examinarWorldPanel.transform.GetChild(2).GetComponentInChildren<Text>().text = "Aceptar";
        }
        examinarWorldPanel.transform.GetChild(2).gameObject.transform.localPosition = new Vector3(examinarWorldPanel.transform.GetChild(0).gameObject.transform.localPosition.x, examinarWorldPanel.transform.GetChild(2).gameObject.transform.localPosition.y, examinarWorldPanel.transform.GetChild(2).gameObject.transform.localPosition.z);
    }
    public void ColocarPanelExaminarMundoModoSiNo()
    {
        FindObjectOfType<GameManager>()._panelOpen = true;
        examinarWorldPanel.transform.GetChild(3).gameObject.SetActive(true);
        if (gameMan.escenaIngles)
        {
            examinarWorldPanel.transform.GetChild(2).GetComponentInChildren<Text>().text = "Yes";
        }
        else
        {
            examinarWorldPanel.transform.GetChild(2).GetComponentInChildren<Text>().text = "Si";
        }
       
        examinarWorldPanel.transform.GetChild(2).gameObject.transform.localPosition = new Vector3(-257, examinarWorldPanel.transform.GetChild(2).gameObject.transform.localPosition.y, examinarWorldPanel.transform.GetChild(2).gameObject.transform.localPosition.z);

    }
 
    public void QuitarCandado()
    {
        candadoMovimientoInventario = false;
    }

    // Use this for initialization
    public void CheckItem()
    {
        if (currentItem.GetComponent<infoItem>().equipable)
        {
            //botonEquipar.SetActive(true);
            botonUsar.SetActive(false);
        }
        else
        {
          //  botonEquipar.SetActive(false);
            botonUsar.SetActive(true);
        }
        //if (!currentItem.GetComponent<infoItem>().ocupado)
        //{
        //    if (ultimoMovidoDerecha)
        //    {
        //        MoverIzquierda();
        //    }
        //    else
        //    {
        //        MoverDerecha();
        //    }
        //}
    }

    public void AddItem(infoItem item)//  REHACER
    {
        print("proviene de " + item.gameObject.name);
        print("REHACER SCRIPT");
        bool itemAdded = false;
        //auxiliarNuevoObjeto = Instantiate(itemVacioAllenar,new Vector3(itemSlots[itemSlots.Count - 1].transform.position.x + 200, itemSlots[itemSlots.Count - 1].transform.position.y, itemSlots[itemSlots.Count - 1].transform.position.z),Quaternion.identity, padreObjetos.transform);
        //topeDerecha.transform.position = new Vector3(auxiliarNuevoObjeto.transform.position.x+ 200, auxiliarNuevoObjeto.transform.position.y, auxiliarNuevoObjeto.transform.position.z);
        //        auxiliarNuevoObjeto.GetComponent<infoItem>().equipable = item.equipable;
        //        auxiliarNuevoObjeto.GetComponent<infoItem>().textoDeExaminado = item.textoDeExaminado;
        //        auxiliarNuevoObjeto.GetComponent<infoItem>().imagenAMostrarCuandoExaminamos = item.imagenAMostrarCuandoExaminamos;
        //        auxiliarNuevoObjeto.GetComponent<infoItem>().idItem = item.idItem;
        //        auxiliarNuevoObjeto.GetComponent<infoItem>().textoEquipar = item.textoEquipar;
        //        auxiliarNuevoObjeto.GetComponent<infoItem>().imagenDeEquipar = item.imagenDeEquipar;
        //        auxiliarNuevoObjeto.GetComponent<Image>().sprite = item.miniaturaInventario;
        //        auxiliarNuevoObjeto.name = item.nombreObjeto;
        //        auxiliarNuevoObjeto.GetComponent<infoItem>().ocupado = true;
        //        Debug.Log(item.name + "fue añadido");
        if (item.name == "ObjetoPalanca")
        {
            GameObject.Find("TriggerConversacionNoSinPalanca").SetActive(false);
            GameObject.Find("MuroNoSinPalanca").SetActive(false);
            _animPlayer.SetBool("Tuberia", true);
            FindObjectOfType<CursorManager>().CursorMouseOn(false);
        }

                itemAdded = true;

      //  itemSlots.Add(auxiliarNuevoObjeto);


        //Si el inventario está lleno
        if (!itemAdded)
        {
            Debug.Log("El inventario está lleno, no se añadió");
        }
    }

    public void AddItemManual (int itemNum)
    {
        itemsImages[itemNum].SetActive(true);
        inventoryOpen = true;

      
    }

    public void SelecionarItem(Button boton)
    {
        soundManager.PlaySound(11);
        boton.transform.parent.transform.parent.transform.parent.GetComponent<Image>().color = Color.yellow;
        currentId = boton.gameObject.transform.parent.gameObject.GetComponent<infoItem>().idItem;
        currentItem = boton.transform.parent.gameObject;
        //ANIMACION ARRIBA ABAJO
        boton.transform.parent.transform.parent.transform.parent.GetComponent<Animator>().SetBool("MeMuevo", true);   

        foreach (var item in itemsImages)
        {
            if (item != boton.transform.parent.gameObject)
            {
                item.transform.parent.gameObject.transform.parent.GetComponent<Image>().color = Color.red;
                item.transform.parent.gameObject.transform.parent.GetComponent<Animator>().SetBool("MeMuevo", false);
                //animacion se queda quieta
            }
        }
        if (boton.gameObject.transform.parent.gameObject.GetComponent<infoItem>().equipable)
        {
            panelBlockUsar.SetActive(false);
            botonUsar.GetComponent<Button>().enabled = true;
        }
        else
        {
            panelBlockUsar.SetActive(true);
            botonUsar.GetComponent<Button>().enabled = false;
        }
        panelBlockExam.SetActive(false);
        botonExaminarInv.GetComponent<Button>().enabled = true;

    }
    public void RemoveItem(string idItemLookFor)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].GetComponent<infoItem>().idItem == idItemLookFor)
            {
                Destroy(itemSlots[i]);
                print("Se ha destruido el gameobject id " + idItemLookFor);
                itemSlots.RemoveAt(i);
                print("Se ha removido de la lista el id " + idItemLookFor);
                numeroSlotRemovido = i;
                print("El objeto removido estaba en el slot " + numeroSlotRemovido);
            }
        }
       

        for (int i = numeroSlotRemovido; i < itemSlots.Count; i++)
        {
            itemSlots[i].GetComponent<RectTransform>().position = new Vector3(itemSlots[i].GetComponent<RectTransform>().position.x - 200, itemSlots[i].GetComponent<RectTransform>().position.y, itemSlots[i].GetComponent<RectTransform>().position.z);
            print("Se ha movido a la izquierda el objeto" + itemSlots[i].name);
        }
        int totalItems = itemSlots.Count;
       
        topeDerecha.transform.position = new Vector3(itemSlots[totalItems - 1].transform.position.x + 200, itemSlots[totalItems - 1].transform.position.y, itemSlots[totalItems - 1].transform.position.z);

    }

    public void RemoveItemManual(int itemNum)
    {
        itemsImages[itemNum].SetActive(false);
    }
    public void MoverDerecha()
    {
        if (!candadoMovimientoInventario)
        {
            padreObjetos.GetComponent<RectTransform>().position = new Vector3(padreObjetos.GetComponent<RectTransform>().position.x - 200, padreObjetos.GetComponent<RectTransform>().position.y, padreObjetos.GetComponent<RectTransform>().position.z);
            ultimoMovidoDerecha = true;
            Invoke("CheckItem", 0.1f);
            candadoMovimientoInventario = true;
            Invoke("QuitarCandado", 0.2f);
        }
    }
    public void MoverIzquierda()
    {
        padreObjetos.GetComponent<RectTransform>().position = new Vector3(padreObjetos.GetComponent<RectTransform>().position.x + 200, padreObjetos.GetComponent<RectTransform>().position.y, padreObjetos.GetComponent<RectTransform>().position.z);
        ultimoMovidoDerecha = false;
        Invoke("CheckItem", 0.1f);
        Invoke("QuitarCandado", 0.2f);
    }

    public void usar()
    {
        if (currentId.Contains("Rombo"))
        {
            FindObjectOfType<GameManager>().usandoRombo = true;
            FindObjectOfType<GameManager>().usandoCurry = false;
            FindObjectOfType<GameManager>().usandoMoneda = false;
            FindObjectOfType<CursorManager>().SetRomboTextura(true);         
            print("<Color=blue> tenemos el rombo "+"</Color>");

        }
        else if (currentId.Contains("Moneda"))
        {
            FindObjectOfType<GameManager>().usandoMoneda = true;
            FindObjectOfType<GameManager>().usandoRombo = false;
            FindObjectOfType<GameManager>().usandoCurry = false;
            FindObjectOfType<CursorManager>().SetMonedaTextura(true);
            print("<Color=purple> tenemos la moneda " + "</Color>");
        }
        else if (currentId.Contains("CurryObj"))
        {
            FindObjectOfType<GameManager>().usandoMoneda = false;
            FindObjectOfType<GameManager>().usandoRombo = false;
            FindObjectOfType<GameManager>().usandoCurry = true;
            FindObjectOfType<CursorManager>().SetCurryTextura(true);
            print("<Color=yellow> tenemos la curry " + "</Color>");
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        botonInventarioPantalla.SetActive(true);
        FindObjectOfType<GameManager>()._panelOpen = false;
        inventoryOpen = false;

    }
    public void equipar()
    {
        imagenDeEquipar.sprite = currentItem.GetComponent<infoItem>().imagenDeEquipar;
        textoEquipar.text = currentItem.GetComponent<infoItem>().textoEquipar;

        panelEquipar.SetActive(true);

        if (currentItem.GetComponent<infoItem>().idItem.Equals("BarraAcero00"))
        {
            print("se ha equipado la barra de acero");
            _animPlayer.SetBool("Tuberia", true);
        }
        
        //operamos con current item para saber que accion tenemos que hacer
    }

    public void salirPanelEquipar()
    {
        panelEquipar.SetActive(false);
    }

    public void examinar()
    {
        if (currentItem.name.Equals("ImageRestosCurry"))
        {
            objetoLetrasMarcaCurry.SetActive(true);
        }
        else
        {
            objetoLetrasMarcaCurry.SetActive(false);
        }
        imagenDeExaminar.sprite = currentItem.GetComponent<infoItem>().imagenAMostrarCuandoExaminamos;
        textoExaminar.text = currentItem.GetComponent<infoItem>().textoDeExaminado;

        panelExaminar.SetActive(true);
        //operamos con current item para saber que accion tenemos que hacer
    }
    public void salirPanelexaminar()
    {
        panelExaminar.SetActive(false);
    }
    public void VaciarCurrentItem()
    {
        foreach (var item in itemsImages)
        {
           
                item.transform.parent.gameObject.transform.parent.GetComponent<Image>().color = Color.red;
                item.transform.parent.gameObject.transform.parent.GetComponent<Animator>().SetBool("MeMuevo", false);
                //animacion se queda quieta
      
        }

        currentItem = null;

        panelBlockUsar.SetActive(true);
        panelBlockExam.SetActive(true);
        botonExaminarInv.GetComponent<Button>().enabled = false;
        botonUsar.GetComponent<Button>().enabled = false;

    }
}