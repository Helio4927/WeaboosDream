using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3D_Elkin : AnimEvents
{
    private LifeBar _lifeBar;
    private Animator _anim;
    private Player _player;
    public float maxDistance = 5;
    public float minDistance = 1;
    public float speed = 0.05f;
    private bool _isTarget = false;
    public float stuneTime = 0.5f;
    private bool _isInvincible = false;
    private State _currentState;
    private bool _isRigth = false;
    private bool _isAlive = true;
    public float _distanceWhenPlayerHit = 2;
    public float _timeToDissapear = 1;
    public Transform parts;
    private Rigidbody2D _rigid;

    public enum State
    {
        IDLE, FOLLOW, ATTACK, WAIT, HURT, DEATH
    }
    void Start()
    {
        _lifeBar = GetComponent<LifeBar>();
        _anim = GetComponentInChildren<Animator>();
        _player = FindObjectOfType<Player>();
        _rigid = GetComponentInChildren<Rigidbody2D>();
        _rigid.transform.parent = null;

        _currentState = State.IDLE;
        //_player.AddEnemy(this);
    }

    public void ShowHitAnim(string animName)
    {
        Debug.Log("Enemy Play: " + animName);
        if (CanSetNextState(_currentState, State.HURT))
        {
            _isAlive = _lifeBar.UpdateHp(1);
            parts.gameObject.SetActive(_isAlive);
            CancelInvoke("RemoveTarget");
            _isTarget = true;
            _anim.Play(animName, 0, 0);
            _rigid.velocity = Vector2.zero;
        }
    }

    public void DoInvincible()
    {
        _isInvincible = true;
    }

    public void FarAway()
    {
        var dir = _isRigth ? Vector3.left : Vector3.right;
        _rigid.bodyType = RigidbodyType2D.Dynamic;
        _rigid.AddForce(dir * _distanceWhenPlayerHit, ForceMode2D.Impulse);
        Invoke("StopFarAway", 0.1f);
        //transform.position +=  dir * _distanceWhenPlayerHit; 
        Debug.Log("FarAway");
    }

    private void StopFarAway()
    {
        _rigid.velocity = Vector3.zero;
        _rigid.bodyType = RigidbodyType2D.Kinematic;
    }

    public void ResetEnemyTarget()
    {
        if (_isTarget)
        {
            Invoke("RemoveTarget", stuneTime);
        }
        Debug.Log("ResetEnemyTarget");
    }

    private void RemoveTarget()
    {
        _isTarget = false;
        CanSetNextState(_currentState, State.IDLE);
        Debug.Log("RemoveTarget");
    }

    private void TestFarEnemyAway()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            FarAway();
        }
    }

    void Update()
    {
        TestFarEnemyAway();

        SetFlip(transform.position.x < _player.transform.position.x);

        _anim.GetComponent<SpriteRenderer>().sortingOrder = transform.position.y < _player.transform.position.y ? 3 : 1;

        var distance = Vector2.Distance(_player.transform.position, transform.position);

        if (!_isTarget)
        {
            if (distance < maxDistance && distance > minDistance)
            {
                if (CanSetNextState(_currentState, State.FOLLOW))
                {
                    //_player.AddEnemy(this);
                }
            }
            else if (distance <= minDistance)
            {
                if (CanSetNextState(_currentState, State.WAIT))
                {
                    Debug.Log("Current: " + _currentState);
                    ProccessOnceState();
                }
            }
            else
            {
                if (CanSetNextState(_currentState, State.IDLE))
                {
                    ProccessOnceState();
                    //_player.RemoveEnemy(this);
                }
            }
        }

        ProccessUpdateState();
        transform.position = _rigid.position;
    }

    private void Attack()
    {
        if (CanSetNextState(_currentState, State.ATTACK))
        {
            ProccessOnceState();
        }
    }

    public override void OnAnimationStart(string animName)
    {
        //Debug.Log("OnAnimationStart Enemy");
    }

    public override void OnAnimationCompleted(string animName)
    {
        Debug.Log("OnAnimationCompleted Enemy: " + _currentState);

        switch (_currentState)
        {
            case State.ATTACK:
                CanSetNextState(_currentState, State.IDLE);
                break;
            case State.HURT:
                if (_isAlive)
                {
                    CanSetNextState(_currentState, State.WAIT);
                }
                else
                {
                    CanSetNextState(_currentState, State.DEATH);
                    ProccessOnceState();
                }
                break;
            case State.DEATH:
                Invoke("Deactive", _timeToDissapear);
                break;
        }

        _isInvincible = false;
    }

    private void Deactive()
    {
        _rigid.transform.parent = transform;
        gameObject.SetActive(false);
    }

    public bool IsInvincible
    {
        get
        {
            return _isInvincible;
        }
    }

    private void SetFlip(bool isRight)
    {
        _isRigth = isRight;
        if (_isRigth)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    private bool CanSetNextState(State current, State next)
    {
        switch (current)
        {
            case State.IDLE:
                if (next == State.ATTACK || next == State.FOLLOW || next == State.WAIT || next == State.HURT)
                {
                    _currentState = next;
                    return true;
                }
                break;

            case State.ATTACK:
                if (next == State.IDLE)
                {
                    _currentState = next;
                    return true;
                }
                break;

            case State.FOLLOW:
                if (next == State.IDLE || next == State.ATTACK || next == State.WAIT || next == State.HURT)
                {
                    _currentState = next;
                    return true;
                }
                break;

            case State.WAIT:
                if (next == State.FOLLOW || next == State.ATTACK || next == State.HURT)
                {
                    _currentState = next;
                    return true;
                }
                break;

            case State.HURT:
                if (next == State.IDLE || next == State.HURT || next == State.DEATH)
                {
                    _currentState = next;
                    return true;
                }
                break;
        }
        return false;
    }

    private void ProccessUpdateState()
    {
        switch (_currentState)
        {
            case State.FOLLOW:
                Vector2 dir = (_player.transform.position - transform.position).normalized;
                var vel = dir * speed;
                _rigid.velocity = vel;

                //Debug.Log("Dir: "+vel);
                /* var pos = transform.position + (_player.transform.position-transform.position).normalized * speed;
				transform.position = pos;	*/
                _anim.Play("Walk");
                break;
        }
    }

    private void ProccessOnceState()
    {
        switch (_currentState)
        {

            case State.IDLE:
                _anim.Play("Idle");
                break;

            case State.ATTACK:
                //if(!_isInvincible){					
                _anim.Play("Attack", 0, 0);
                //_isInvincible = true;			
                //}			
                break;

            case State.WAIT:
                _anim.Play("Idle");
                Invoke("Attack", 1);
                break;

            case State.HURT:
                CancelInvoke("Attack");
                break;

            case State.DEATH:
                _anim.Play("Death");
                break;
        }
        _rigid.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D: " + collision.name);
    }
}

