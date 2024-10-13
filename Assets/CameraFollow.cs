using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
   // public Transform target;
    private Transform childTransform;
    public GameObject Player;

    // Update is called once per frame
    void Start()
    {
        childTransform = this.transform;
        Invoke("Set", 2);
        
    }
    void Update()
    {
        Set();
        Vector3 newPos = new Vector3(childTransform.position.x, childTransform.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }

    /*void Set()
    {
        target = GameObject.Find("Adventurer(Clone)").transform;
    }*/

    void Set()
    {
        Transform[] transforms = Player.GetComponentsInChildren<Transform>();

        foreach (Transform t in transforms)
        {
            if (t != Player.transform)
            {
                childTransform = t;
                break;
            }
        }

        /*if (childTransform != null)
        {
            Debug.Log("Child name: " + childTransform.name);
        }
        else
        {
            Debug.Log("No child found");
        }*/
    }
}
