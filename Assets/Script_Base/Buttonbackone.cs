using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonbackone : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvasTwo;
    public void Click()
    {
        AudioManager.Instance.PlaySFX("click-b");
        canvas.SetActive(true);
        canvasTwo.SetActive(false);

    }
}
