using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auxiliarCamaraEvents : MonoBehaviour {

    // Use this for initialization
    public Animator animPesadillas;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RestaurarTiempo()
    {
        Time.timeScale = 1;
    }
    public void desapareceCursor()
    {
        FindObjectOfType<CursorManager>().eliminaTodoCursor();
    }
    public void ApareceCursor()
    {
        FindObjectOfType<CursorManager>().reactivarCursor();
    }
    public void camera2DClampControlEliminar()
    {
        Invoke("camera2DClampEliminarVerdadero", 0.5f);
    }
    public void camera2DClampEliminarVerdadero()
    {
        GetComponent<Camera2DClampControl>().enabled = false;
    }

    public void camera2DClampControlRestaurar()
    {
        GetComponent<Camera2DClampControl>().enabled = true;
    }

    public void CambiarPanelOpen()
    {
        FindObjectOfType<GameManager>()._panelOpen = !FindObjectOfType<GameManager>()._panelOpen;

    }

    public void ApagarAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }

    public void EjecutarAnimacionPesadillas()
    {
        //GameObject.Find("PadrePanelpesadillas").GetComponent<Animator>().enabled = true;
        GameObject.Find("PadrePanelpesadillas").GetComponent<Animator>().enabled = true;
        animPesadillas.Play("pesadillacamara");
    }

    public void Auxllamarfadeout()
    {
        FindObjectOfType<AuxPanelFade>().PanelFadeOUT();
    }

    public void CallAnimEscenarioMuerteSplash()
    {

        GameObject.Find("EscenarioSplashMuerte").GetComponent<Animator>().Play("GoUpFallSprite");
    }
  
    
}
