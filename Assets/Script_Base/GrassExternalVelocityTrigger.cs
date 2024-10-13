using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassExternalVelocityTrigger : MonoBehaviour
{
    private GrassVelocityContrloller grassVelocityContrloller;

    private GameObject[] _player;

    private Material material;

    private float playerRB;

    private bool easeInCoroutineRunning;
    private bool easeOutCoroutineRunning;

    private int _externalInfluence = Shader.PropertyToID("_ExternalInfluence");

    private float _startingXVelocity;
    private float _velocityLastFrame;
    private int index;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        _startingXVelocity = material.GetFloat(_externalInfluence);
    }

    private void Update()
    {
        _player = GameObject.FindGameObjectsWithTag("Player");
        index = FindIndex(_player);
        Debug.Log("Index" + index);
        playerRB = _player[index].GetComponent<PlayerControl>().virVelocity;
        grassVelocityContrloller = GetComponentInParent<GrassVelocityContrloller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == _player[index])
        {
            
            if(!easeOutCoroutineRunning && Mathf.Abs(playerRB) > Mathf.Abs(grassVelocityContrloller.VelocityThreshold))
            {
                //Debug.Log("trigged" + playerRB);
                StartCoroutine(EaseIn(playerRB * grassVelocityContrloller.ExternalInfluenceStrength));
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == _player[index])
        {
            //Debug.Log("trigged");
            StartCoroutine(EaseOut());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject == _player[index])
        {
            if (Mathf.Abs(_velocityLastFrame) > Mathf.Abs(grassVelocityContrloller.VelocityThreshold) &&
                Mathf.Abs(playerRB) < Mathf.Abs(grassVelocityContrloller.VelocityThreshold))
            {
                StartCoroutine(EaseOut());
            }

            else if (Mathf.Abs(_velocityLastFrame) < Mathf.Abs(grassVelocityContrloller.VelocityThreshold) &&
                Mathf.Abs(playerRB) > Mathf.Abs(grassVelocityContrloller.VelocityThreshold))
            {
                StartCoroutine(EaseIn(playerRB * grassVelocityContrloller.ExternalInfluenceStrength));
            }

            else if (!easeInCoroutineRunning && !easeOutCoroutineRunning &&
                Mathf.Abs(playerRB) > Mathf.Abs(grassVelocityContrloller.VelocityThreshold))
            {
                grassVelocityContrloller.InfluenceGrass(material, playerRB * grassVelocityContrloller.ExternalInfluenceStrength);
            }
        }
    }

    private IEnumerator EaseIn(float XVelocity)
    {
        easeInCoroutineRunning = true;

        float elapsedTime = 0f;
        while(elapsedTime < grassVelocityContrloller.EaseInTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedAmount = Mathf.Lerp(_startingXVelocity, XVelocity, (elapsedTime / grassVelocityContrloller.EaseInTime));
            grassVelocityContrloller.InfluenceGrass(material, lerpedAmount);

            yield return null;
        }

        easeInCoroutineRunning = false;
    }

    private IEnumerator EaseOut()
    {
        easeOutCoroutineRunning = true;

        float currentXInfluence = material.GetFloat(_externalInfluence);
        float elapsedTime = 0f;
        while (elapsedTime < grassVelocityContrloller.EaseOutTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedAmount = Mathf.Lerp(currentXInfluence, _startingXVelocity, (elapsedTime / grassVelocityContrloller.EaseOutTime));
            Debug.Log(lerpedAmount);
            grassVelocityContrloller.InfluenceGrass(material, lerpedAmount);

            yield return null;
        }

        easeOutCoroutineRunning = false;
    }

    private int FindIndex(GameObject[] player)
    {
        if (player == null || player.Length == 0)
        {
            Debug.LogError("The array is empty or null");
            return int.MaxValue;
        }

        float minValue = Vector2.Distance(player[0].transform.position, transform.position);
        int index = 0;
        for (int i = 1; i < player.Length; i++)
        {
            if (Vector2.Distance(player[i].transform.position, transform.position) < minValue)
            {
                minValue = Vector2.Distance(player[i].transform.position, transform.position);
                index = i;
            }

        }

        return index;
    }
}
