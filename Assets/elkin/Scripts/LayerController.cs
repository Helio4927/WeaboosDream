using UnityEngine;

public class LayerController : MonoBehaviour {
    private Transform _player;
    private SpriteRenderer _spRenderer;
    private Camera _cam;

    private void Awake()
    {
        _player = FindObjectOfType<Player>().transform;    
        _spRenderer = GetComponent<SpriteRenderer>();
        _cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        var disPlayer = Mathf.Abs(_cam.transform.position.z - _player.position.z);
        var disEnemy = Mathf.Abs(_cam.transform.position.z - transform.position.z);
        _spRenderer.sortingOrder = disPlayer > disEnemy ? 3 : 1;
    }
}
