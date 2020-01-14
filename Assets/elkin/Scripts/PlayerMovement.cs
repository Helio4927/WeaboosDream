using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody _rigid;
    public float speed = 1;

    void Start () {
        _rigid = GetComponentInChildren<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {

            /*var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var pos = new Vector2(ray.origin.x, ray.origin.y);
            Debug.Log("<color=green>CLick: "+pos+"</color>");*/

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            Debug.Log("<color=green>CLick</color>");
            if (hit.collider != null)
            {
                Debug.Log("Colisionando con: " + hit.collider.name);
            }

            /*if (_renderer.bounds.Contains(pos))
            {
                Debug.Log("<color=green>CLick: "+transform.parent.parent.name+"</color>");
                //var comp = GetComponentInParent<Enemy>();
                //if(comp) _player.Hit(GetComponentInParent<Enemy>(), _partName);
            }*/
        }

        var inputX = Input.GetAxisRaw("Horizontal");
        var inputY = Input.GetAxisRaw("Vertical");

        GetComponent<NavMeshAgent>().Move(transform.forward * Time.deltaTime * inputY * speed);
        GetComponent<NavMeshAgent>().Move(transform.right * Time.deltaTime * inputX * speed);
        /*var vel = _rigid.velocity;
        vel.x = speed * inputX * Time.deltaTime;
        vel.z = speed * (inputY * Time.deltaTime) / 2;
        _rigid.velocity = vel;*/
    }
}
