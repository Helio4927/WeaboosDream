using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConectorPuzle  {
    
    public bool esSalida;    
    public PiezaPuzle piezaAsociada;    
    public PiezaPuzle.Direccion direccion;    

    public void ReasignarSalida(bool salida)
    {
        esSalida = salida;
    }
}
