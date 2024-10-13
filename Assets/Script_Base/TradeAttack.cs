using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeAttack : MonoBehaviour
{
    private Interaction_Object intera;
    public GameObject go;
    public bool buy;
    public bool stone;
    void Start()
    {
        intera = GetComponent<Interaction_Object>();
        buy = true;
        stone = true;
    }

    public void Trade(Collider2D hitCollider)
    {
        if (intera.boolalogue && intera.checkbuy)
        {
            if (gameObject.tag.Equals("Wizard") && buy)
            {
                if (hitCollider.gameObject.GetComponent<CoinAndSoul>().soul >= 50)
                {
                    hitCollider.gameObject.GetComponent<CoinAndSoul>().soul -= 50;
                    GetComponent<AttackWizard>().enabled = true;
                    AudioManager.Instance.PlaySFX("Up");
                    buy = false;
                    intera.checkbuy = false;
                }
                else
                {
                    return;
                }
                
            }
            if (gameObject.tag.Equals("Nena"))
            {
                if(hitCollider.gameObject.GetComponent<CoinAndSoul>().countCoin >= 35)
                {
                    hitCollider.gameObject.GetComponent<CoinAndSoul>().countCoin -= 35;
                    hitCollider.gameObject.GetComponent<PlayerControl>().damageP += 2;
                    AudioManager.Instance.PlaySFX("Up");
                    intera.checkbuy = false;
                }
                else
                {
                    return;
                }
                
            }
            if (gameObject.tag.Equals("Blessing"))
            {
                if (hitCollider.gameObject.GetComponent<CoinAndSoul>().soul >= 20)
                {
                    hitCollider.gameObject.GetComponent<CoinAndSoul>().soul -= 20;
                    hitCollider.gameObject.GetComponent<PlayerControl>().reMana -= 0.5f;
                    AudioManager.Instance.PlaySFX("Up");
                    intera.checkbuy = false;
                }
                else
                {
                    return;
                }
            }

            if (gameObject.tag.Equals("Stone") && stone)
            {
                if (hitCollider.gameObject.GetComponent<CoinAndSoul>().soul >= 100)
                {
                    hitCollider.gameObject.GetComponent<CoinAndSoul>().soul -= 100;
                    go.SetActive(true);
                    GameObject.FindGameObjectsWithTag("Manage")[0].GetComponent<GlobleBool>().count += 1;
                    AudioManager.Instance.PlaySFX("Up");
                    intera.checkbuy = false;
                    stone = false;
                }
                else
                {
                    return;
                }
            }
        }
    }
  
}
