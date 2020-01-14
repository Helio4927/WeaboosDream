using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    public GameObject deathSprite;
    public int poolSize = 3;
    private Queue<GameObject> _pool;
	void Start () {
        _pool = new Queue<GameObject>();

		for(int i=0; i<poolSize; i++)
        {
            var g = Instantiate(deathSprite);
            g.SetActive(false);
            g.transform.position = deathSprite.transform.position;
            g.transform.parent = gameObject.transform;
            _pool.Enqueue(g);
        }

        Destroy(deathSprite);
	}
	
    public GameObject GetSpriteDeath()
    {
        var g = _pool.Dequeue();
        Debug.Assert(g!=null,"Se acabaron los gameobjects de la muerte, aumente el size del pool.");
        return g;
    }
}
