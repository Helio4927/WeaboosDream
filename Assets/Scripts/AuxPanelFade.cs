using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuxPanelFade : MonoBehaviour {
    public GameObject panelFinal;
    public Animator panelfade;
    public int sceneToLoad;
	// Use this for initialization
	void Start () {
		
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            finalDeJuego();
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name.Equals("TriggerFinal"))
        {
            finalDeJuego();
        }
    }

    public void finalDeJuego()
    {
        panelfade.Play("FadeOutFinal");

    }
    public void PanelFinal()
    {
        panelFinal.SetActive(true);
        FindObjectOfType<GameManager>().UpdateCronometro();
      
    }

    public void PanelFadeIN()
    {
        panelfade.Play("FadeIn");
    }

    public void PanelFadeCarne()
    {
        panelfade.Play("FadeCarne");
    }

    public void CambioCarne()
    {
        FindObjectOfType<GameManager>().CambioPaquetesCarne();
        GameObject.Find("ObstaculoParedCarne").SetActive(false);
    }

    public void PanelFadeOUT()
    {
        panelfade.Play("FadeOut");
    }
    public void CallFade()
    {
        Invoke("PanelFadeIN", 1f);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void StartGameSpa()
    {
        SceneManager.LoadScene(3);
    }

    public void StartGameEng()
    {
        SceneManager.LoadScene(4);
    }

    public void MenuEng()
    {
        SceneManager.LoadScene(2);
    }

    public void MenuSpa()
    {
        SceneManager.LoadScene(1);
    }

    public void camarallamarAnimacionSplash()
    {
          GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().enabled = true;
        FindObjectOfType<GameManager>().caidaWeaboos.SetActive(false);

          GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>().Play("SplashCaidaWeaboo");
        print(  GameObject.FindGameObjectWithTag("MainCamera").name + " anim camara");
    }

    public void PanelOpenTrue()
    {
        FindObjectOfType<GameManager>()._panelOpen = true;
    }
    public void PanelOpenFalse()
    {
        FindObjectOfType<GameManager>()._panelOpen = false;
    }

    public void SonidoDisolver()
    {
        FindObjectOfType<SoundManager>().PlaySound(0);
    }
}
