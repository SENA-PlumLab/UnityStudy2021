using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour
{
    private Vector3 startPosition; //position 담는 변수
    private Quaternion startRotation; //ratation 담는 변수
    private Vector3 startLocalScale; //scale 담는 변수 

    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startLocalScale = transform.localScale;

        //rigidbody 없는 오브젝트도 있으므로.
        if(GetComponent<Rigidbody2D>() != null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetObject()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startLocalScale;

        if(myRigidbody != null)
        {
            myRigidbody.velocity = Vector3.zero; //(0, 0, 0)
        }
    }


}
