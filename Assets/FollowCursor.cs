using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Camera.main == null) return;
        Vector3 auxVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        auxVector.z = 0;
        transform.position = auxVector; 
    }
}
