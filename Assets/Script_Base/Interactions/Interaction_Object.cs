using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Interaction_Object : MonoBehaviour
{
    public Image ui_interaction;
    private PlayerInput player;
    int count;
    int count_2;
    int triggerCount;
    private TradeAttack trade;
    LayerMask mask;

    public bool boolalogue;
    public bool checkbuy;
    // Start is called before the first frame update
    void Start()
    {
        ui_interaction.enabled = false;
        trade = GetComponent<TradeAttack>();
        mask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, (GetComponent<BoxCollider2D>().size * transform.localScale), 0f, mask);
        if (hitColliders.Length > 0)
        {
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag.Equals("Player"))
                {
                    Debug.Log("Trigged");
                    if (hitCollider.gameObject.GetComponentInParent<Player>().player == Player.PlayerType.Player2 && hitCollider.gameObject.GetComponent<PlayerInput>().actions["Trigger"].WasPerformedThisFrame())
                    {
                        Dialogue_One(hitCollider);
                        trade.Trade(hitCollider);
                    }
                    if(hitCollider.gameObject.GetComponentInParent<Player>().player == Player.PlayerType.Player1 && hitCollider.gameObject.GetComponent<PlayerInput>().actions["Trigger2"].WasPerformedThisFrame())
                    {
                        Dialogue_Two(hitCollider);
                        trade.Trade(hitCollider);
                    }
                }
            }
        }
    }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, (GetComponent<BoxCollider2D>().size * transform.localScale));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
            triggerCount++;
            ui_interaction.enabled = true;
                Debug.Log("Trigger");
                //GetComponent<DialogueTrigger>().TriggerDialogue();
                if(triggerCount == 1)
            {
                gameObject.GetComponentInChildren<UISpriteAnimation>().Func_PlayUIAnim();
            }
                
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
            triggerCount--;
            if (triggerCount == 0)
            {
                ui_interaction.enabled = false;
                gameObject.GetComponentInChildren<UISpriteAnimation>().Func_StopUIAnim();
            }
                
            }
        }

        private void Dialogue_One(Collider2D hitCollider)
    {
        Debug.Log(hitCollider.gameObject.name);
        count += 1;
            //Debug.Log(count);
            if (count == 1)
            {
                GetComponent<DialogueTrigger>().TriggerDialogue(hitCollider);
                //Debug.Log("1");
            }
            if (count >= 2)
            {
                hitCollider.gameObject.GetComponentInChildren<DialogueManager>().DisplayNextSentence(hitCollider);
                if (hitCollider.gameObject.GetComponentInChildren<DialogueManager>().sentences.Count == 0)
                {
                    count = 0;
                    checkbuy = true;
                //Debug.Log(FindAnyObjectByType<DialogueManager>().sentences.Count);
            }
            }
            if (count == 0)
            {
                hitCollider.gameObject.GetComponentInChildren<DialogueManager>().DisplayNextSentence(hitCollider);
                boolalogue = true;
            }
    }
        private void Dialogue_Two(Collider2D hitCollider)
    {
        Debug.Log(hitCollider.gameObject.name);
        count_2 += 1;
            //Debug.Log(count);
            if (count_2 == 1)
            {
                GetComponent<DialogueTrigger>().TriggerDialogue(hitCollider);
                //Debug.Log("1");
            }
            if (count_2 >= 2)
            {
                hitCollider.gameObject.GetComponentInChildren<DialogueManager>().DisplayNextSentence(hitCollider);
                if (hitCollider.gameObject.GetComponentInChildren<DialogueManager>().sentences.Count == 0)
                {
                    count_2 = 0;
                    checkbuy = true;
                    //Debug.Log(FindAnyObjectByType<DialogueManager>().sentences.Count);
                }
            }
            if (count_2 == 0)
            {
                hitCollider.gameObject.GetComponentInChildren<DialogueManager>().DisplayNextSentence(hitCollider);
                boolalogue = true;
        }
        }
} 
