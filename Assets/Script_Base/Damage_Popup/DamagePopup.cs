using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMeshPro;
    private float disappearTimer;
    private Color textColor;
    private const float DISAPPEAR_TIMER_MAX = 0.5f;
    private Vector3 moveVector;
    private static int sortingOrder;
    
    //Create Damage Popup
    public static  DamagePopup Create(Vector3 posiotion, int damageAmount, bool criticalHit)
    {

            Transform damagePopupTransform = Instantiate(GameAsset.i.dfDamagePopup, posiotion, Quaternion.identity);

            DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
            damagePopup.Setup(damageAmount, criticalHit);
            return damagePopup;
        
    }

    private void Awake()
    {
        textMeshPro = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool criticalHit)
    {
        textMeshPro.SetText(damageAmount.ToString());
        if (!criticalHit)
        {
            textMeshPro.fontSize = textMeshPro.fontSize;
            textColor = textMeshPro.color;
        }
        else
        {
            textMeshPro.fontSize = textMeshPro.fontSize + 2;
            ColorUtility.TryParseHtmlString("#e32636", out textColor);
        }
        textMeshPro.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        Debug.Log(damageAmount);
        sortingOrder++;
        textMeshPro.sortingOrder = sortingOrder;
        moveVector = new Vector3(0.7f, 1) * 2f;
    }

    private void Update()
    {
        disappearTimer -= Time.deltaTime;
        if(disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        if(disappearTimer < 0)
        {
            float disappearSpeed = 5f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMeshPro.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
    }
}
