using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
   
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    public bool Shaking;
    public Camera _cam;
    //public Camera2DClampControl _camClamp;
    public SpriteRenderer currentBackground;

    //si este script siempre va en la camara podria agregarse un require
    void Awake()
    {
        camTransform = transform;
        //_cam = GetComponent<Camera>();        
        //_camClamp = GetComponent<Camera2DClampControl>();
    }


    public void ShakeON()
    {
        originalPos = transform.position;
        //for (int i = 0; i < FindObjectOfType<CambioDeZona>().escenarios[FindObjectOfType<GameManager>().numeroEscenarioActual].transform.childCount; i++)
        //{
        //    if (FindObjectOfType<CambioDeZona>().escenarios[FindObjectOfType<GameManager>().numeroEscenarioActual].transform.GetChild(i).gameObject.tag.Equals("FondoClamp"))
        //    {
        //        _camClamp.background = FindObjectOfType<CambioDeZona>().escenarios[FindObjectOfType<GameManager>().numeroEscenarioActual].transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
        //        _camClamp.enabled = true;
        //        enabled = true;
        //    }
        //}
        enabled = true;
        Shaking = true;

        camTransform = transform.parent.transform;
        originalPos = camTransform.localPosition;
    }
    public void ShakeHit()
    {
        shakeDuration = 0.2f;
        shakeAmount = 0.1f;
        decreaseFactor = 1f;

        Shaking = true;
   
        transform.parent.GetComponent<Animator>().enabled = false;
        originalPos = camTransform.localPosition;
        GetComponent<Camera>().orthographicSize = transform.parent.GetComponent<Camera>().orthographicSize;
        transform.parent.GetComponent<Camera>().enabled = false;
        GetComponent<Camera>().enabled = true;
    }

    public void ShakeOFF()
    {
        shakeDuration = 0f;
        camTransform.localPosition = originalPos;
        Shaking = false; 
    }

    void Update()
    {
        if (shakeDuration > 0 && Shaking)
        {  
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
         //   transform.position = originalPos;  //duda
            Shaking = false;
            transform.parent.GetComponent<Camera>().enabled = true;
            GetComponent<Camera>().enabled = false;

        }
    }
}