using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    private Interaction_Object intera;
    private DialogueTrigger dialogueOld;

    private void Start()
    {
        intera = GetComponent<Interaction_Object>();
        dialogueOld = GetComponent<DialogueTrigger>();
}

    // Update is called once per frame
    void Update()
    {
        if (intera.boolalogue)
        {
            dialogueOld.dialogue = dialogue;
        }
    }
}
