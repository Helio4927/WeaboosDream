using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour {

    public GameObject panelSettings;
    public Animator animPanelFade;

	// Use this for initialization
	void Start () {
        FindObjectOfType<GameManager>().enMenuPrincipal = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGame()
    {
        CancelInvoke();
        animPanelFade.Play("FadeStartGame");
    }


    public void DisplaySettings()
    {
        //  panelSettings.SetActive(true);
        Debug.Log("Abre Settings");
        //La verdad es que en el botón de exitSetting tendría que llamar a la vida del PinkHorse y ponerlo en 3 de nuevo.


    }
    
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Nos vemos");
    }

    public void CallStartSpa()
    {
        FindObjectOfType<AuxPanelFade>().sceneToLoad = 3;
        Invoke("StartGame", 1f);
    }
    public void CallStartEng()
    {
        FindObjectOfType<AuxPanelFade>().sceneToLoad = 4;
        Invoke("StartGame", 1f);
    }
    public void CallMenuSpa()
    {
        FindObjectOfType<AuxPanelFade>().sceneToLoad = 1;
        Invoke("StartGame", 1f);
    }
    public void CallMenuEng()
    {
        FindObjectOfType<AuxPanelFade>().sceneToLoad = 2;
        Invoke("StartGame", 1f);
    }

    public void CallSettings()
    {
        Invoke("DisplaySettings", 1f);
    }

    public void CallExit()
    {
        this.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        Invoke("ExitGame", 1f);
    }
}
