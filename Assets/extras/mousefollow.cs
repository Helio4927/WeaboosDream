using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousefollow : MonoBehaviour {

    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
            //mousePosition = Input.mousePosition;
            //mousePosition = Camera.main.ScreenToViewportPoint(mousePosition);
            transform.position = Input.mousePosition;
    }
}
