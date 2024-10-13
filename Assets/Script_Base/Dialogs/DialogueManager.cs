using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Animator animator;
    private GameObject gameObjectReferance;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        Debug.Log(gameObject.GetComponentInParent<Player>().player);
        if(gameObject.GetComponentInParent<Player>().player == Player.PlayerType.Player2)
        {
            gameObjectReferance = GameObject.Find("Dialog_Two");
            Debug.Log(gameObjectReferance.name);
            nameText = gameObjectReferance.GetComponentsInChildren<TMP_Text>()[0];
            dialogueText = gameObjectReferance.GetComponentsInChildren<TMP_Text>()[1];
            animator = gameObjectReferance.GetComponent<Animator>();
        }
        else
        {
            gameObjectReferance = GameObject.Find("Dialog_One");
            nameText = gameObjectReferance.GetComponentsInChildren<TMP_Text>()[0];
            dialogueText = gameObjectReferance.GetComponentsInChildren<TMP_Text>()[1];
            animator = gameObjectReferance.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartDialogue(Dialogue dialogue, Collider2D hitCollider)
    {
        Debug.Log("Starting Conversation With " + dialogue.name);
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(hitCollider);
        hitCollider.gameObject.GetComponent<PlayerControl>().enabled = false;
    }

    public void DisplayNextSentence(Collider2D hitCollider)
    {
        if (sentences.Count == 0)
        {
            hitCollider.gameObject.GetComponent<PlayerControl>().enabled = true;
            EndDialogue();
            //Debug.Log(sentences.Count);
            return;
            
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        //dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Debug.Log("End of conversation.");
    }
}
