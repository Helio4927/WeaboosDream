using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleQTE : MonoBehaviour
{

    [SerializeField]
    private string _name;

    [SerializeField]
    private SpriteRenderer _bar;

    [SerializeField]
    private bool _isStarted = false;

    private Action<bool> _action;        

    public float duration;
    public float threshold = 0.2f;
    public float increment = 0.2f;
    public float decrement = 0.1f;
    private float total;
    private float amount;
    private float counterThreshold;
    private float time;
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("play", false);
        total = _bar.transform.localScale.x;
        gameObject.SetActive(false);
    }
    public void Init(Action<bool> action, Vector3 qtePos)
    {
        transform.position = new Vector2(qtePos.x, qtePos.y);
        Debug.Log("QTE.Init");
        _action = action;               
        gameObject.SetActive(true);        
        _isStarted = true;
        anim.SetBool("play", true);
        amount = 0;
        time = 0;
    }
        
    void Update()
    {       

        if (!_isStarted) return;

        time += Time.deltaTime;
        if (time > duration)
        {
            Debug.Log("Final");
            BadFinish();
            return;
        }

        
        var scale = _bar.transform.localScale;

        if (Input.GetMouseButtonDown(0))
        {
            amount += increment;            
        }

        counterThreshold += Time.deltaTime;
        if(counterThreshold >= threshold)
        {
            counterThreshold = 0;
            amount -= decrement;
        }

        amount = Mathf.Clamp(amount, 0, total);
        scale.x = amount;
        _bar.transform.localScale = scale;

        if (amount >= total)
        {
            GoodFinish();
        }
    } 

    private void BadFinish()
    {
        Debug.Log("FinishingFase ");
        _action.Invoke(false);
        anim.SetBool("play", false);
        gameObject.SetActive(false);
        _isStarted = false;        
    }

    private void GoodFinish()
    {
        Debug.Log("FinishingFase ");
        _action.Invoke(true);
        anim.SetBool("play", false);
        gameObject.SetActive(false);
        _isStarted = false;
    }
}
