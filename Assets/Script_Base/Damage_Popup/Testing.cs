using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Testing : MonoBehaviour
{

    private void Start()
    {
        //Debug.Log(Resources.Load<GameAsset>("GameAssets"));
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool isCriticalHit = Random.Range(0, 100) < 30;
            DamagePopup.Create(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -2), 100, isCriticalHit);
            
        }
    }
}
