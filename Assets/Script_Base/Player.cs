using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public Characterdatabase characterdatabase;

    public CharacterMain characterMain;

    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;

    private int selectedOption_P2 = 0;

    private Transform childTransform;

    public Light2D defense;

    public enum PlayerType
    {
        Player1,Player2
    }

    public PlayerType player;

    // Start is called before the first frame update
    void Start()
    {
        defense = GetComponentInChildren<Light2D>();
        //Invoke("Set", 2);
        if (player == PlayerType.Player1)
        {
            if (!PlayerPrefs.HasKey("selectedOption"))
            {
                selectedOption = 0;
            }

            else
            {
                Debug.Log("save&load");
                Load();
            }

            UpdateCharacter(selectedOption);
        }
        else
        {
            if (!PlayerPrefs.HasKey("selectedOption_P2"))
            {
                selectedOption_P2 = 0;
            }

            else
            {
                Debug.Log("save&load");
                Load();
            }

            UpdateCharacter(selectedOption_P2);
        }

        childTransform = GetComponentInChildren<Transform>();
        transform.position = childTransform.position;



    }

    private void UpdateCharacter(int selectedOption)
    {
        //Character character = characterdatabase.GetCharacter(selectedOption);

        GameObject player = characterMain.GetCharacter(selectedOption);

        player = Instantiate(player, this.transform);

        player.transform.position = transform.position;

       // artworkSprite.sprite = character.characterSprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
        selectedOption_P2 = PlayerPrefs.GetInt("selectedOption_P2");
        Debug.Log(selectedOption);
        Debug.Log(selectedOption_P2);
    }

    /*void Set()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();

        foreach (Transform t in transforms)
        {
            if (t != this.transform)
            {
                childTransform = t;
                break;
            }
        }

        if (childTransform != null)
        {
            transform.position = childTransform.position;
            Debug.Log("Child name: " + childTransform.name);
        }
        else
        {
            Debug.Log("No child found");
        }
    }*/
}
