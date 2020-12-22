using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MissNasty : MonoBehaviour
{
    public float minTimeToChangeState;
    public float maxTimeToChangeState;
    public float distanceToDetection;
    public float distanceToAttack;
    public float speed = 0.05f;

    private State _state = State.IDLE;
    private Animator _anim;
    private SoundManager _soundManager;
    private NavMeshAgent _agent;
    private float _time;
    private float _timeToChangeState;
    private Player _player;

    public enum State
    {
        IDLE, FOLLOW, ATTACK
    }


    private void Start()
    {
        _state = State.IDLE;
        _player = FindObjectOfType<Player>();
        _agent = GetComponent<NavMeshAgent>();
        _soundManager = FindObjectOfType<SoundManager>();        
        _anim = GetComponentInChildren<Animator>();
        _agent.speed = speed;

        CalculateTimeNextState();
    }

    private void CalculateTimeNextState()
    {
        _timeToChangeState = Random.Range(minTimeToChangeState, maxTimeToChangeState);
    }
    
    private bool CanNextState(State prevState, State nextState)
    {
        switch(prevState)
        {
            case State.FOLLOW:
                if (nextState == State.IDLE || nextState == State.ATTACK) return true;
                return false;

            case State.IDLE:
                if (nextState == State.FOLLOW) return true;
                return false;

            case State.ATTACK:
                if (nextState == State.IDLE) return true;
                return false;

            default:
                return false;
        }
    }

    private void SetNewState(State newState)
    {
        _state = newState;
    }

    void Update()
    {
        CheckStateMachine();        
    }

    private void CheckStateMachine()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }

        switch (_state)
        {
            case State.IDLE:
                _time += Time.deltaTime;
                if(_time > _timeToChangeState)
                {
                    if (!CanNextState(_state, State.FOLLOW)) return;

                    
                    if (CalculateDistance(_player.gameObject, gameObject) > distanceToDetection)
                    {
                        _agent.SetDestination(_player.transform.position);
                        SetNewState(State.FOLLOW);
                    }
                }
            break;

            case State.FOLLOW:
                
                if (CalculateDistance(_player.gameObject, gameObject) > distanceToAttack && CanNextState(_state, State.ATTACK))
                {
                    _agent.isStopped = true;
                    SetNewState(State.ATTACK);
                }
            break;

        }
    }

    private float CalculateDistance(GameObject target, GameObject destination)
    {
        var distance = Vector3.Distance(target.transform.position, destination.transform.position);
        Debug.Log("Distance: " +distance);
        return distance;
    }

    public void OnCompletedAnimation()
    {

    }
}
