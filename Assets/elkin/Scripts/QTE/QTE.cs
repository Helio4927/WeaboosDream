using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE : MonoBehaviour
{
    

    [Header("Tiempo de espera antes de iniciar el QTE")]
    public float delayToStartSeconds = 3;

    [Header("Tiempo para mantener la señal del QTE")]
    public float showingSeconds = 2;

    [Header("Tiempo para recibir clicks")]
    public float clickingSeconds = 4;

    [Header("Cantidad de veces que se realiza el QTE")]
    public int amountAttempts = 2;

    [Header("Cantidad de clicks esperados por intento")]
    public int amountClicks = 2;

    [Header("Tiempo entre intentos")]
    public float intervalBetweenAttempt = 3f;   
    
    [Header("Imagen que se muestra para avisar que esta apunto de iniciar el QTE")]
    public Sprite imgNormal;

    [Header("Imagen que se muestra en el momento que se reciben clicks")]
    public Sprite imgPress;

    [SerializeField]
    private SpriteRenderer _bar;

    [SerializeField]
    private State _currentState = State.NONE;
        
    private Action _action;     
    private int _clickCounter = 0;
    private float _counterFaseSeconds;
    private int _currentAttempts = 0;
    private SpriteRenderer _spriteRend;

    public float threshold = 0.2f;
    public float increment = 0.2f;
    public float decrement = 0.1f;
    private float total;
    private float amount;
    private float counterThreshold;
    
    enum State {
        NONE, WAITING, SHOWING, CLICKING, HIDING, FINISHING
    }

    private void Awake()
    {
        total = _bar.transform.localScale.x;
        //gameObject.SetActive(false);
        _spriteRend = GetComponent<SpriteRenderer>();
    }
    public void Init(Action action)
    {
        Debug.Log("QTE.Init");
        _action = action;
        _currentState = State.WAITING;
        _clickCounter = 0;
        _currentAttempts = 0;
        gameObject.SetActive(true);
        _counterFaseSeconds = 0;
    }
        
    void Update()
    {
        _counterFaseSeconds += Time.deltaTime;
        var scale = _bar.transform.localScale;

        if (Input.GetMouseButtonDown(0))
        {
            amount += increment;            
                     
            Debug.Log("Amount: "+amount);
        }

        counterThreshold += Time.deltaTime;
        if(counterThreshold >= threshold)
        {
            counterThreshold = 0;
            amount -= decrement;
        }
        //ProccessState();  

        if (amount >= total) amount = total;
        if (amount <= 0) amount = 0;

        scale.x = amount;
        _bar.transform.localScale = scale;
    }

    void ProccessState()
    {
        switch(_currentState)
        {
            case State.NONE:
                break;

            case State.WAITING:
                WaitingFase();
                break;

            case State.SHOWING:
                ShowingFase();
                break;

            case State.CLICKING:
                ClickingFase();
                break;

            case State.HIDING:
                HidingFase();
                break;           

            case State.FINISHING:
                FinishingFase();
                break;
        }
    } 

    private void WaitingFase()
    { 
        if (_counterFaseSeconds > delayToStartSeconds)
        {
            _currentState = State.SHOWING;
            _counterFaseSeconds = 0;
        }        
    }

    private void ShowingFase()
    {       
        _spriteRend.sprite = imgNormal;
        if (_counterFaseSeconds >= showingSeconds)
        {
            _counterFaseSeconds = 0;
            _currentState = State.CLICKING;
        }        
    }

    private void ClickingFase()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _clickCounter++;
        } 
      
        _spriteRend.sprite = imgPress;
        if (_counterFaseSeconds >= clickingSeconds)
        {
            _currentAttempts++;
            _counterFaseSeconds = 0;           
            _currentState = _currentAttempts >= amountAttempts ? State.FINISHING : State.HIDING; 
        }       
    }

    private void HidingFase()
    {
        _spriteRend.sprite = null;
      
        if (_counterFaseSeconds>=intervalBetweenAttempt)
        {
            _counterFaseSeconds = 0;
            _currentState = State.SHOWING;
        }
    }

    private void FinishingFase()
    {
        Debug.Log("FinishingFase "+ _clickCounter);
        _action.Invoke();
        _currentState = State.NONE;
        _counterFaseSeconds = 0;
        gameObject.SetActive(false);
    }
}
