using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE : MonoBehaviour
{
    public int amountAttempts = 2;
    public int amountClicks = 2;
    public float intervalTime = 3f;
    public float duration = 2f;

    private Action _action;
    private float currentTime;
    private float tickQteTime;
    private State _currentState;
    private int _clickCounter = 0;
    private int _currentAttempts = 0;
    enum State {
        WAITING, DOING, FINISHING
    }

    public void Init(Action action)
    {
        Debug.Log("QTE.Init");
        _action = action;
        _currentState = State.WAITING;
        _clickCounter = 0;
        _currentAttempts = 0;
    }
        
    void Update()
    {
        ProccessState();        
    }

    void ProccessState()
    {
        switch(_currentState)
        {
            case State.WAITING:
                WaitingFase();
                break;

            case State.DOING:
                DoingFase();
                break;

            case State.FINISHING:
                FinishingFase();
                break;
        }
    }

    private void DoingFase()
    {        
        tickQteTime += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            _clickCounter++;
        }

        if (tickQteTime > duration)
        {
            _currentAttempts++;
            if(_currentAttempts >= amountAttempts)
            {
                _currentState = State.FINISHING;
            }
            else
            {
                _currentState = State.WAITING;
            }
            
        }        
    }

    private void WaitingFase()
    {       
        currentTime += Time.deltaTime;

        if (currentTime > intervalTime)
        {
            _currentState = State.DOING;
        }        
    }

    private void FinishingFase()
    {
        _action.Invoke();           
    }
}
