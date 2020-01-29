using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemy : Enemy
{
    public bool debilitado;
    public int tiempoDebilitado;
    public void ShowSpecialAnim()
    {
        print("AQUIIIIIIIIIIIIIIIIIIIII");
        _agent.isStopped = true;
        _anim.speed = 0;
    }

    public void ActivarSpecialEnemy()
    {
        _agent.isStopped = false;
        _anim.speed = 1;
        _anim.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void DesActivarSpecialEnemy()
    {
        _agent.isStopped = true;
        _anim.speed = 0;
        _anim.GetComponent<SpriteRenderer>().enabled = false;

    }

    public void RecuperarseDeDebilidad()
    {
        debilitado = false;
        //idle vuelve a ser el normal de antes
    }

    public override void ShowHitAnim(string animName)
    {

        if (animName.Equals("P1P2"))
        {
            print(" showhitAnimdebilitar" + animName);
            CancelInvoke();
            debilitado = true;
            Invoke("RecuperarseDeDebilidad", tiempoDebilitado);
        }
        else
        {
            base.ShowHitAnim(animName);
        }
    }
    protected void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Y))
        {
            print("Activar");
            ActivarSpecialEnemy();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            print("Desactivar");
            DesActivarSpecialEnemy();
        }
    }

}
