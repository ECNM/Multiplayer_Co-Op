using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPlay : MonoBehaviour
{
    public GameObject[] checkobj;
    

    // Update is called once per frame
    void Update()
    {
        if (checkobj != null)
        {
            if (checkobj[0].GetComponent<TMPro.TMP_Text>().text == checkobj[1].GetComponent<TMPro.TMP_Text>().text)
            {
                GetComponent<Button>().enabled = false;
            }
            else
            {
                GetComponent<Button>().enabled = true;
            }
        }
        else
        {
            Debug.LogError("myObject is null");
        }
        
    }
}
