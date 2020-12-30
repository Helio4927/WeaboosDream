using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    private AnimEvents _animEvents;
    public SoundManager _soundManager;


    private void Start()
    {
        _animEvents = GetComponentInParent<AnimEvents>();
        _soundManager = FindObjectOfType<SoundManager>();
    }
  
    protected void OnAnimationStart(string animName)
    {        
        _animEvents.OnAnimationStart(animName);
    }
    public void AuxShakeOn()
    {
        print("shakeon");
        FindObjectOfType<CameraShake>().ShakeHit();
    }

    public void AuxSonidoBlock()
    {
        _soundManager.PlaySoundCombat(0);
    }

    protected void OnAnimationCompleted(string animName)
    {
        _animEvents.OnAnimationCompleted(animName);
    }

    protected void HacerDano()
    {
        _animEvents.HacerDano();
    }

    protected void SetVulnerable(int vulnerable)
    {      
        _animEvents.SetVulnerable(vulnerable == 1);   // 0 es false y 1 es true       
    }

    protected void MoveAwayPlayer()
    {
        _animEvents.MoveAwayPlayer();  // 0 es false y 1 es true       
    }

    protected void HabilitarElAtaque(int valor)
    {
        _animEvents.HabilitarElAtaque(valor == 1);
    }
    protected void CallPushFromAnim()
    {
        _animEvents.CallPushFromAnim();
    }

    protected void DoBlockOnOff(int onOff)
    {
        _animEvents.DoBlockOnOff(onOff == 1);        
    }

    protected void CompletedFatality(string anim)
    {
        _animEvents.CompletedFatality(anim);
    }

    protected void CheckEnemyType()
    {
        _animEvents.CheckEnemyType();
    }
}