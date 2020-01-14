using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	private Animator _anim;
	private string _actualAnim = "Idle";
	private int _actualTipo = 0;
	private int hashIdle;
	private int _contador=0;

	void Start () {
		_anim = GetComponent<Animator>();					
	}
	
	// Update is called once per frame
	void Update () {	
		float inputX = Input.GetAxisRaw("Horizontal");
		float inputY = Input.GetAxisRaw("Vertical");

		if(Input.GetKeyDown(KeyCode.F)){				
			var anteriorTipo = _actualTipo;			
			_contador++;
			_actualTipo = 1;
			int newNumber = int.Parse(anteriorTipo.ToString() + _actualTipo.ToString() + _contador.ToString());		
			_anim.SetInteger("tipo",_actualTipo);
			_anim.SetInteger("contador",_contador);		
			_anim.SetInteger("codigo",newNumber);				
			_actualAnim = "Attack"+GetPreffix(1)+_contador;
			Debug.Log("Atacar: "+_actualAnim);		
						
			return;		
		}

		if(Input.GetKeyDown(KeyCode.G)){	
			var anteriorTipo = _actualTipo;					
			_contador++;
			_actualTipo = 2;
			int newNumber = int.Parse(anteriorTipo.ToString() + _actualTipo.ToString() + _contador.ToString());		
			_anim.SetInteger("tipo",_actualTipo);
			_anim.SetInteger("contador",_contador);		
			_anim.SetInteger("codigo",newNumber);			
			_actualAnim = "Attack"+GetPreffix(2)+_contador;		
			Debug.Log("Atacar: "+_actualAnim);							
			return;	
		}

		
		var info = _anim.GetCurrentAnimatorStateInfo(0);
		
		

		/*if(Input.GetKeyDown(KeyCode.H)){					
			_contador++;
			_anim.SetInteger("tipo",3);
			_anim.SetInteger("contador",_contador);	
			
			_currentAnim = "Attack"+GetPreffix(3)+1;				
			return;	
		}*/		
		
		
		//Debug.Log("Contador: "+_contador);

		if(info.IsName("Idle")){
			_anim.SetInteger("tipo",0);
			_anim.SetInteger("contador",0);	
			_actualAnim = "Idle";	
			_anim.SetInteger("codigo",0);		
			_contador=0;
			_actualTipo = 0;
			
			Debug.Log("Idle");
		}			
		
	}

	private string GetPreffix(int num){
		switch(num){
			case 1:
			return "C";
			case 2:
			return "T";
			case 3:
			return "P";
			default: return "C";
		}
	}

	public void ShowAnim(float inputX, float inputY){	
		//Debug.Log("INput x: "+Mathf.Abs(inputX));	
		_anim.SetBool("mover",Mathf.Abs(inputX)>0 || Mathf.Abs(inputY)>0);			
	}
}
