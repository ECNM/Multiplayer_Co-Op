using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenful : MonoBehaviour
{
    public void Click()
    {
        AudioManager.Instance.PlaySFX("click-a");
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
