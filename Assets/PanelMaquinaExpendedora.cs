using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMaquinaExpendedora : MonoBehaviour {

    public string combinacionCorrecta;
    public int primerDigitoCorrecto;
    public int segundoDigitoCorrecto;
    public int tercerDigitoCorrecto;
    public string combinacionActual = "";
    public GameObject[] checkSuccesArray;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RecibirDigito(int digito)
    {
        combinacionActual += digito.ToString();
        if (combinacionActual.Length == 1)
        {
            if (digito == primerDigitoCorrecto)
            {
                checkSuccesArray[0].GetComponent<Image>().color = Color.green;
            }
            else
            {
                checkSuccesArray[0].GetComponent<Image>().color = Color.red;
                comprobarCombinacion(combinacionActual);
                combinacionActual = null;
            }
        }

        if (combinacionActual.Length == 2)
        {
            if (digito == segundoDigitoCorrecto)
            {
                checkSuccesArray[1].GetComponent<Image>().color = Color.green;
            }
            else
            {
                checkSuccesArray[1].GetComponent<Image>().color = Color.red;
                comprobarCombinacion(combinacionActual);
                combinacionActual = null;
            }
        }
        else if (combinacionActual.Length == 3)
        {

            comprobarCombinacion(combinacionActual);
            combinacionActual = null;

           
        }
    }

    public void comprobarCombinacion(string combinacion)
    {
        if (combinacion == combinacionCorrecta)
        {
            FindObjectOfType<SoundManager>().PlaySound(1);
            checkSuccesArray[2].GetComponent<Image>().color = Color.green;
            print("<Color=orange>" + "Puzle completado, dame el curry </Color>");
            GameObject.Find("PanelTecladoMaquinaExpendedora").SetActive(false);
           GameObject.Find("AreaInteraccionPuzzle").GetComponent<InfoExam>().MostrarCogerObjetoCurry();
        }
        else
        {
            print("<Color=red>" + "has anadido 3 digitos incorrectos </Color>");
            foreach (var tempArrayItem in checkSuccesArray)
            {
                tempArrayItem.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
