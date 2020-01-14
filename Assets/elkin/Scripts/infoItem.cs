using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class infoItem : MonoBehaviour
{
    public bool equipable;
    public bool ocupado;
    public string textoDeExaminado;
    public Sprite imagenAMostrarCuandoExaminamos;
    public string idItem;
    public string textoEquipar;
    public Sprite imagenDeEquipar;
    public Sprite miniaturaInventario;
    public string nombreObjeto;
    public inventoryManager inventoryManagerRef;
    public GameManager gameMan;

    public void Start()
    {
        gameMan = FindObjectOfType<GameManager>();
    }
    public void CallAddItem()
    {
        inventoryManagerRef.AddItem(this);
        inventoryManagerRef.examinarWorldPanel.SetActive(false);
        Destroy(this.gameObject);
    }
    public void AuxiliarCallAddItem()
    {
        if (idItem == "BarraAcero00")
        {
            Invoke("CallAddItem", 0.3f);
            FindObjectOfType<GameManager>()._panelOpen = false;
        }
        else
        {
            inventoryManagerRef.gameObject.SetActive(true);
            FindObjectOfType<GameManager>()._panelOpen = false;
            Invoke("CallAddItem", 0.3f);
        }
      
    }
    public void AbrirPanelExaminarObjeto()
    {
        inventoryManagerRef.examinarWorldPanel.SetActive(true);
        FindObjectOfType<GameManager>()._panelOpen = true;
        if (gameMan.escenaIngles)
        {
            GameObject.Find("TextoCogerObjeto").GetComponent<Text>().text = "¿Do you want to take " + nombreObjeto + "?";
        }

        else
        {
            GameObject.Find("TextoCogerObjeto").GetComponent<Text>().text = "¿Seguro que quieres coger " + nombreObjeto + "?";
        }
      
        GameObject.Find("ImagenDeObjeto").GetComponent<Image>().sprite = miniaturaInventario;
        GameObject.Find("BotonSi").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("BotonSi").GetComponent<Button>().onClick.AddListener(AuxiliarCallAddItem);
        FindObjectOfType<CursorManager>().CursorMouseOn(true);      
    }
    public void CallRemoveItem(int itemNum)
    {
        FindObjectOfType<inventoryManager>().RemoveItemManual(itemNum);
    }
}