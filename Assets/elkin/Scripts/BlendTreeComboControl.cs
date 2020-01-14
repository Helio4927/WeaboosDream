using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreeComboControl : MonoBehaviour {

    Animator anim;
    int healthHash;
    int attackHash;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // 
    private void Update()
    {
        // Attack Key para hacer los combos*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Attack");
        }

        // Up & Down para modificar Health. Para testear como varia la animacion al cambiar el parametro desde el codigo.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetFloat("Health", anim.GetFloat("Health") + 1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetFloat("Health", anim.GetFloat("Health") - 1);
        }
    }

    // * El combo 3 tiene una animacion de base solo porque no estaba implementado en el proyecto original, 

}
