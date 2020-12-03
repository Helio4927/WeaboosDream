using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemy : Enemy
{
	public void ShowSpecialAnim(){
		_agent.isStopped = true;
		_anim.speed = 0;
	}
	
}
