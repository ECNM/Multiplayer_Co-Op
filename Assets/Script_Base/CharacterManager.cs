using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class CharacterManager : MonoBehaviour
{
    public Characterdatabase characterdatabase;

    public TMP_Text nameText;

    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;

    private int sceneID = 0;

    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
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

    public void NextOption()
    {
        AudioManager.Instance.PlaySFX("click-a");

        selectedOption++;

        if(selectedOption >= characterdatabase.characterCount)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    public void BackOption()
    {
        AudioManager.Instance.PlaySFX("click-a");

        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = characterdatabase.characterCount - 1;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterdatabase.GetCharacter(selectedOption);

        animator.runtimeAnimatorController = character.animatorController;

        artworkSprite.sprite = character.characterSprite;

        nameText.text = character.nameCharacter;

    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
        Debug.Log(selectedOption);
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);

        Debug.Log("save");
        Debug.Log(selectedOption);
    }

    public void ChangeScene(int sceneID)
    {
        AudioManager.Instance.PlaySFX("click-b");

        Invoke("Change",0.3f);

        this.sceneID = sceneID;

       // SceneManager.LoadScene(sceneID);
    }

    private void Change()
    {
        SceneManager.LoadScene(this.sceneID);
    }

    
}
