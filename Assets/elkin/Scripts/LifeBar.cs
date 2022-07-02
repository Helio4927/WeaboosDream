using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour {
	public float totalHp = 10;
	public float initHp = 10;
	public Transform spriteBar;
	
	void Start () {
        if (spriteBar == null)
        {
            Debug.Log("<color=red>No se encontro Sprite para la barra de vida</color>");
        }
        else
        {
            spriteBar.localScale = new Vector3(initHp, 1, 1);
        }		
	}
	
    public bool IsGoingToDie(float hitValue)
    {
        var tempInitHp = initHp;
        tempInitHp -= hitValue;
        return tempInitHp <= 0;
    }

	public bool UpdateHp(float hitValue){     
		initHp-=hitValue;
        if (initHp > totalHp)
        {
            initHp = totalHp;
        }
        if (spriteBar == null)
        {
            return initHp > 0;
        }

		if(initHp<=0){
			initHp = 0;	
			spriteBar.parent.gameObject.SetActive(false);		
		}
		spriteBar.localScale = new Vector3(initHp,1,1);
        return initHp > 0;
    }

    public bool IsAlive
    {
        get
        {
            return initHp > 0;
        }
    }
}
