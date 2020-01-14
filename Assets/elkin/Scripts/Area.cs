using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

	public Transform center;
	public bool hasEnemy = false;
	private Player _player;
	public float distance = 3;
	void Start () {
		_player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		// Cast a ray straight down.
		var dir = _player.IsRight ? Vector3.right : Vector3.left; 
        RaycastHit2D hit = Physics2D.Raycast(center.position, dir, distance);	

		if(hit){
			Debug.DrawLine(center.position,hit.point, Color.green);
			//Debug.Log("Hit: "+hit.transform.name);			
		}else
		{			
			Debug.DrawLine(center.position,center.position + dir * distance, Color.red);
		}
		hasEnemy = hit.collider != null;					
	}
}
