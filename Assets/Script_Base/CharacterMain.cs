using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterMain : ScriptableObject
{
    public GameObject[] gameObjects;

    public int characterCount
    {
        get
        {
            return gameObjects.Length;
        }
    }

    public GameObject GetCharacter(int index)
    {
        return gameObjects[index];
    }
}
