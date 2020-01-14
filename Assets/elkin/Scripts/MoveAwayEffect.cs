using UnityEngine;

public class MoveAwayEffect : MonoBehaviour {
        
    [SerializeField] private float _timeMoveAway = 1;
    [SerializeField] private float _force = 400;
      	
	
	void Update () {
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    //StartCoroutine("ScaleIn");   
        //    MoveAway(Vector3.right);
        //}
    }

    public void MoveAway(Vector3 dir,Enemy enemigo)
    {

        //Debug.Log("Move");
        enemigo.enabled = false;
        enemigo.GetComponent<Rigidbody>().AddForce(dir * _force);
        Invoke("ActivateComponent", _timeMoveAway);      
    }

    public void MoveAway(Vector3 dir, Player player)
    {
        //Debug.Log("Move");
        player.enabled = false;
        player.GetComponent<Rigidbody>().AddForce(dir * _force);
        Invoke("ActivateComponentPlayer", _timeMoveAway);
    }

    private void ActivateComponent()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Enemy>().enabled = true;
    }

    private void ActivateComponentPlayer()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Player>().enabled = true;
    }
}
