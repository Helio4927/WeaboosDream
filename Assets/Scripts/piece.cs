using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piece : MonoBehaviour
{  
    float realRotation;
    public int[] values;
    public bool[] valuesCheck;
    public bool padreInicioCamino;
    public bool llevaElectricidad;
    public bool noMovible;
    public bool mismoColor;
    public bool verde;
    public piece piezaEncima;
    public piece piezaDerecha;
    public piece piezaDebajo;
    public piece piezaIzquierda;
    bool estaConectado;
    

    // Start is called before the first frame update
    void Start()
    {
       
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (!gameObject.transform.parent.tag.Equals("PiezaNull"))
        {
            RotatePiece();
        }
    
        //ResetFlujoElectrico();
        

    }

    public void ComprobarValuesCheck()      //esto funciona bien
    {
        if (piezaEncima != null)
        {
            if (values[0] == 1 && piezaEncima.values[2] == 1) //¿conecta arriba?
            {
                valuesCheck[0] = true;
                print(this.gameObject.name + " comprobarvalueescheck arriba ");
            }
            else
            {
                valuesCheck[0] = false;
            }
        }
        if (piezaDerecha != null)
        {
            if (values[1] == 1 && piezaDerecha.values[3] == 1) //¿conecta Derecha?
            {
                valuesCheck[1] = true;
                print(this.gameObject.name +  " comprobarvalueescheck Derecha ");
            }
            else
            {
                valuesCheck[1] = false;
            }
        }
        if (piezaDebajo != null)
        {
            if (values[2] == 1 && piezaDebajo.values[0] == 1) //¿conecta Abajo?
            {
                valuesCheck[2] = true;
                print(this.gameObject.name + " comprobarvalueescheck Abajo ");
            }
            else
            {
                valuesCheck[2] = false;
            }
        }
        if (piezaIzquierda != null)
        {
            if (values[3] == 1 && piezaIzquierda.values[1] == 1) //¿conecta Izquierda?
            {
                valuesCheck[3] = true;
                print(this.gameObject.name + " comprobarvalueescheck Izquierda ");
            }
            else
            {
                valuesCheck[3] = false;
            }
        }
    }

    public void SuminElectricidadValuesCheck()              
    {
        if (valuesCheck[0] && llevaElectricidad && piezaEncima.valuesCheck[2])  // doy luz arriba
        {
            print(this.gameObject.name + " da luz arriba");
            piezaEncima.llevaElectricidad = true;
            piezaEncima.ComprobarValuesCheck();
            piezaEncima.ComprobarValoresDePieza();
      
        }
        if (valuesCheck[1] && llevaElectricidad && piezaDerecha.valuesCheck[3])  // doy luz Derecha
        {
            print(this.gameObject.name + " da luz Derecha");
            piezaDerecha.llevaElectricidad = true;
            piezaDerecha.ComprobarValuesCheck();
            piezaDerecha.ComprobarValoresDePieza();
       
        }
        if (valuesCheck[2] && llevaElectricidad && piezaDebajo.valuesCheck[0])  // doy luz Abajo
        {
            print(this.gameObject.name + " da luz Abajo");
            piezaDebajo.llevaElectricidad = true;
            piezaDebajo.ComprobarValuesCheck();
            piezaDebajo.ComprobarValoresDePieza();
    
        }
        if (valuesCheck[3] && llevaElectricidad && piezaIzquierda.valuesCheck[1])  // doy luz Izquierda
        {
            print(this.gameObject.name + " da luz izquierda");
            piezaIzquierda.llevaElectricidad = true;
            piezaIzquierda.ComprobarValuesCheck();
            piezaIzquierda.ComprobarValoresDePieza();
       
        }


        if (valuesCheck[0] && piezaEncima.valuesCheck[2] && piezaEncima.llevaElectricidad)  //recibo luz porque conecto con la de arriba y tiene luz
        {
            print(this.gameObject.name + " Recibe luz de arriba");
            llevaElectricidad = true;
            ComprobarValuesCheck();
            ComprobarValoresDePieza();
   
        }
        if (valuesCheck[1] && piezaDerecha.valuesCheck[3] && piezaDerecha.llevaElectricidad)  //recibo luz porque conecto con la de Derecha y tiene luz
        {
            print(this.gameObject.name + " Recibe luz de Derecha");
            llevaElectricidad = true;
            ComprobarValuesCheck();
            ComprobarValoresDePieza();
         
        }
        if (valuesCheck[2] && piezaDebajo.valuesCheck[0] && piezaDebajo.llevaElectricidad)  //recibo luz porque conecto con la de Abajo y tiene luz
        {
            print(this.gameObject.name + " Recibe luz de Abajo");
            llevaElectricidad = true;
            ComprobarValuesCheck();
            ComprobarValoresDePieza();
      
        }
        if (valuesCheck[3] && piezaIzquierda.valuesCheck[1] && piezaIzquierda.llevaElectricidad)  //recibo luz porque conecto con la de izquierda y tiene luz
        {
            print(this.gameObject.name + " Recibe luz de izquierda");
            llevaElectricidad = true;
            ComprobarValuesCheck();
            ComprobarValoresDePieza();
    
        }

    }
    #region ResetFlujoElectricoYsuministrarElectricidad
    //public void ResetFlujoElectrico()
    //{

    //    print("este objeto es " + this.gameObject.name);
    //    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Piece").Length; i++)
    //    {
    //        if (!GameObject.FindGameObjectsWithTag("Piece")[i].GetComponent<piece>().padreInicioCamino)
    //        {
    //            GameObject.FindGameObjectsWithTag("Piece")[i].GetComponent<piece>().llevaElectricidad = false;                          
    //        }
    //    }
    //    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Piece").Length; i++)
    //    {

    //            if (!GameObject.FindGameObjectsWithTag("Piece")[i].GetComponent<piece>().padreInicioCamino)
    //            {
    //                GameObject.FindGameObjectsWithTag("Piece")[i].GetComponent<piece>().BlinkColliders();


    //            }

    //            if (mismoColor == false)
    //            {
    //                GameObject.FindGameObjectsWithTag("Piece")[i].GetComponent<SpriteRenderer>().color = new Color(0, 73, 250, 255);
    //            }

    //    }
    //    SuministrarElectricidadReset(this.gameObject);

    //}

    //public void SuministrarElectricidadReset(GameObject pieza)  // a rehacer sin colliders
    //{
    //    for (int i = 0; i < pieza.GetComponent<piece>().valuesCheck.Length; i++)
    //    {
    //        print("Comprueba Booleanas de conexion " + this.gameObject.name);

    //        if (pieza.GetComponent<piece>().valuesCheck[0])//conexion con arriba 
    //        {
    //            //if (piezaEncima.GetComponent<piece>().llevaElectricidad)
    //            //{
    //           // esto luego
    //            //}
    //            print("conexion Arriba");
    //            //compruebame el collider "X" y dale al other object electricidad y color
    //            for (int q = 0; q < pieza.transform.parent.childCount; q++)//deberia ser de cualquier gameobject
    //            {
    //                if (pieza.transform.parent.GetChild(q).name.Equals("Arriba")
    //                     && pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos != null
    //                    && pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad == false
    //                   )
    //                {
    //                    print("Entra " + pieza.transform.parent.GetChild(q).name);
    //                    pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad = true;
    //                    if (mismoColor==false)
    //                    {
    //                        pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<SpriteRenderer>().color = new Color(255, 249, 0, 255);
    //                    }
    //                    if (verde == true)
    //                    {
    //                        GameObject.FindGameObjectsWithTag("Piece")[i].GetComponent<SpriteRenderer>().color = new Color(76, 250, 0, 255);
    //                    }
    //                    pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().SuministrarElectricidadReset(pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos);
    //                }
    //            }
    //        }

    //        if (pieza.GetComponent<piece>().valuesCheck[1])//conexion con derecha
    //        {
    //            print("conexion Derecha");
    //            for (int q = 0; q < pieza.transform.parent.childCount; q++)//deberia ser de cualquier gameobject
    //            {
    //                if (pieza.transform.parent.GetChild(q).name.Equals("Derecha")
    //                      && pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos != null
    //                    && pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad == false
    //                  )
    //                {
    //                    print("Entra " + pieza.transform.parent.GetChild(q).name);
    //                    pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad = true;
    //                    if (mismoColor==false)
    //                    {
    //                        pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<SpriteRenderer>().color = new Color(255, 249, 0, 255);
    //                    }
    //                    if (verde == true)
    //                    {
    //                        GameObject.FindGameObjectsWithTag("Piece")[i].GetComponent<SpriteRenderer>().color = new Color(76, 250, 0, 255);
    //                    }
    //                    pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().SuministrarElectricidadReset(pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos);
    //                }
    //            }
    //            //compruebame el collider "X" y dale al other object electricidad y color
    //        }


    //        if (pieza.GetComponent<piece>().valuesCheck[2])//conexion con Abajo
    //        {
    //            for (int q = 0; q < pieza.transform.parent.childCount; q++)//deberia ser de cualquier gameobject
    //            {
    //                print("conexion Abajo");
    //                if (pieza.transform.parent.GetChild(q).name.Equals("Abajo")
    //                    && pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos != null
    //                    && pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad == false
    //                    )
    //                {
    //                    print("Entra " + pieza.transform.parent.GetChild(q).name);
    //                    pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad = true;
    //                    if (mismoColor==false)
    //                    {
    //                        pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<SpriteRenderer>().color = new Color(255, 249, 0, 255);
    //                    }
    //                    if (verde == true)
    //                    {
    //                        GameObject.FindGameObjectsWithTag("Piece")[i].GetComponent<SpriteRenderer>().color = new Color(76, 250, 0, 255);
    //                    }
    //                    pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().SuministrarElectricidadReset(pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos);
    //                }
    //            }
    //            //compruebame el collider "X" y dale al other object electricidad y color
    //        }


    //        if (pieza.GetComponent<piece>().valuesCheck[3])//conexion con Izquierda
    //        {
    //            print("conexion Izquierda");
    //            for (int q = 0; q < pieza.transform.parent.childCount; q++)//deberia ser de cualquier gameobject
    //            {
    //                if (pieza.transform.parent.GetChild(q).name.Equals("Izquierda")
    //                    && pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos != null
    //                    && pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad == false
    //               )
    //                {
    //                    print("Entra " + pieza.transform.parent.GetChild(q).name);
    //                    pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad = true;
    //                    if (mismoColor==false)
    //                    {
    //                        pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<SpriteRenderer>().color = new Color(255, 249, 0, 255);
    //                    }
    //                    if (verde == true)
    //                    {
    //                        GameObject.FindGameObjectsWithTag("Piece")[i].GetComponent<SpriteRenderer>().color = new Color(76, 250, 0, 255);
    //                    }
    //                    pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos.GetComponent<piece>().SuministrarElectricidadReset(pieza.transform.parent.GetChild(q).GetComponent<Colision>().objetoConElQueColisionamos);
    //                }
    //            }
    //compruebame el collider "X" y dale al other object electricidad y color
    //}
    //}
    //}
    #endregion

    public void ComprobarValoresDePieza()
    {    
        if ((valuesCheck[0] && piezaEncima.llevaElectricidad && piezaEncima.valuesCheck[2]) || (valuesCheck[1] && piezaDerecha.llevaElectricidad && piezaDerecha.valuesCheck[3]) || (valuesCheck[2] && piezaDebajo.llevaElectricidad && piezaDebajo.valuesCheck[0]) || (valuesCheck[3] && piezaIzquierda.llevaElectricidad && piezaIzquierda.valuesCheck[1]))
        {
            estaConectado = true;
        }
        if (!valuesCheck[0] && !valuesCheck[1] && !valuesCheck[2] && !valuesCheck[3])
        {
            estaConectado = false;
            llevaElectricidad = false; print("cambiamos color otra vez");
            GetComponent<SpriteRenderer>().color = new Color(0, 73, 250, 255);//NO CONECTADO
        }
        if (!estaConectado)
        {
            print("cambiamos color");
            //if (mismoColor==false)
            //{
                GetComponent<SpriteRenderer>().color = new Color(0, 73, 250, 255);//NO CONECTADO
            //}     
            if (!gameObject.transform.parent.gameObject.tag.Equals("InicioPieza"))
            {
                llevaElectricidad = false;
            }         
        }
        else if(estaConectado)
        {
            print(this.gameObject.name + " EstaConectado y se enciende");
          GetComponent<SpriteRenderer>().color = new Color(255, 249, 0, 255); // CONECTADO
        }
        //Invoke("ResetFlujoElectrico", 0.3f); esto lo hace petar     
    }

    public void RotatePiece()
    {
        if (noMovible==false)
        {
            realRotation += 90;
            if (Input.GetMouseButtonDown(0))
            {
                transform.rotation = Quaternion.Euler(0, 0, realRotation);
                RotateValues();
                //for (int i = 0; i < FindObjectsOfType<piece>().Length; i++)
                //{
                //    if (FindObjectsOfType<piece>()[i].transform.parent.gameObject.tag.Equals("PiezaNull"))
                //    {
                //        FindObjectsOfType<piece>()[i].ComprobarValuesCheck();
                //    }
                //}
                //FindObjectOfType<ManagerPuzlePiezas>().DarElectricidadDesdeInicio();  
               
                //Invoke("BlinkColliders", 0.3f);  //ya no hay blink
            }
        
        }
        }

        #region blinkYvolverdeBlink
        //public void BlinkColliders()
        //{
        //    transform.parent.GetChild(1).gameObject.SetActive(false);
        //    transform.parent.GetChild(2).gameObject.SetActive(false);
        //    transform.parent.GetChild(3).gameObject.SetActive(false);
        //    transform.parent.GetChild(4).gameObject.SetActive(false);
        //    Invoke("volverdeBlink", 0.3f);
        //}

        //public void volverdeBlink()
        //{
        //    transform.parent.GetChild(1).gameObject.SetActive(true);
        //    transform.parent.GetChild(2).gameObject.SetActive(true);
        //    transform.parent.GetChild(3).gameObject.SetActive(true);
        //    transform.parent.GetChild(4).gameObject.SetActive(true);
        //    Invoke("ComprobarValoresDePieza", 0.3f);
        //}
        #endregion

        public void RotateValues()
    {

        int aux = values[0];
        for (int i = 0; i < values.Length-1; i++)
        {
            values[i] = values[i + 1];
        }values[3] = aux;
    }
    

    

}



