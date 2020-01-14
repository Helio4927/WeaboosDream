using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPuzlePiezas : MonoBehaviour {

    public PiezaPuzle piezaInicial;
    public PiezaPuzle[] piezas;
    public Color sinConectar;
    public Color conectado;
    public GameObject nodobombilla;
    public GameObject interruptorOn;
    public GameObject interruptorOff;
    public GameObject bombillaPuzleTerminado;
    public bool chapadaEncendida;

    public PiezaPuzle trampaIzq;
    public PiezaPuzle trampaDer;
    public PiezaPuzle piezaEspecial;

    private void Start()
    {
        piezaInicial.GetComponent<SpriteRenderer>().color = new Color(255, 249, 0, 255); // CONECTADO
        piezas = FindObjectsOfType<PiezaPuzle>();
        foreach (var piezaTemp in piezas)
        {
            piezaTemp.SetManager(this);
        }
        RecorrerPiezas();
    }

    public void RecorrerPiezas()
    {
        //se desconecta todo (se pone todo azul)
        piezaInicial.SetColorDesconectar();
        foreach (var conectorTemp in piezaInicial.listaConectores)
        {
            if (conectorTemp.piezaAsociada != null)
            {
                conectorTemp.piezaAsociada.DesconectarSalidas();
            }
        }

        //se activa unicamente el inicial
        piezaInicial.SetColorConectar();

        //se recorre cada pieza para determinar si recibe corriente
        foreach (var conectorTemp in piezaInicial.listaConectores)
        {
            if (conectorTemp.esSalida && conectorTemp.piezaAsociada != null)
            {
                conectorTemp.piezaAsociada.ConectarSalida(conectorTemp.direccion);
            }            
        }
    }

    public void RevisarPiezaEspecial()
    {
        if (trampaDer.LlevaElectricidad || trampaIzq.LlevaElectricidad)
        {
            piezaEspecial.RotarAIncorrecto();
            nodobombilla.SetActive(false);
        }
        else
        {
            piezaEspecial.RotarACorrecto();
            nodobombilla.SetActive(true);
        }
    }
}
