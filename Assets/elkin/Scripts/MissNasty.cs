using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MissNasty : Enemy
{
    public QteManager _qteManager;
    public float minTimeToChangeState;
    public float maxTimeToChangeState;
    public float distanceToDetection; 
    private SoundManager _soundManager;
    protected bool _isRight = false;  


    public override void Start()
    {
        _currentState = State.IDLE;
        _player = FindObjectOfType<Player>();
        _agent = GetComponent<NavMeshAgent>();
        _soundManager = FindObjectOfType<SoundManager>();        
        _anim = GetComponentInChildren<Animator>();
        _lifeBar = GetComponent<LifeBar>();
        _agent.speed = speed;

        //CalculateTimeNextState();
        //SetNewState(State.FOLLOW);
    }

    private void CalculateTimeNextState()
    {
        //_timeToChangeState = Random.Range(minTimeToChangeState, maxTimeToChangeState);
    }
    
    protected override bool CanSetNextState(State prevState, State nextState)
    {
        switch(prevState)
        {
            case State.FOLLOW:
                if (nextState == State.IDLE || nextState == State.ATTACK)
                {
                    _currentState = nextState;
                    return true;
                }
                return false;

            case State.IDLE:
                if (nextState == State.FOLLOW || nextState == State.HURT || nextState == State.BLOCK || nextState == State.IN_QTE)
                {
                    _currentState = nextState;
                    return true;
                }
                return false;

            case State.ATTACK:
                if (nextState == State.IDLE)
                {
                    _currentState = nextState;
                    return true;
                }
                return false;

            case State.BLOCK:
                if (nextState == State.IDLE)
                {
                    _currentState = nextState;
                    return true;
                }
                return false;

            case State.IN_QTE:
                if (nextState == State.IDLE)
                {
                    _currentState = nextState;
                    return true;
                }
                return false;

            default:
                return false;
        }
    }

    private void SetNewState(State newState)
    {
        _currentState = newState;
    }

    public override void Update()
    {
        CheckStateMachine();
        SetFlip(transform.position.x < _player.transform.position.x);
    }

    public override void ShowHitAnim(string animName)
    {

        if (_isAlive)
        {
            var damage = GetDamageValue(animName);
            _isAlive = _lifeBar.UpdateHp(damage);
            Debug.Log("Alive: " + _isAlive);
            Debug.Log("Damage: " + damage);

            
            if (_isAlive)
            {
                if(animName[0]=='P' && CanSetNextState(_currentState, State.BLOCK))
                {
                    parts.gameObject.SetActive(_isAlive);
                    CancelInvoke("RemoveTarget");
                    CancelInvoke("Attack");
                    
                    _anim.Play("miss_nasty_hurt_legs", 0, 0);
                    _agent.isStopped = true;
                    soyVulnerable = true;

                    
                    CancelInvoke("QuitarVulnerable");
                    Invoke("QuitarVulnerable", 3);
                    return;
                }

                if (animName[0] == 'T' && CanSetNextState(_currentState, State.HURT))
                {
                    _player.RecibirBloqueo();
                    _anim.Play("miss_nasty_hurt_body", 0, 0);
                    _agent.isStopped = true;
                    return;
                    
                }

                if (animName[0] == 'C')
                {                    
                    if (soyVulnerable)
                    {
                        //cae al suelo      
                        _anim.Play("miss_nasty_hurt_head", 0, 0);
                        /*StartingQTE();
                        SetNewState(State.IN_QTE);
                        _qteManager.CallQTE("QteMissNastyWeak", QTEWeakFinished);*/
                    }
                    else
                    {
                        // agarrar al player
                        _qteManager.CallQTE("QteMissNastyStrong", QTEStrongFinished);
                    }
                    
                }
            }
            else
            {
                _anim.Play("miss_nasty_dead");
                Deactive();
            }
            
        }
       
    }

    private void StartingQTE()
    {
        parts.gameObject.SetActive(false);
        _anim.Play("miss_nasty_hurt_head", 0, 0);
        _agent.isStopped = true;
        //parts.gameObject.SetActive(_isAlive);
        CancelInvoke("RemoveTarget");
        CancelInvoke("Attack");
    }

    private void QTEWeakFinished(bool result)
    {
        Debug.Log("QTEWeakFinished: " + result);
        _anim.gameObject.SetActive(true);
        SetNewState(State.IDLE);
        parts.gameObject.SetActive(true);
    }

    private void QTEStrongFinished(bool result)
    {
        Debug.Log("QTEStrongFinished: " + result);
        _anim.gameObject.SetActive(true);
        SetNewState(State.IDLE);
        parts.gameObject.SetActive(true);
        _anim.Play("miss_nasty_hurt_body", 0, 0);
    }

    private void QuitarVulnerable()
    {
        soyVulnerable = false;
    }

    private void CheckStateMachine()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }

        switch (_currentState)
        {
            case State.IDLE:
               
                //if (!CanSetNextState(_currentState, State.FOLLOW)) return;

                    
                /*if (CalculateDistance(_player.gameObject, gameObject) > distanceToDetection)
                {
                    _agent.SetDestination(_player.transform.position);
                    _agent.isStopped = false;
                    SetNewState(State.FOLLOW);
                    _anim.Play("walk", 0, 0);
                }*/
              
            break;

            case State.FOLLOW:
                
                if (_player.IsAlive)                    
                {
                    if(CalculateDistance(_player.gameObject, gameObject) < distanceToAttack && CanSetNextState(_currentState, State.ATTACK))
                    {
                        _agent.isStopped = true;
                        SetNewState(State.ATTACK);
                        _anim.Play("attack", 0, 0);
                    }
                }
                else
                {
                    SetNewState(State.IDLE);
                }
            break;

        }
    }

    public override void HacerDano()
    {        
         _player.ShowDamage(this);        
    }

    private float CalculateDistance(GameObject target, GameObject destination)
    {
        var distance = Vector3.Distance(target.transform.position, destination.transform.position);
        Debug.Log("Distance: " +distance);
        return distance;
    }

    public override void OnAnimationCompleted(string animName)
    {
        
        if( animName== "hurt_head")
        {
            //inicia qte
            _anim.gameObject.SetActive(false);
            StartingQTE();
            SetNewState(State.IN_QTE);
            _qteManager.CallQTE("QteMissNastyWeak", QTEWeakFinished);
            return;
        }
        
        switch (_currentState)
        {
            case State.ATTACK:
            case State.HURT:
            case State.BLOCK:
                SetNewState(State.IDLE);
            break;
        }

    }

    private void SetFlip(bool isRight)
    {
        _isRight = isRight;
        if (_isRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
}
