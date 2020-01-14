using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour {

    public Sprite normalTexture;
    //public Sprite cabezaTexture;
    //public Sprite torsoTexture;
    //public Sprite piernaTexture;
    private Vector2 cursorHotspot;
    public CursorMode cursorMode = CursorMode.Auto;
    public Sprite ojoCursor;
    public Sprite puntoRojoTexture;
    public Sprite romboTexture;
    public Sprite monedaTexture;
    public Sprite curryTexture;
    public Texture2D MouseTexture;
    public Camera camaraShake;
    public GameObject cursorSpriteWorld;

    //public Vector2 hotSpot = Vector2.zero;

    // initialize mouse with a new texture with the
    // hotspot set to the middle of the texture
    // (don't forget to set the texture in the inspector
    // in the editor)

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;
        if (currentScene.name == "Escena Principal")
        {
            Cursor.visible = false;
        }
     
        //cursorHotspot = new Vector2(normalTexture.width / 2, normalTexture.height / 2);
        //Cursor.SetCursor(normalTexture, cursorHotspot, cursorMode);
    }


    public void CursorMouseOn(bool valor)
    {
        if (!FindObjectOfType<GameManager>().usandoCurry && !FindObjectOfType<GameManager>().usandoRombo && !FindObjectOfType<GameManager>().usandoMoneda)
        {
            Cursor.SetCursor(MouseTexture, cursorHotspot, cursorMode);
        }
  
        Cursor.visible = valor;
        cursorSpriteWorld.SetActive(!valor);

    }
    public void eliminaTodoCursor()
    {
        Cursor.visible = false;
        cursorSpriteWorld.SetActive(false);
    }
    public void reactivarCursor()
    {     
            cursorSpriteWorld.SetActive(true);     
    }
    public void QuitarTodasLasVariablesDeUsar()
    {
        FindObjectOfType<GameManager>().usandoMoneda = false;
        FindObjectOfType<GameManager>().usandoRombo = false;
        FindObjectOfType<GameManager>().usandoCurry = false;
        SetCurryTextura(false);
    }
    void Update()
    {
        Ray ray;
        if (Camera.main != null)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        else
        {
            ray = camaraShake.ScreenPointToRay(Input.mousePosition);
        }

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);


        //do the raycast specifying the mask
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (hit.collider != null)
        {
            //print("<color=purple>" + hit.collider.gameObject + "  </color>");     //COMENTADO POR EL SNAP DE CURSOR
            //if (hit.collider.gameObject.name.Equals("cabeza"))
            //{
            //    SetTextura(cabezaTexture);
            //}
            //else if (hit.collider.gameObject.name.Equals("torso"))
            //{
            //    SetTextura(torsoTexture);
            //}
            //else if (hit.collider.gameObject.name.Equals("pierna"))
            //{
            //    SetTextura(piernaTexture);
            //}
            //else 
            if (hit.collider.gameObject.tag.Equals("AreaExam") || hit.collider.gameObject.tag.Equals("WorldItem"))
            {
                if (FindObjectOfType<GameManager>().usandoMoneda || FindObjectOfType<GameManager>().usandoRombo || FindObjectOfType<GameManager>().usandoCurry)          
                {
                 // no where to run
                }
                else
                {
                    SetTextura(ojoCursor);
                }   
           
            }
        }
        else
        {
            if (FindObjectOfType<GameManager>().usandoMoneda)
            {
                SetMonedaTextura(true);
            }
           else if (FindObjectOfType<GameManager>().usandoRombo)
            {
                SetRomboTextura(true);
            }
            else if (FindObjectOfType<GameManager>().usandoCurry)
            {
                SetCurryTextura(true);
            }
            else
            {
                SetTextura(normalTexture);
            }         
        }       
                    
    }
    
    public void SetTextura(Sprite newsprite)
    {
        if (!SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            cursorSpriteWorld.GetComponent<SpriteRenderer>().sprite = newsprite;
        }  
        //Cursor.SetCursor(texture, cursorHotspot, cursorMode);
    }

    public void SetPuntoRojoTextura(bool ponerRojo)
    {
        if (ponerRojo)
        {
            //Cursor.SetCursor(puntoRojoTexture, cursorHotspot, cursorMode);
            cursorSpriteWorld.GetComponent<SpriteRenderer>().sprite = puntoRojoTexture;
        }
        else
        {
            cursorSpriteWorld.GetComponent<SpriteRenderer>().sprite = normalTexture;
            //Cursor.SetCursor(normalTexture, cursorHotspot, cursorMode);
        } 
    }

    public void SetRomboTextura(bool ponerRombo)
    {
        if (ponerRombo)
        {
            CursorMouseOn(false);
            cursorSpriteWorld.GetComponent<SpriteRenderer>().sprite = romboTexture;

            //Cursor.SetCursor(romboTexture, cursorHotspot, cursorMode);
        }
        else
        {
            //CursorMouseOn(false);
            cursorSpriteWorld.GetComponent<SpriteRenderer>().sprite = normalTexture;
            //Cursor.SetCursor(normalTexture, cursorHotspot, cursorMode);
        }
    }
    public void setNormalTexture()
    {
        print("texturanormal puesta");
        cursorSpriteWorld.GetComponent<SpriteRenderer>().sprite = normalTexture;
    }
    public void SetMonedaTextura(bool ponerMoneda)
    {
        if (ponerMoneda)
        {
            CursorMouseOn(false);
            cursorSpriteWorld.GetComponent<SpriteRenderer>().sprite = monedaTexture;
            //Cursor.SetCursor(monedaTexture, cursorHotspot, cursorMode);
        }
        else
        {
            //CursorMouseOn(false);
            cursorSpriteWorld.GetComponent<SpriteRenderer>().sprite = normalTexture;
            //Cursor.SetCursor(normalTexture, cursorHotspot, cursorMode);
        }
    }
    public void SetCurryTextura(bool ponerCurry)
    {
        if (ponerCurry)
        {
            CursorMouseOn(false);
            cursorSpriteWorld.GetComponent<SpriteRenderer>().sprite = curryTexture;
            //Cursor.SetCursor(curryTexture, cursorHotspot, cursorMode);
        }
        else
        {
            //CursorMouseOn(false);
            cursorSpriteWorld.GetComponent<SpriteRenderer>().sprite = normalTexture;
            //Cursor.SetCursor(normalTexture, cursorHotspot, cursorMode);
        }
    }
}
