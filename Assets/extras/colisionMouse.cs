using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class colisionMouse : MonoBehaviour {

    public Text tiempodeCombo;
    public Text comboText;
    public bool comboActivo;
    public float contadorcombo;
    public float tiempoParaEncadenarCombo;
    public string cadenaDeCombos;
    public int combosEnlazados;
    public Animator animatorPlayer;
    public Animator animatorEnemy;
    // Use this for initialization
    void Start () {
        cadenaDeCombos = null;

    }
	
	// Update is called once per frame
	void Update () {
        comboText.text = cadenaDeCombos;
        tiempodeCombo.text = contadorcombo.ToString();
        if (comboActivo)
        {
            contadorcombo += Time.deltaTime;
            if (contadorcombo >= tiempoParaEncadenarCombo)// 1 para hacer pruebas
            {           
                comboActivo = false;
                cadenaDeCombos = null;
                contadorcombo = 0;
                combosEnlazados = 0;
                animatorPlayer.SetInteger("Combo Count", 0);
                animatorEnemy.SetInteger("Combo Count", 0);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Cabeza"))
        {
            print("ha entrado en area cabeza");
        }
        else if (collision.gameObject.tag.Equals("Torso"))
        {
            print("ha entrado en area Torso");
        }
        else if (collision.gameObject.tag.Equals("Pies"))
        {
            print("ha entrado en area Pies");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Cabeza"))
        {
            if ((Input.GetMouseButtonDown(0)))
            {
                combosEnlazados++;
                comboActivo = true;
             
                if (cadenaDeCombos == null)
                {
                    cadenaDeCombos = "C1INIT";
                    animatorPlayer.SetInteger("Combo Count", 1);
                    animatorEnemy.SetInteger("Combo Count", 1);
                    contadorcombo = 0;
                }
                else
                {
                    cadenaDeCombos += " C" + combosEnlazados;
                    contadorcombo = 0;
                }
            }
        }

    else if (collision.gameObject.tag.Equals("Torso"))
    {
        if ((Input.GetMouseButtonDown(0)))
        {
                combosEnlazados++;
                comboActivo = true;

            if (cadenaDeCombos == null)
            {
                cadenaDeCombos = "T1INIT";
                contadorcombo = 0;
            }
            else
            {
                cadenaDeCombos += " T" + combosEnlazados;
                contadorcombo = 0;
            }
                if (cadenaDeCombos.Equals("C1INIT T2"))// COMBO CABEZA + TORSO
                {
                    animatorPlayer.SetInteger("Combo Count", 2);
                    animatorEnemy.SetInteger("Combo Count", 2);
                }
        }
    }
        else if (collision.gameObject.tag.Equals("Pies"))
        {
            if ((Input.GetMouseButtonDown(0)))
            {
                combosEnlazados++;
                comboActivo = true;

                if (cadenaDeCombos == null)
                {
                    cadenaDeCombos = "P1INIT";
                    contadorcombo = 0;
                }
                else
                {
                    cadenaDeCombos += " P" + combosEnlazados;
                    contadorcombo = 0;
                }
            }
        }
    }

}
