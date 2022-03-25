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
    public float distanceForce = 300;
    public float ragePercentage = 40;
    public float distanceToDash = 2;

    private bool _isInit;
    private float _time = 0;
    private float _timeToEnableRage = 0;

    public float timeFollow = 2;
    public float timeInIdle = 2;
    public float timeForRage = 10;
    public float timeForTired = 10;
    public float durationDash = 10;
    public override void Start()
    {
        _currentState = State.IDLE;
        _player = FindObjectOfType<Player>();
        _agent = GetComponent<NavMeshAgent>();
        _soundManager = FindObjectOfType<SoundManager>();        
        _anim = GetComponentInChildren<Animator>();
        _lifeBar = GetComponent<LifeBar>();
        _agent.speed = speed;
        soyVulnerable = false;
        _isInit = false;
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
            case State.HURT:
                if(nextState == State.IDLE || nextState == State.IN_QTE)
                {
                    _currentState = nextState;
                    return true;
                }
                return false;

            case State.FOLLOW:
                if (nextState == State.IDLE || nextState == State.ATTACK || nextState == State.HURT)
                {
                    _currentState = nextState;
                    return true;
                }
                return false;

            case State.IDLE:
                if (nextState == State.DASH || nextState == State.FOLLOW || nextState == State.HURT || nextState == State.BLOCK || nextState == State.IN_QTE)
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

            case State.DASH:
                if (nextState == State.TIRED)
                {
                    _currentState = nextState;
                    return true;
                }
                return false;

            case State.TIRED:
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

    public override void Update()
    {
        CheckStateMachine();
        SetFlip(transform.position.x < _player.transform.position.x);
    }

    protected override int GetDamageValue(string animName)
    {
        // cambiar para obtener daño especifico
        return base.GetDamageValue(animName);
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
                var letter = animName[animName.Length - 2];
                if (letter=='P' && CanSetNextState(_currentState, State.HURT))
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

                if (letter == 'T' && CanSetNextState(_currentState, State.BLOCK) )
                {
                    _player.RecibirBloqueo();
                    _anim.Play("miss_nasty_hurt_body", 0, 0);
                    _agent.isStopped = true;
                    return;
                    
                }

                if (letter == 'C' && CanSetNextState(_currentState, State.HURT))
                {
                    _player.HidePlayer();
                    if (soyVulnerable)
                    {
                        //recibe golpe y cae al suelo      
                        //_anim.Play("miss_nasty_hurt_head_weak", 0, 0);
                        //_anim.Play("wait_qte_weak", 0, 0);
                        _anim.Play("hurt_in_floor", 0, 0);
                        StartingQTE();
                        CanSetNextState(_currentState, State.IN_QTE);
                        _qteManager.CallQTE("QteMissNastyWeak", QTEWeakFinished);
                    }
                    else
                    {   
                        // detener golpe de player (animacion incluye el player)
                        _anim.Play("miss_nasty_hurt_head_strong", 0, 0);                        
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
        _agent.isStopped = true;
        //parts.gameObject.SetActive(_isAlive);
        CancelInvoke("RemoveTarget");
        CancelInvoke("Attack");
    }

    private void QTEWeakFinished(bool result)
    {
        _anim.gameObject.SetActive(true);
        parts.gameObject.SetActive(true);

        //_anim.Play("hit_from_floor", 0, 0);
        _player.ResetActions();           
        QuitarVulnerable();

        var itWouldDie = _lifeBar.IsGoingToDie(10);

        if (itWouldDie)
        {
            //CanSetNextState(_currentState, State.IDLE);
            //player hace fatality
            _anim.Play("miss_nasty_dead", 0, 0);

        }
        else
        {
            //aca deberia ir animacion de ponerse de pie
            _anim.Play("hit_from_floor", 0, 0);            
        }

        Debug.Log("QTEWeakFinished: " + result);     
        
    }

    private void QTEStrongFinished(bool result)
    {
        Debug.Log("QTEStrongFinished: " + result);
        if(result)
        {
            _anim.gameObject.SetActive(true);
            CanSetNextState(_currentState, State.IDLE);
            parts.gameObject.SetActive(true);
            _anim.Play("miss_nasty_hurt_body", 0, 0);
            _player.ShowPlayer();
            _player.MoveAwayPlayerOf(_player.transform.position - Vector3.right, distanceForce);
            _player.ShowAnimDefend();
        }
        else
        {
            _player.HidePlayer();
            _anim.gameObject.SetActive(true);
            parts.gameObject.SetActive(true);
            _anim.Play("miss_nasty_fatality", 0, 0);
        }
        _player.ResetActions();
        QuitarVulnerable();
    }

    /* Tambien se llama desde un Invoke */
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

        if (_player == null) return;

        if(_isInit)
        {
            _time += Time.deltaTime;
            _timeToEnableRage += Time.deltaTime;
        }
        

        var currentDistance = CalculateDistance(_player.gameObject, gameObject);
        switch (_currentState)
        {
            case State.IDLE:
                
                if (!_player.IsAlive) return;

                var probability = Random.Range(0, 100);
                Debug.Log("Current Distance: " + currentDistance);
                Debug.Log("Probability: " + probability);
                
                if(currentDistance < distanceToDetection)
                {
                    _isInit = true;
                }

                if (_time > timeInIdle && _isInit )
                {
                    _time = 0;
                    _agent.isStopped = false;
                    _agent.speed = speed;
                    CanSetNextState(_currentState, State.FOLLOW);
                    SetDestinationByInterval();
                    _anim.Play("walk", 0, 0);
                }
                else if(currentDistance> distanceToDetection && currentDistance < distanceToDash && probability <= ragePercentage && _timeToEnableRage > timeForRage)
                {                    
                    _time = 0;
                    _timeToEnableRage = 0;
                    _agent.SetDestination(_player.transform.position);
                    _agent.isStopped = false;
                    CanSetNextState(_currentState, State.DASH);
                    _anim.Play("dash", 0, 0);
                    _agent.speed *= 8;
                    Invoke("FinishDash", durationDash);
                }
                else
                {
                    _agent.speed = speed;
                    _agent.isStopped = true;
                }

                /*if (probability <= ragePercentage && distanceCal < distanceToDash && _currentState != State.DASH)
                {
                    _agent.SetDestination(_player.transform.position);
                    _agent.isStopped = false;
                    CanSetNextState(_currentState, State.DASH);
                    _anim.Play("dash", 0, 0);
                    _agent.speed *= 2;
                    Invoke("FinishDash", 10);

                }else if (distanceCal < distanceToDetection)
                {
                    
                    _agent.isStopped = false;
                    _agent.speed = speed;
                    CanSetNextState(_currentState, State.FOLLOW);
                    SetDestinationByInterval();
                    _anim.Play("walk", 0, 0);
                }*/

                break;

            case State.FOLLOW:
                
                
                if (_player.IsAlive)                    
                {                   

                    
                    if (currentDistance < distanceToAttack && CanSetNextState(_currentState, State.ATTACK))
                    {
                        _time = 0;
                        _agent.isStopped = true;
                        CanSetNextState(_currentState, State.ATTACK);
                        _anim.Play("attack", 0, 0);
                    }
                    else if (_time > timeFollow)
                    {
                        _time = 0;
                        _agent.isStopped = true;
                        _anim.Play("idle", 0, 0);
                        CanSetNextState(_currentState, State.IDLE);
                    }

                    
                }
                else
                {
                    CanSetNextState(_currentState, State.IDLE);
                }
                break;

            case State.DASH:
                Debug.Log("Current velocity: "+ _agent.velocity);
                Debug.Log("DASH Current Distance: " + currentDistance);
                if (currentDistance < 1 /*|| _agent.velocity.magnitude < 0.1f*/)
                {
                    _agent.isStopped = true;
                    _player.ShowDamage(this);
                    CancelInvoke("FinishDash");
                    FinishDash();
                }

                break;

        }
    }

    private void SetDestinationByInterval()
    {
        if(_currentState == State.FOLLOW)
        {
            _agent.SetDestination(_player.transform.position);
            Invoke("SetDestinationByInterval", 1);
        }        
    }

    private void FinishDash()
    {
        _agent.isStopped = true;
        _anim.Play("tired", 0, 0);
        _timeToEnableRage = 0;
        CanSetNextState(_currentState, State.TIRED);
        Invoke("FinishedTiredState", timeForTired);
    }

    public void FinishedTiredState()
    {
        CanSetNextState(_currentState, State.IDLE);
    }

    public override void HacerDano()
    {        
        _player.ShowDamage(this);        
    }

    public override void HitFromFloorHacerDano()
    {
        _player.ShowPlayer();
        _player.ShowAnimDefend();
        _player.MoveAwayPlayerOf(transform.position, distanceForce);
    }

    private float CalculateDistance(GameObject target, GameObject destination)
    {
        var distance = Vector3.Distance(target.transform.position, destination.transform.position);        
        return distance;
    }

    public override void OnAnimationCompleted(string animName)
    {
        switch(animName)
        {
            case "hurt_body":
                //player muestra daño                
                _anim.Play("idle", 0, 0);
                CanSetNextState(_currentState, State.IDLE);
                break;
                
            case "hit_from_floor":
                //player muestra daño                
                _anim.Play("idle", 0, 0);
                CanSetNextState(_currentState, State.IDLE);                
                break;
                /*
            case "hurt_in_floor":
                var itWouldDie = _lifeBar.IsGoingToDie(10);
                
                if (itWouldDie)
                {
                    //CanSetNextState(_currentState, State.IDLE);
                    //player hace fatality
                    _anim.Play("miss_nasty_dead",0,0);

                }
                else
                {
                    //aca deberia ir animacion de ponerse de pie
                    _anim.Play("idle", 0, 0);
                    CanSetNextState(_currentState, State.IDLE);

                    //al terminar hace defense
                    _player.ShowPlayer();
                    _player.MoveAwayPlayerOf(_player.transform.position-Vector3.right, distanceForce);
                    _player.ShowAnimDefend();
                }

                break;
                */
            case "hurt_head_weak":
                /*
                //inicia qte miss nasty debil
                _anim.Play("wait_qte_weak", 0, 0);
                StartingQTE();
                CanSetNextState(_currentState, State.IN_QTE);
                _qteManager.CallQTE("QteMissNastyWeak", QTEWeakFinished);       
                */
                break;

            case "hurt_head_strong":
                //inicia qte miss nasty fuerte
                _anim.gameObject.SetActive(true);
                _anim.Play("miss_nasty_forcejeo", 0, 0);
                //inicia qte
                StartingQTE();
                CanSetNextState(_currentState, State.IN_QTE);
                _qteManager.CallQTE("QteMissNastyStrong", QTEStrongFinished);
                break;

            case "dash":
                return;                
        }    
        

        switch (_currentState)
        {
            case State.ATTACK:
            case State.HURT:
            case State.BLOCK:
                _anim.Play("idle", 0, 0);
                CanSetNextState(_currentState, State.IDLE);
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
