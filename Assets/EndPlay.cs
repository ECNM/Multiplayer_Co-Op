using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlay : MonoBehaviour
{
    public GlobleBool go;
    string text;
    // Update is called once per frame
    void Update()
    {
        text = go.textM;
    }

    public void Play()
    {
        AudioManager.Instance.PlayMusic(text);
    }
}
