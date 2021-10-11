using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;
    public Vector3 offset = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        CalcNewPos();
    }

    void CalcNewPos()
    {
        if(objectToFollow)
        {
            transform.position = new Vector3(objectToFollow.transform.position.x + offset.x, objectToFollow.transform.position.y + offset.y, transform.position.z + offset.z);
        }     
        else
        {
            Debug.Log("Warning, " + name + " needs an object to follow!");
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        CalcNewPos();
    }

}
