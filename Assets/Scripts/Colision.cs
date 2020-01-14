using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour
{
    private string nombreColisionador;
    // Start is called before the first frame update
    public GameObject objetoConElQueColisionamos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void OnTriggerEnter2D(Collider2D collision)//que solo ocurra si con lo que conectas tiene electricidad
    {

        if (collision.gameObject.tag == "Piece" && collision.gameObject.transform.parent != gameObject.transform.parent)
        {
           
            objetoConElQueColisionamos = collision.gameObject;
            print("vengo de la pieza "+ transform.parent.GetChild(0).gameObject.name);
            nombreColisionador = gameObject.name;
         
            switch (gameObject.name)
            {
                case "Arriba":
                    if (transform.parent.GetChild(0).GetComponent<piece>().values[0] == 1) // si la pieza donde estoy conecta hacia arriba
                    {
                        if (objetoConElQueColisionamos.GetComponent<piece>().values[2] == 1 && objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad)
                        {
                            print(transform.parent.GetChild(0).gameObject.name + " conecta con " + collision.gameObject.name + " por arriba-Abajo");
                            FeedBackConexion();
                            transform.parent.GetChild(0).gameObject.GetComponent<piece>().valuesCheck[0] = true;
                            objetoConElQueColisionamos.GetComponent<piece>().valuesCheck[2] = true;
                            if (transform.parent.GetChild(0).GetComponent<piece>().llevaElectricidad)
                            {
                                objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad = true;
                            }
                            if (objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad)
                            {
                                transform.parent.GetChild(0).GetComponent<piece>().llevaElectricidad = true;
                            }
                        }                 
                    }
                    else
                    {
                        print("colision arriba no tiene conexion");
                        transform.parent.GetChild(0).gameObject.GetComponent<piece>().valuesCheck[0] = false;
                        objetoConElQueColisionamos.GetComponent<piece>().valuesCheck[2] = false;
                        objetoConElQueColisionamos.GetComponent<piece>().ComprobarValoresDePieza();
                     
                    }
                    break;

                case "Derecha":
                    if (transform.parent.GetChild(0).GetComponent<piece>().values[1] == 1) // si la pieza donde estoy conecta hacia la derecha
                    {
                        print("entra en derecha");
                        print(objetoConElQueColisionamos.GetComponent<piece>().values[3]+  " " + transform.parent.GetChild(0).GetComponent<piece>().values[1]);
                        if (collision.GetComponent<piece>().values[3] == 1 && objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad)
                        {
                            print(transform.parent.GetChild(0).gameObject.name + " conecta con " + collision.gameObject.name + " por Derecha-izquierda");
                            FeedBackConexion();
                            transform.parent.GetChild(0).gameObject.GetComponent<piece>().valuesCheck[1] = true;
                            objetoConElQueColisionamos.GetComponent<piece>().valuesCheck[3] = true;
                            if (transform.parent.GetChild(0).GetComponent<piece>().llevaElectricidad)
                            {
                                objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad = true;
                            }
                            if (objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad)
                            {
                                transform.parent.GetChild(0).GetComponent<piece>().llevaElectricidad = true;
                            }
                        }
                    }
                    else 
                    {
                        print("colision derecha no tiene conexion");
                        transform.parent.GetChild(0).gameObject.GetComponent<piece>().valuesCheck[1] = false;
                        objetoConElQueColisionamos.GetComponent<piece>().valuesCheck[3] = false;
                        objetoConElQueColisionamos.GetComponent<piece>().ComprobarValoresDePieza();
                    }

                    break;
                case "Abajo":
                    if (transform.parent.GetChild(0).GetComponent<piece>().values[2] == 1) // si la pieza donde estoy conecta hacia abajo
                    {
                        if (collision.GetComponent<piece>().values[0] == 1 && objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad)
                        {
                            print(transform.parent.GetChild(0).gameObject.name + " conecta con " + collision.gameObject.name + " por Abajo-Arriba");
                            FeedBackConexion();
                            transform.parent.GetChild(0).gameObject.GetComponent<piece>().valuesCheck[2] = true;
                            objetoConElQueColisionamos.GetComponent<piece>().valuesCheck[0] = true;
                            if (transform.parent.GetChild(0).GetComponent<piece>().llevaElectricidad)
                            {
                                objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad = true;
                            }
                            if (objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad)
                            {
                                transform.parent.GetChild(0).GetComponent<piece>().llevaElectricidad = true;
                            }
                        }                 
                    }
                    else
                    {
                        print("colision abajo no tiene conexion");
                        transform.parent.GetChild(0).gameObject.GetComponent<piece>().valuesCheck[2] = false;
                        objetoConElQueColisionamos.GetComponent<piece>().valuesCheck[0] = true;
                        objetoConElQueColisionamos.GetComponent<piece>().ComprobarValoresDePieza();
                    }
                    break;

                case "Izquierda":
                    if (transform.parent.GetChild(0).GetComponent<piece>().values[3] == 1) // si la pieza donde estoy conecta hacia la izquierda
                    {
                        if (collision.GetComponent<piece>().values[1] == 1 && objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad)
                        {
                            print(transform.parent.GetChild(0).gameObject.name + " conecta con " + collision.gameObject.name + " por Izquierda-Derecha");
                            FeedBackConexion();
                            transform.parent.GetChild(0).gameObject.GetComponent<piece>().valuesCheck[3] = true;
                            objetoConElQueColisionamos.GetComponent<piece>().valuesCheck[1] = true;
                            if (transform.parent.GetChild(0).GetComponent<piece>().llevaElectricidad)
                            {
                                objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad = true;
                            }
                            if (objetoConElQueColisionamos.GetComponent<piece>().llevaElectricidad)
                            {
                                transform.parent.GetChild(0).GetComponent<piece>().llevaElectricidad = true;
                            }
                        }                    
                    }
                    else
                    {
                        print("colision izquierda no tiene conexion");
                        transform.parent.GetChild(0).gameObject.GetComponent<piece>().valuesCheck[3] = false;
                        objetoConElQueColisionamos.GetComponent<piece>().valuesCheck[1] = false;
                        objetoConElQueColisionamos.GetComponent<piece>().ComprobarValoresDePieza();
                    }
                    break;

            }
        }
    }

    public void FeedBackConexion()//PROVISIONAL HAY QUE REHACERLO CON EL ARTE 
    {    
            objetoConElQueColisionamos.GetComponent<SpriteRenderer>().color = new Color(255,249,0,255);
            transform.parent.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 249, 0, 255);
        
    }
}
