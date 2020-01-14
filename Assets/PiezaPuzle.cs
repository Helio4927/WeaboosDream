using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiezaPuzle : MonoBehaviour
{
    public bool breakPoint;
    private bool _estaEnPosicion = true;
    private bool llevaElectricidad = true;
    public int contador = 0;
    float realRotation;
    private ManagerPuzlePiezas _managerPuzle;
    [SerializeField]
    public ConectorPuzle conectorA;     //arriba
    [SerializeField]
    public ConectorPuzle conectorB;     //Derecha
    [SerializeField]
    public ConectorPuzle conectorC;     //Abajo
    [SerializeField]
    public ConectorPuzle conectorD;     //izquierda
    [SerializeField]
    public List<ConectorPuzle> listaConectores;
    public bool noSePuedeMover;
    public GameObject chapa1;
    public GameObject chapa2;
    public GameObject piezaLEspecial;

    public SoundManager soundManager;

    public enum Direccion
    {
        ARRIBA, ABAJO, DERECHA, IZQUIERDA
    }


    // Use this for initialization
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        listaConectores = new List<ConectorPuzle>();
        listaConectores.Add(conectorA);
        listaConectores.Add(conectorB);
        listaConectores.Add(conectorC);
        listaConectores.Add(conectorD);       

        // valor inicial para reseteo
        llevaElectricidad = true;
    }

    private void OnMouseDown()
    {
        if (!noSePuedeMover)
        {
            soundManager.BotonTuberia();
            RotatePiece();
            CheckConectorValues();
            _managerPuzle.RecorrerPiezas();
            _managerPuzle.RevisarPiezaEspecial();
            _managerPuzle.RecorrerPiezas();
        }
    }

    public void RotatePiece()
    {        
        realRotation -= 90;
        if (Input.GetMouseButtonDown(0))
        {
            transform.rotation = Quaternion.Euler(0, 0, realRotation);
        }
       
    }

    //se cambia de color a conectado y se pone corriente
    public void SetColorConectar()
    {
        if (noSePuedeMover)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = _managerPuzle.conectado; // CONECTADO
        }
        llevaElectricidad = true;

        if (this.gameObject.name.Equals("FinalPieza"))      //  HAN DADO LUZ A FINAL PIEZA
        {
            CancelInvoke("AuxTerminarPuzleTuberias");
            foreach (PiezaPuzle item in _managerPuzle.piezas)// congelamos las piezas
            {
                item.noSePuedeMover = true;
            }       
            Invoke("EncenderBombilla", 0.2f);// METODO QUE ENCIENDE LA BOMBILLA
            Invoke("DarInterruptorArriba", 1f); // METODO QUE ENCIENDE EL INTERRUPTOR
            Invoke("AuxTerminarPuzleTuberias", 3f);// aqui acaba el puzle
        }

        if (this.gameObject == chapa1 || this.gameObject == chapa2)
        {
            print("<color=purple>" + gameObject.name +" comprueba conectar Chapa y o es chapa 1 o es chapa 2 "+ "  </color>");
            /*piezaLEspecial.GetComponent<Transform>().rotation = Quaternion.identity;
            piezaLEspecial.GetComponent<PiezaPuzle>().conectorA.esSalida = false;
            piezaLEspecial.GetComponent<PiezaPuzle>().conectorB.esSalida = true;
            piezaLEspecial.GetComponent<PiezaPuzle>().conectorC.esSalida = true;
            piezaLEspecial.GetComponent<PiezaPuzle>().conectorD.esSalida = false;
            RotarAIncorrecto();
            _managerPuzle.nodobombilla.SetActive(false);*/
        }
    }

    public void EncenderBombilla()
    {
        _managerPuzle.bombillaPuzleTerminado.SetActive(true);
    }

    public void DarInterruptorArriba()
    {
        print("dar interruptor okj");
        FindObjectOfType<SoundManager>().PlaySound(4);
        _managerPuzle.interruptorOff.SetActive(false);
        _managerPuzle.interruptorOn.SetActive(true);

    }

    public void RotarACorrecto()
    {
        if (_estaEnPosicion) return;
        print("RotarACorrecto");
        for (int i = 0; i < 3; i++)
        {
            realRotation -= 90;
            transform.rotation = Quaternion.Euler(0, 0, realRotation);
            CheckConectorValues();
        }        
        _estaEnPosicion = true;
    }

    public void RotarAIncorrecto()
    {
        if (!_estaEnPosicion) return;
        print("RotarAIncorrecto");
        realRotation -= 90;
        transform.rotation = Quaternion.Euler(0, 0, realRotation);        
        CheckConectorValues();
        _estaEnPosicion = false;
    }

    public void AuxTerminarPuzleTuberias()
    {
        FindObjectOfType<GameManager>().TerminarPuzleTuberias();
    }
    //se cambia color y se quita corriente
    public void SetColorDesconectar()
    {
        Debug.Log("DesconectarSalidas: " + name);
        if (noSePuedeMover)
        {
            GetComponent<SpriteRenderer>().color = Color.red; // DESCONECTADO
        }
        else
        {
            GetComponent<SpriteRenderer>().color = _managerPuzle.sinConectar; // DESCONECTADO
        }
 
        llevaElectricidad = false;       
        
    }

    public void DesconectarSalidas()
    {
        SetColorDesconectar();
        foreach (var conectorAux in listaConectores)
        {
            if (conectorAux.piezaAsociada != null && conectorAux.piezaAsociada.llevaElectricidad)
            {
                conectorAux.piezaAsociada.DesconectarSalidas();
            }
        }
    }

    //modifica los valores de las salidas
    public void CheckConectorValues()
    {
        bool auxConectorAEsSalida = conectorA.esSalida;
        bool auxConectorBEsSalida = conectorB.esSalida;
        bool auxConectorCEsSalida = conectorC.esSalida;
        bool auxConectorDEsSalida = conectorD.esSalida;

        conectorA.ReasignarSalida(auxConectorDEsSalida);
        conectorB.ReasignarSalida(auxConectorAEsSalida);
        conectorC.ReasignarSalida(auxConectorBEsSalida);
        conectorD.ReasignarSalida(auxConectorCEsSalida);
    }

    public void SetManager(ManagerPuzlePiezas manager)
    {
        _managerPuzle = manager;
    }

    //verifica si existe alguna salida en esta pieza y pasa la corriente a sus conectores
    public void VerificarSalidasAdicionales()
    {
        foreach (var conectorTemp in listaConectores)
        {
            if (conectorTemp.esSalida)
            {
                if (llevaElectricidad && conectorTemp.piezaAsociada != null && !conectorTemp.piezaAsociada.llevaElectricidad)
                {
                    conectorTemp.piezaAsociada.ConectarSalida(conectorTemp.direccion);
                }
            }
        }
    }

    //se conecta dependiendo de la direccion de conexion
    public void ConectarSalida(Direccion dir)
    {
        if (breakPoint)
        {
            Debug.Log("Detener");
        }

        foreach (var conectorTemp in listaConectores)
        {
            if (conectorTemp.esSalida)
            {
                if (dir == Direccion.ARRIBA && conectorTemp.direccion == Direccion.ABAJO)
                {               
                    SetColorConectar();
                    break;  
                }
                else if (dir == Direccion.DERECHA && conectorTemp.direccion == Direccion.IZQUIERDA)
                {                
                    SetColorConectar();
                    break;
                }
                else if (dir == Direccion.ABAJO && conectorTemp.direccion == Direccion.ARRIBA)
                {                
                    SetColorConectar();
                    break;
                }
                else if (dir == Direccion.IZQUIERDA && conectorTemp.direccion == Direccion.DERECHA)
                {                
                    SetColorConectar();
                    break;             
                }
            }
            
        }

        VerificarSalidasAdicionales();
    }

    public bool LlevaElectricidad
    {
        get
        {
            return llevaElectricidad;
        }
    }
}
