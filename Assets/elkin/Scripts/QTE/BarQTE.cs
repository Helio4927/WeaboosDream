using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarQTE : MonoBehaviour, ISimpleQTE
{
    public float percentageGreen = 40f;
    public float percentageYellow = 30f;
    public float percentageRed = 30f;

    public SpriteRenderer sprGreen;
    public SpriteRenderer sprYellow;
    public SpriteRenderer sprRed;

    [SerializeField]
    private string _name;

    [SerializeField]
    private SpriteRenderer _indicator;
    [SerializeField]
    private Transform _leftLimitIndicator;
    [SerializeField]
    private Transform _rightLimitIindicator;

    [SerializeField]
    private bool _isStarted = false;

    private Action<bool, int> _action;
    private Action<int> _onUpdate;

    public float duration;
    public float threshold = 0.2f;
    public float increment = 0.2f;
    public float decrement = 0.1f;
    private float total;
    private float amount;
    private float counterThreshold;
    private float time;
    private Animator anim;

    private int _damageType = 0;
    private const float MAX_WIDTH_SPRITE = 24f;

    void Awake()
    {
        var scale = sprGreen.transform.localScale;

        sprGreen.transform.localScale = new Vector3(MAX_WIDTH_SPRITE * percentageGreen / 100, scale.y, scale.z);
        sprYellow.transform.localScale = new Vector3(MAX_WIDTH_SPRITE * percentageYellow / 100, scale.y, scale.z);
        sprRed.transform.localScale = new Vector3(MAX_WIDTH_SPRITE * percentageRed / 100, scale.y, scale.z);

        Debug.Log("sprGreen: " + sprGreen.transform.localScale);
        Debug.Log("sprYellow: " + sprYellow.transform.localScale);
        Debug.Log("sprRed: " + sprRed.transform.localScale);

        var greenPos = sprGreen.transform.localPosition;
        var yellowPos = sprYellow.transform.localPosition;
        var redPos = sprRed.transform.localPosition;

        sprYellow.transform.localPosition = new Vector3(greenPos.x + sprGreen.bounds.size.x, yellowPos.y, yellowPos.z);
        yellowPos = sprYellow.transform.localPosition;

        sprRed.transform.localPosition = new Vector3(yellowPos.x + sprYellow.bounds.size.x, redPos.y, redPos.z);        

        Debug.Log("Awake: "+sprGreen.bounds.size);
        Debug.Log("Awake: " + sprYellow.bounds.size);
        Debug.Log("Awake: " + sprRed.bounds.size);

        anim = GetComponent<Animator>();
        anim.SetBool("play", false);
        total = _rightLimitIindicator.localPosition.x;

        gameObject.SetActive(false);
        
    }

    public void Init(Action<bool, int> action, Vector3 qtePos, Action<int> onUpdate)
    {
        _onUpdate = onUpdate;
        transform.position = new Vector2(qtePos.x, qtePos.y);
        Debug.Log("QTE.Init");
        _action = action;
        gameObject.SetActive(true);
        _isStarted = true;
        anim.SetBool("play", true);
        amount = _leftLimitIndicator.localPosition.x;
        time = 0;
    }

    private void Test(bool result, int val)
    {
        Debug.Log($"Test {val}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Init(Test, Vector3.zero, null);
        }

        if (!_isStarted) return;

        time += Time.deltaTime;
        
        if (time > duration)
        {
            Debug.Log("Final");
            GoodFinish();
            //BadFinish();
            return;
        }
        

        var pos = _indicator.transform.localPosition;

        if (Input.GetMouseButtonDown(0))
        {
            amount += increment;
        }

        counterThreshold += Time.deltaTime;
        if (counterThreshold >= threshold)
        {
            counterThreshold = 0;
            amount -= decrement;
        }

        amount = Mathf.Clamp(amount, _leftLimitIndicator.localPosition.x, total);
        pos.x = amount;
        _indicator.transform.localPosition = pos;
        /*
        if (amount >= total)
        {
            GoodFinish();
        }
        */

        if (_indicator.transform.localPosition.x > sprRed.transform.localPosition.x)
        {
            _damageType = 2;
        }
        else if (_indicator.transform.localPosition.x > sprYellow.transform.localPosition.x)
        {
            _damageType = 1;
        }
        else
        {
            _damageType = 0;
        }

        _onUpdate?.Invoke(_damageType);
        Debug.Log("DamageType "+_damageType);
    }

    private void BadFinish()
    {
        Debug.Log("FinishingFase ");
        _action.Invoke(false, _damageType);
        anim.SetBool("play", false);
        gameObject.SetActive(false);
        _isStarted = false;
    }

    private void GoodFinish()
    {
        Debug.Log("FinishingFase ");
        _action.Invoke(true, _damageType);
        anim.SetBool("play", false);
        gameObject.SetActive(false);
        _isStarted = false;
    }

    public string Name => name;
    public int DamageType => _damageType;
}
