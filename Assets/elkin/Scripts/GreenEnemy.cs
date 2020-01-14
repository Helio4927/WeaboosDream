using UnityEngine;
using UnityEngine.AI;

public class GreenEnemy : Enemy
{

    [SerializeField] private float _tiempoVulnerable = 1;

    public override void Start()
    {
        base.Start();
        soyVulnerable = false;
    }

    public override void SetVulnerable(bool vulnerable)
    {
        //base.SetVulnerable(vulnerable);
        //Invoke("HacerInvulnerable", _tiempoVulnerable);
    }

    public override void ShowAnimBeBlocked()
    {
        soyVulnerable = true;
        base.ShowAnimBeBlocked();        
        Invoke("HacerInvulnerable", _tiempoVulnerable);
    }

    private void HacerInvulnerable()
    {
        soyVulnerable = false;
    }

    protected override bool CanSetNextState(State current, State next)
    {
        switch (current)
        {
            case State.IDLE:
                if (next == State.BLOCK || next == State.ATTACK || next == State.FOLLOW || next == State.WAIT || next == State.HURT || next == State.DEATH)
                {
                    _currentState = next;
                    return true;
                }
                break;

            case State.ATTACK:
                if (next == State.IDLE || next == State.BLOCK || next == State.DEATH || next == State.BEBLOCKED)
                {
                    _currentState = next;

                    return true;
                }
                break;

            case State.FOLLOW:
                if (next == State.BLOCK || next == State.IDLE || next == State.ATTACK || next == State.WAIT || next == State.HURT || next == State.DEATH)
                {
                    _currentState = next;
                    return true;
                }
                break;

            case State.WAIT:
                if (next == State.BLOCK || next == State.FOLLOW || next == State.ATTACK || next == State.HURT || next == State.DEATH)
                {
                    _currentState = next;
                    return true;
                }
                break;

            case State.HURT:
                if (next == State.WAIT || next == State.HURT || next == State.DEATH || next == State.IDLE)
                {
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
}
