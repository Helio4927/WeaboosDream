using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    public int[] idFraseRetrato;
    public int delay;


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        FindObjectOfType<DialogueManager>().dialogueTriggerRef = this;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colision de" + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            if (dialogue.diagKey == "PuertaNegra")
            {
                if (GameObject.Find("AreaInteracion01 Muerto").GetComponent<InfoExam>().cadaverExam)
                {
                    Debug.Log("Es el player, lanzamos conversación" + " en " + delay + " segundos");
                    Invoke("DialogoEnTriggerEnter", delay);
                }
                else
                {
                    Debug.Log("No has registrado el cadaver, asi que nada.");
                }
            }
            else
            {
                Debug.Log("Es el player, lanzamos conversación" + " en " + delay + " segundos");
                Invoke("DialogoEnTriggerEnter", delay);
            }
          
        }

    }

    public void DialogoEnTriggerEnter()
    {
        FindObjectOfType<DialogueManager>().dialogueTriggerRef = this;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

        this.gameObject.GetComponent<Collider>().enabled = false;
    }
}
