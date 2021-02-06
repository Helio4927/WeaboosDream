using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour {
	
    public virtual void DoBlockOnOff(bool v)
    {
        Debug.LogError("No se ha implementado DoBLockonOff");
    }

    public virtual void OnAnimationStart(string animName){
		Debug.LogError("No se ha implementado OnAnimationStart");
	}

	public virtual void OnAnimationCompleted(string animName){
		Debug.LogError("No se ha implementado OnAnimationCompleted");
	}

    public virtual void HacerDano()
    {
        Debug.LogError("No se ha implementado HacerDano" + " :"+gameObject.name);
    }

    public virtual void HitFromFloorHacerDano()
    {
        Debug.LogError("No se ha implementado HitFromFloorHacerDano" + " :" + gameObject.name);
    }

    public virtual void SetVulnerable(bool vulnerable)
    {
        Debug.LogError("No se ha implementado SetVulnerable");
    }

    public virtual void MoveAwayPlayer()
    {
        Debug.LogError("No se ha implementado MoveAwayPlayer");
    }
   
    public virtual void CallPushFromAnim()
    {
        Debug.LogError("No se ha implementado CallPushFromAnim");
    }

    public virtual void CompletedFatality(string anim)
    {
        Debug.LogError("No se ha implementado CompletedFatality");
    }

    public virtual void HabilitarElAtaque(bool valor)
    {
        Debug.LogError("No se ha implementado HabilitarElAtaque");
    }

    public virtual void CheckEnemyType()
    {
        Debug.LogError("No se ha implementado CheckEnemyType");
    }
}
