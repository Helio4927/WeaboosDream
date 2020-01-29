using UnityEngine;
using UnityEngine.AI;

public class Enemy : AnimEvents {
    protected LifeBar _lifeBar;
    protected Animator _anim;
    protected Player _player;
    public float distanceToFollow = 5;
    public float distanceToAttack = 1;
    public float minDistanceToMoveAway = 2;
    public float speed = 0.05f;
    protected bool _isTarget = false;
    public float stuneTime = 0.5f;
    //private bool _isInvincible = false;
    protected State _currentState;
    protected bool _isRigth = false;
    protected bool _isAlive = true;
    public float _distanceWhenPlayerHit = 2;
    public float _timeToDissapear = 1;
    public Transform parts;
    protected NavMeshAgent _agent;
    public Transform target;
    public bool soyVulnerable = true;
    public float tiempoEsperaAtaqueEnemigo;
    public float contadorPostRecibirAtaque;
    public SoundManager soundManager;
    
    public enum State
    {
        IDLE, FOLLOW, ATTACK, WAIT, HURT, DEATH, BLOCK,BEBLOCKED
    }

    public virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        soundManager = FindObjectOfType<SoundManager>();
        _lifeBar = GetComponentInChildren<LifeBar>();
        _anim = GetComponentInChildren<Animator>();
        _player = FindObjectOfType<Player>();
        //_rigid = GetComponentInChildren<Rigidbody2D>();
        //_rigid.transform.parent = null;
       
        _currentState = State.IDLE;
        _player.AddEnemy(this);

    }

    public void LlamarFollowDespuesDeHurt()
    {
        CanSetNextState(_currentState, State.FOLLOW);
        ProccessOnceState();
    }

    private int GetDamageValue(string animName)
    {
        var letra = animName[animName.Length - 2];
        Debug.Log("letra: "+letra);

        if (letra.Equals('C'))
        {
            return 3;
        }else if (letra.Equals('T'))
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public virtual void ShowHitAnim(string animName) {
        if (animName.Equals("Fatality001"))
        {
            _isAlive = _lifeBar.UpdateHp(100);
            if (!_isAlive)
            {
                CanSetNextState(_currentState,State.DEATH);
                ShowFatality();
                _player.IncrementarVida();
            }      
            return;
        }

        if (soyVulnerable && _isAlive)
        {
            var damage = GetDamageValue(animName);
            _isAlive = _lifeBar.UpdateHp(damage);            
            Debug.Log("Alive: " + _isAlive);
            Debug.Log("Damage: " + damage);

            if (_isAlive)
            {
                if (CanSetNextState(_currentState, State.HURT))
                {
                    parts.gameObject.SetActive(_isAlive);
                    CancelInvoke("RemoveTarget");
                    CancelInvoke("Attack");                    
                    _anim.Play(animName, 0, 0);
                    _agent.isStopped = true;
                    Invoke("LlamarFollowDespuesDeHurt",contadorPostRecibirAtaque);
                }
            }
            else
            {
              //  soundManager.PlaySound(9);
                Debug.Log("Current Anim: " + _currentState);
                if (CanSetNextState(_currentState, State.DEATH))
                {                    
                    var hitZone = animName[0];             
                    if(animName.Length == 4)
                    {
                        hitZone = animName[2];
                    }
                    Deactive();
                    
                    _anim.Play("Death_" + hitZone, 0, 0);
                    Debug.Log("Enemy Play Death: " + hitZone);
                    _player.IncrementarVida();
                }
            }           
        }
        else
        {
            if (CanSetNextState(_currentState, State.BLOCK))
            {
                ProccessOnceState();
                print("el jugador ha atacado al enemigo sin ningun efecto");
            }
        }
    }

    private void ShowFatality()
    {
        print("ShowFatality _currentState : " + _currentState);
        CanSetNextState(_currentState, State.DEATH);
        _anim.gameObject.SetActive(false);

        Deactive();    
        
    }

    public virtual void ShowAnimBeBlocked()
    {
        print("ShowAnimBeBlocked _currentState : " + _currentState);
        CanSetNextState(_currentState, State.BEBLOCKED);        
        ProccessOnceState();
    }

    public void FarAway() {

        if (IsCloseToMoveAway())
        {
            if ((_player.IsRight && transform.position.x > _player.transform.position.x) ||(!_player.IsRight && transform.position.x < _player.transform.position.x))
            {
                var dir = (transform.position - _player.transform.position).normalized;
                GetComponentInChildren<MoveAwayEffect>().MoveAway(dir, this);
            }         
            //Debug.Log("FarAway: " + dir);
        }
    }

    private void RemoveTarget() {
        _isTarget = false;
        CanSetNextState(_currentState, State.IDLE);
        //Debug.Log("RemoveTarget");
    }

    private void TestFarEnemyAway()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            FarAway();
        }
    }

    private void TryToFollowPlayer()
    {
        if (_currentState != State.FOLLOW && IsCloseToFollow())
            SeguirObjetivo();
    }

    private bool IsCloseToFollow()
    {
        var dist = Vector3.Distance(transform.position, target.position);
        //Debug.Log("distanceToFollow: " + dist);
        return dist < distanceToFollow;
    }

    private bool IsCloseToAttack()
    {
        var dist = Vector3.Distance(transform.position, target.position);
        //Debug.Log("distanceToAttack: " + dist);
        return dist < distanceToAttack;
    }

    private bool IsCloseToMoveAway()
    {
        var dist = Vector3.Distance(transform.position, target.position);
        //Debug.Log("minDistanceToMoveAway: " + dist);
        return dist < minDistanceToMoveAway;
    }
    public void SelectTarget()
    {
        var targetfrontal = Vector3.Distance(transform.position, _player.posEnemigoAtaqueDelante.position);
        var targetTrasero = Vector3.Distance(transform.position, _player.posEnemigoAtaqueatras.position);
        if (targetfrontal < targetTrasero)
        {
            target = _player.posEnemigoAtaqueDelante;
        }
        else
        {
            target = _player.posEnemigoAtaqueatras;
        }
    }

    protected void Update()
    {
        //if (!_player.IsAlive) return;

        if (!_isAlive || _currentState == State.DEATH || _currentState == State.BEBLOCKED) return; 
        
        SelectTarget();
        if (_currentState != State.FOLLOW && IsCloseToFollow())
            SeguirObjetivo();
        
        SetFlip(transform.position.x < _player.transform.position.x);

        _anim.GetComponent<SpriteRenderer>().sortingOrder = transform.position.y < _player.transform.position.y ? 3 : 1;

        if (_currentState == State.WAIT || _currentState == State.ATTACK || _currentState == State.HURT || _currentState == State.DEATH)
            return;

        if (!_agent.isOnNavMesh) return;

        //Debug.Log("Enemy: "+_agent);
        if (_agent.isStopped)
        {
            if (CanSetNextState(_currentState, State.IDLE))
            {
                ProccessOnceState();
            } else if (IsCloseToFollow())
            {
                if (CanSetNextState(_currentState, State.FOLLOW))
                {
                    ProccessOnceState();
                }
            }
        }
        else
        {
            if (_agent.remainingDistance > _agent.stoppingDistance) {
                if (CanSetNextState(_currentState, State.FOLLOW)) {
                    ProccessOnceState();
                }
            } else
            {
                //Debug.Log("Current Antes: " + _currentState);
                if (CanSetNextState(_currentState, State.WAIT)) {
                    //Debug.Log("Current Despues: " + _currentState);
                    ProccessOnceState();
                }
            }
        }

        //}

        //ProccessUpdateState();
        //transform.position = _rigid.position;
    }

    private void Attack() {
        if (CanSetNextState(_currentState, State.ATTACK)) {
            //Debug.Log("Attack");
            ProccessOnceState();
        }
    }

    public override void OnAnimationStart(string animName) {
        //Debug.Log("OnAnimationStart Enemy");
    }

    public override void OnAnimationCompleted(string animName) {
        Debug.Log("OnAnimationCompleted Enemy: " + _currentState);
        //_isInvincible = false;
        switch (_currentState) {
            case State.ATTACK:
                CanSetNextState(_currentState, State.IDLE);
                SeguirObjetivo();
                ProccessOnceState();
                break;

            case State.HURT:
                if (_isAlive) {
                    CanSetNextState(_currentState, State.IDLE);
                    ProccessOnceState();
                }              
                break;           

            case State.BLOCK:
                CanSetNextState(_currentState, State.ATTACK);
                ProccessOnceState();            
                break;

            case State.BEBLOCKED:
                CanSetNextState(_currentState, State.IDLE);
                ProccessOnceState();
                
                break;
        }
    }

    private void Deactive() {
        print(gameObject.name+" viene del deactive");
        gameObject.tag = "Untagged";
        _agent.isStopped = true;
        _anim.transform.parent = null;
        gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        parts.gameObject.SetActive(false);

    }

    private void SetFlip(bool isRight) {
        _isRigth = isRight;
        if (_isRigth) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            transform.localScale = Vector3.one;
        }
    }

    protected virtual bool CanSetNextState(State current, State next)
    {
        switch (current) {
            case State.IDLE:
                if (next == State.ATTACK || next == State.FOLLOW || next == State.WAIT || next == State.HURT || next == State.DEATH) {
                    _currentState = next;
                    return true;
                }
                break;

            case State.ATTACK:
                if (next == State.IDLE || next == State.BLOCK || next == State.DEATH || next == State.BEBLOCKED) {
                    _currentState = next;

                    return true;
                }
                break;

            case State.FOLLOW:
                if (next == State.IDLE || next == State.ATTACK || next == State.WAIT || next == State.HURT || next == State.DEATH) {
                    _currentState = next;
                    return true;
                }
                break;

            case State.WAIT:
                if (next == State.FOLLOW || next == State.ATTACK || next == State.HURT || next == State.DEATH) {
                    _currentState = next;
                    return true;
                }
                break;

            case State.HURT:
                if (next == State.WAIT || next == State.HURT || next == State.DEATH || next == State.IDLE) {
                    _currentState = next;
                    return true;
                }
                break;

            case State.BLOCK:
                if (next == State.ATTACK || next == State.DEATH)
                {
                    _currentState = next;
                    return true;
                }
                break;

            case State.BEBLOCKED:
                if (next == State.IDLE || next == State.HURT || next == State.DEATH)
                {
                    _currentState = next;               
                    return true;
                }
                break;
        }
        return false;
    } 

    public override void HacerDano()
    {
        if (IsCloseToAttack())
        {
            _player.ShowDamage(this);
        }        
    }

    public override void SetVulnerable(bool vulnerable)
    {
        //Debug.Log("SetVulnerable Enemy: "+vulnerable);
        soyVulnerable = vulnerable;
    }

    private void ProccessOnceState() {
        switch (_currentState) {
            case State.FOLLOW:
                _agent.isStopped = false;
                //soyVulnerable = true;
                _anim.Play("Walk");
                InvokeRepeating("SeguirObjetivo", 0.1f, 0.5f);                
            break;

            case State.IDLE:
                CancelInvoke("Attack");
                CancelInvoke("SeguirObjetivo");
                //_isInvincible = false;
                _anim.Play("Idle");
                _agent.isStopped = true;
                
                break;

            case State.ATTACK:
                //Debug.Log("ProccessOnceState.Attack");
                CancelInvoke("Attack");
                CancelInvoke("SeguirObjetivo");
                _anim.Play("Attack", 0, 0);
                _agent.isStopped = true;                   
                break;

            case State.WAIT:
                CancelInvoke("SeguirObjetivo");
                _anim.Play("Idle");
                CancelInvoke("Attack");
                Invoke("Attack", tiempoEsperaAtaqueEnemigo);
                _agent.isStopped = true;
                break;

            case State.HURT:
                
                CancelInvoke("SeguirObjetivo");
                CancelInvoke("Attack");
                _agent.isStopped = true;

                break;

            case State.DEATH:
                soundManager.PlaySound(9);
                CancelInvoke("SeguirObjetivo");
                _anim.Play("Death");
             
                break;

            case State.BLOCK://a probar con y sin para balancearlo
             
                CancelInvoke("SeguirObjetivo");
                _anim.Play("Block", 0, 0);
                _agent.isStopped = true;
                _player.RecibirBloqueo();
                
                break;

            case State.BEBLOCKED:
                _anim.Play("BeBlocked", 0, 0);
                CancelInvoke("Attack");

                break;
        }
        //_rigid.velocity = Vector2.zero;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D: " + collision.name);
    }
  
    public void SeguirObjetivo()
    {
        if (gameObject.activeSelf)
        {
            GetComponent<NavMeshAgent>().SetDestination(target.position);
        }        
    }

    public bool IsTarget
    {
        set{
            _isTarget = value;
        }

        get
        {
            return _isTarget;
        }
    }
}
