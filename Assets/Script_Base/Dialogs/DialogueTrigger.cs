using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue(Collider2D hitCollider)
    {
        /*Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f);
        //FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
        if (hitColliders.Length > 0)
        {
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag.Equals("Player"))
                {
                    hitCollider.gameObject.GetComponentInChildren<DialogueManager>().StartDialogue(dialogue);
                }
            }
        }*/
        
        hitCollider.gameObject.GetComponentInChildren<DialogueManager>().StartDialogue(dialogue, hitCollider);
        


    }
}
