using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;


public class Player : AnimEvents
{
    [Header("Distancia de ajuste de Animacion")]
    [SerializeField] private int _distBetweenEnemy = 3;
    
    [SerializeField] private float _maxDistance = 3.5f;    
    public float timeToStartHit = 1;
    public float tiempoParaCombo;
    private string _combo;
    private int _contador;
    private bool _isAttacking = false;
    private string _actualAnim = "";
    private int _actualTipo = 0;
    private Animator _anim;
    private Enemy _currentEnemy;
    public float speed = 1;
    private Queue<AnimStruct> _actions;
    private string _anteriorAnim;
    private bool _hasEnemy;
    private bool _isRigth = true;    
    private List<Enemy> _enemyList;
    private bool _waiting = false;
    public Transform posEnemigoAtaqueDelante;
    public Transform posEnemigoAtaqueatras;
    public bool soyVulnerable = true;
    public bool _isBlocked;
    public GameObject padrePanelExaminarWorld;
    public bool _isDamaged;
    public bool doingBLock;
    public bool canDoBlock;
    public Animator _animFatality;
    public CameraShake _cameraShaker;
    private AnimatorClipInfo[] m_CurrentClipInfo;
    private bool _isAlive = true;
    public int vidaRestanteParaParpadeo;
    public inventoryManager inventario;
    public SoundManager soundManager;
    private NavMeshAgent _nav;
    public bool habilitarAtaque;

    //public float contadorTiempoDanado;

    private void Awake()
    {
        _enemyList = new List<Enemy>();
        _actions = new Queue<AnimStruct>();
        _anim = GetComponentInChildren<Animator>();
        m_CurrentClipInfo = _anim.GetCurrentAnimatorClipInfo(0);
        soundManager = FindObjectOfType<SoundManager>();
        _nav = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            _anim.SetBool("Tuberia", true);
            Debug.Log("Estamos en menú, le damos la palanca");
        }
        _animFatality.gameObject.SetActive(false);
    }

    private void ResetAnim()
    {
        _contador = 0;
        _combo = "";        
        _actualAnim = "";
        _actions.Clear();
    }

    public override void SetVulnerable(bool vulnerable)
    {
        //Debug.Log("SetVulnerable Player: " + vulnerable);
        soyVulnerable = vulnerable;
    }
    
    public void ComprobarPanelHerido()
    {
        float vida = GameObject.Find("Player3D").GetComponent<LifeBar>().initHp;  
        FindObjectOfType<GameManager>().panelHerido.GetComponent<Animator>().SetInteger("VidaRestante",(int)vida);
        print("Vamos a ver: " + inventario.statusAnimator.name);
        inventario.statusAnimator.SetInteger("VidaPLayer", (int)vida);
        if (vida > 2)
        {
            FindObjectOfType<GameManager>().panelHerido.GetComponent<AudioSource>().enabled = false;
        }
    }

    public void ShowDamage(MissNasty boss)
    {
        var life = GetComponent<LifeBar>();
        if (!life.IsAlive) return;

        _isAlive = life.UpdateHp(1);

        if (_isAlive)
        {
            _isDamaged = true;
            _anim.Play("Damage", 0, 0);
            
            soundManager.PlayRandomGemidoWeeb();
            //ResetAnim();
        }
        else
        {
            soundManager.PlaySound(8);
            _anim.Play("animMuerte");
        }
    }

    public void ShowDamage(Enemy currentAttackingEnemy)
    {
        _isBlocked = false;
        var life = GetComponent<LifeBar>();

        if (!life.IsAlive) return;

        if (soyVulnerable && !_isDamaged && !canDoBlock)
        {
            soundManager.PlayRandomAttackEnemy();
            soundManager.PlayRandomGemidoWeeb();
            _isAlive = life.UpdateHp(1);
            ComprobarPanelHerido();

            if (_isAlive) {
               
                _isDamaged = true;
                _anim.Play("Damage", 0, 0);
                
                ResetAnim();
            }
            else
            {
                
                soundManager.PlaySound(8);
                //mostrar anim de muerte y al terminar llamar el SetActive(false)
                //gameObject.SetActive(false);
                _anim.Play("animMuerte");
                print("LLAMAR A GAME OVER EN EL COMPLETE DE LA MUERTE");
                Invoke("EncenderPanelGameOver", 5f);
;                
            }
        }
        else
        {
            currentAttackingEnemy.ShowAnimBeBlocked();
            //print("EL ATAQUE NO HA SURTIDO EFECTO");
        }
    }

    public void EncenderPanelGameOver()
    {
        FindObjectOfType<GameManager>().panelGameOver.SetActive(true);
        FindObjectOfType<CursorManager>().CursorMouseOn(true);

    }

    public void AddEnemy(Enemy en)
    {
        if (!_enemyList.Contains(en))
        {
            _enemyList.Add(en);
        }
    }

    public void RemoveEnemy(Enemy en)
    {
        if (_enemyList.Contains(en)) _enemyList.Remove(en);
    }

    private void NotificateEnemies(Enemy enToIgnore)
    {
        //print("se ha llamado a check de empuje");
        //if (enToIgnore is MissNasty) Debug.Log("<color=yellow>GOLPEO A MISS NASTY</color>");
        _enemyList.ForEach(e => { 
            if (e != enToIgnore) e.FarAway();            
        });
    }

    public void RecibirBloqueo()
    {
        _anim.Play("Blocked", 0, 0);
        _contador = 0;
        _isBlocked = true;
        soyVulnerable = true;
        ResetAnim();
        //contadorTiempoDanado = 0;
    }

    public override void MoveAwayPlayer()
    {
        GetComponent<MoveAwayEffect>().MoveAway((transform.position - _currentEnemy.transform.position).normalized, this);
    }

    public void MoveAwayPlayerOf(Vector3 position, float force)
    {
        GetComponent<MoveAwayEffect>().MoveAway((transform.position - position).normalized, this, force);
    }

    public void ShowAnimDefend()
    {
        _anim.Play("Defend", 0, 0);
    }

    public override void HabilitarElAtaque(bool valor)
    {
        habilitarAtaque = valor;
    }

    public override void CallPushFromAnim()
    {
        NotificateEnemies(_currentEnemy);
        _cameraShaker.ShakeHit();
        soundManager.PlayRandomAttackWeeb();
        soundManager.PlayRandomGemidoEnemy();


    }

    public override void CompletedFatality(string anim)
    {
        if (anim.Equals("Fatality001"))
        {
            _anim.gameObject.SetActive(true);
            _anim.SetBool("Tuberia", true);
            _isAttacking = false;
            var g = GetComponent<PoolManager>().GetSpriteDeath();
            g.SetActive(true);
            g.transform.parent = null;
            _animFatality.gameObject.SetActive(false);
            _isDamaged = false;
            soyVulnerable = true;
            FindObjectOfType<GameManager>().interacionBloqueada = false;
        }
    }

    public void ResetActions()
    {
        _actions.Clear();
    }

    public void IncrementarVida()
    {      
       _isAlive = GetComponent<LifeBar>().UpdateHp(-1);
        ComprobarPanelHerido();
    }

    public override void OnAnimationCompleted(string anim)
    {
        print("<Color=yellow>"+ anim + " completada player </Color>");
        if (anim == "animMuerte")
        {
            print("PANEL DE MUERTE AQUI HA ACABADO LA ANIMACION");
            print("VOLVER AL SUEÑO SI - NO ETC...");
            return;
        }

        if (anim == "defend")
        {
            _anim.Play("Idle", 0, 0);
            return;
        }

        if (_actions!=null && _actions.Count > 0)
        {
            // se mostrara fatality al 3 combo
            // porque no hay animaciones de 3 combo
            // y anteriormente eran solo 2
            AnimStruct entity = _actions.Dequeue();
            _combo += entity.animName;
            print("<Color=green>Combo: " + _combo + "</Color>");
            
            if (!(_currentEnemy is GreenEnemy) && _combo.Contains("T1T1C2"))      //RECETA FATALITY
            {
                soyVulnerable = false;
                canDoBlock = false;
                doingBLock = false;
                _currentEnemy.ShowHitAnim("Fatality001");
                _anim.gameObject.SetActive(false);
                _animFatality.gameObject.GetComponent<SpriteRenderer>().sprite = null;
                _animFatality.gameObject.SetActive(true);
                _animFatality.Play("Fatality001");
                ResetAnim();
                FindObjectOfType<GameManager>().interacionBloqueada = true;
            }
            else
            {
                _anim.Play(entity.animName, 0, 0);
                print("<Color=green>Reproducir: " + entity.animName + "</Color>");
                entity.enemy.ShowHitAnim(entity.animName);
                // NotificateEnemies(entity.enemy);
            }
        }
        else
        {
            //Debug.Log("OnAnimationCompleted Show Idle");
            

            if (_isAttacking && _contador > 1)
            {
                _waiting = true;
                Invoke("WaitForNextHit", timeToStartHit);
            }
                        
            _actualTipo = 0;
            _isAttacking = false;
            _anim.Play("Idle", 0, 0);
            _isBlocked = false;
            _isDamaged = false;
            doingBLock = false;
            ResetAnim();
            if (_currentEnemy != null)
            {
                _currentEnemy.IsTarget = false;
            }      
            _currentEnemy = null;
            //_anim.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    public void HidePlayer()
    {
        _anim.gameObject.SetActive(false);
    }

    public void ShowPlayer()
    {
        _anim.gameObject.SetActive(true);
        _anim.SetBool("Tuberia", true);
        _isAttacking = false;
    }

    public override void CheckEnemyType()
    {
        Debug.Log("<color=green>CheckEnemyType: "+_currentEnemy+"</color>");
        if(_currentEnemy is SpecialEnemy){
            _anim.speed=0;
            (_currentEnemy as SpecialEnemy).ShowSpecialAnim();
            //ocultar el player y el enemigo
            //llamar la animacion de fatality del enemigo            
        }
    }

    private void WaitForNextHit()
    {
        _waiting = false;
    }

    public override void OnAnimationStart(string anim)
    {

        Debug.Log("OnAnimationStart Count: " + _actions.Count);
        if (_actions.Count > 0)
        {
            AnimStruct entity = _actions.Dequeue();
            _anim.Play(entity.animName, 0, 0);
            entity.enemy.ShowHitAnim(entity.animName);
            Debug.Log("OnAnimationStart Queue: " + entity.animName);
        }
        else
        {
            Debug.Log("OnAnimationStart Param: " + anim);
        }
        //GetComponent<SpriteRenderer>().flipX = false;
        //GetComponent<Transform>().localScale = new Vector3(-1,1,1); //esto era para probar con los sprites correctamente volteados pero tampoco funciona así
    }

    public override void DoBlockOnOff(bool on)
    {
        Debug.Log("DoBlockOnOff:"+ on);
        canDoBlock = on;      
    }

    private void SetFlip(bool isRight)
    {
        _isRigth = isRight;
        if (_isRigth)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Update()
    {
        //si no hay camara o esta muerto no hacer nada
        if (Camera.main == null || !_isAlive) return;        

        if (Input.GetMouseButtonDown(0) && !_isBlocked && !_isDamaged && !FindObjectOfType<GameManager>()._panelOpen)
        {
        //    FiandObjectOfType<Camera>().GetComponent<CameraShake>().enabled = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            var gameManager = FindObjectOfType<GameManager>();
            if (hit.collider == null)
            {
                
                gameManager.usandoCurry = false;
                gameManager.usandoRombo = false;
                gameManager.usandoMoneda = false;
                FindObjectOfType<CursorManager>().setNormalTexture();
            }
            if (hit.collider != null)
            {
                gameManager.ComprobacionesObjetoClave(hit.collider.gameObject);
                if (hit.collider.gameObject.tag.Equals("WorldItem") && !GameObject.Find("PanelRecogerObjeto") && !GameObject.Find("ContenidoInventario") && !FindObjectOfType<DialogueManager>().converActiva) //si choca con un objeto que se puede coger
                {
                    print("entra en World Item");
                    hit.collider.gameObject.GetComponent<infoItem>().AbrirPanelExaminarObjeto();
                    gameManager._panelOpen = true;
                }
                else if (hit.collider.gameObject.tag.Equals("AreaExam") && !GameObject.Find("PanelExaminar") && !GameObject.Find("ContenidoInventario") && !FindObjectOfType<DialogueManager>().converActiva) //si choca con un objeto que se puede examinar
                {
                    print("entra en area exam");
                    //padrePanelExaminarWorld.transform.position = Input.mousePosition;
                    
                    hit.collider.gameObject.GetComponent<InfoExam>().Ejecutar();
                }
                else//si choca con enemigo
                {
                    var compMiss = hit.collider.GetComponentInParent<MissNasty>();
                    if (habilitarAtaque || compMiss != null)
                    {
                        print("entra en enemigo");
                        var partName = hit.collider.name.Substring(0, 1).ToUpper();
                        //Debug.Log("Part: " +partName);
                        var comp = hit.collider.GetComponentInParent<Enemy>();
                        if (compMiss)
                        {
                            Hit(compMiss, partName);
                        }
                        else
                        {
                            
                            if (comp) Hit(comp, partName);
                        }
                    }           
                }
            }
        }
       
        if (doingBLock) return;
       
        if (FindObjectOfType<GameManager>()._panelOpen)
        {
            ShowAnim(0, 0);
            return;
        }

       //  FALTA QUE LA ANIMACION EN LA QUE ESTA PASE A IDLE
        var inputX = Input.GetAxisRaw("Horizontal");
        var inputY = Input.GetAxisRaw("Vertical");

        if (!_isAttacking && !_isBlocked)
        {           
            var mouseX = Input.mousePosition.x;
            var posX = Camera.main.WorldToScreenPoint(transform.position).x;
            //SetFlip(posX < mouseX);       PARA INTENTAR QUE NO MIRE SIEMPRE AL PUNTERO


            if (!_nav.isOnNavMesh) return;

            _nav.Move(transform.forward * Time.deltaTime * inputY * speed);
            _nav.Move(transform.right * Time.deltaTime * inputX * speed);
            ShowAnim(inputX, inputY);
            if (inputX > 0) //PARA INTENTAR QUE mire hacia donde avanza
            {
                SetFlip(true);
            }
            if (inputX < 0)
            {
                SetFlip(false);
            }
        }
        if (Input.GetMouseButtonDown(1) && !_isBlocked && !_isDamaged && _anim.GetBool("Tuberia"))
        {
            _anim.Play("DoBlock",0,0);            
            doingBLock = true;
            ResetAnim();
        }           
       
    }

    public void Hit(MissNasty enemy, string inicial) 
    {
        var dist = Vector3.Distance(enemy.transform.position, transform.position);
        if (dist > _maxDistance) return;

        _contador++;
        string animName = _actualAnim + inicial + _contador;
        //_actualAnim = inicial + _contador;
        _currentEnemy = enemy;
        _contador = 0;
        if (!_isAttacking)
        {
            Debug.Log("Atacando");
            AtaqueNormal(animName);
        }
        else
        {
            _actions.Enqueue(new AnimStruct(animName, _currentEnemy));
        }
    }

    public void Hit(Enemy enemy, string inicial) //HE COMENTADO EL PRIMER FOREACH PORQUE NO HACÍA NADA MÁS QUE UN PRINT
    {
        //foreach (Transform childTemp in enemy.gameObject.transform.parent)  
        //{
        //    if (childTemp.gameObject.tag.Equals("FondoClamp"))
        //    {
        //        print(childTemp.name +" "+ childTemp.tag);
        //      //  FindObjectOfType<Camera>().GetComponent<CameraShake>().currentBackground = childTemp.gameObject;
        //    }
        //}
    
      //  FindObjectOfType<Camera>().GetComponent<CameraShake>().enabled = true;
        if (_currentEnemy && !_currentEnemy.Equals(enemy))
        {
            _currentEnemy.IsTarget = false;
            ResetAnim();
            Debug.Log("<color=green>Current Enemy: </color>" + _currentEnemy.name);
            Debug.Log("<color=green>Enemy: </color>" + enemy.name);
            _currentEnemy = null;
            return;            
        }            

        //validar distancia del player con relacion al enemigo q va a golpear
        var dist = Vector3.Distance(enemy.transform.position, transform.position);
        if (dist > _maxDistance) return;

        //primera vez q golpea
        if (_currentEnemy == null)
        {
            _currentEnemy = enemy;
            enemy.IsTarget = true;
        }
        else if (!_currentEnemy.IsTarget) //enemigo q no esta siendo golpeado
        {
            return;
        }

        if (!_waiting)
        {
            _contador++;
            if (_contador <= 2)
            {
                string animName = _actualAnim + inicial + _contador;
                _actualAnim = inicial + _contador;
                
                if (!_isAttacking)
                {
                    Debug.Log("Atacando");
                    AtaqueNormal(animName);                              
                }
                else
                {
                    _actions.Enqueue(new AnimStruct(animName, _currentEnemy));                    
                }
            }
        }
      
    }

    private void AtaqueNormal(string animName)
    {
        _isAttacking = true;
        _combo = animName;
        _anim.Play(animName, 0, 0);                             
        _currentEnemy.ShowHitAnim(animName);

        //ajustar posicion        
        var pos = transform.position;
        var sign = pos.x > _currentEnemy.transform.position.x ? 1 : -1;
        //pos.x = (_distBetweenEnemy * sign) + _currentEnemy.transform.position.x;
        transform.position = pos;
        SetFlip(sign < 0);
    }

    public void ShowAnim(float inputX, float inputY)
    {
        _anim.SetBool("mover", Mathf.Abs(inputX) > 0 || Mathf.Abs(inputY) > 0);
    }

    public bool IsRight
    {
        get
        {
            return _isRigth;
        }
    }

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
    }
}

public class AnimStruct
{
    public string animName;
    public Enemy enemy;

    public AnimStruct(string anim, Enemy en)
    {
        animName = anim;
        enemy = en;
    }
}