using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            
            PlayerInputManager.instance.JoinPlayer();
        }
    }

}