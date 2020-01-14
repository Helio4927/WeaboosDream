using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorItemsInventario : MonoBehaviour {

    public inventoryManager gestorInventario;
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.gameObject.name);
        if (other.gameObject.GetComponent<infoItem>())
        {
            gestorInventario.currentItem = other.gameObject;
        }
    }
}
