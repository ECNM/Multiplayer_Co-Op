using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvasTwo;
    public void Click()
    {
        AudioManager.Instance.PlaySFX("tap-a");
        canvas.SetActive(false);
        canvasTwo.SetActive(true);
        
    }
}
