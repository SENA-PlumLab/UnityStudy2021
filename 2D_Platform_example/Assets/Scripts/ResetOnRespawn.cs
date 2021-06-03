using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour
{
    private Vector3 startPosition; //position ��� ����
    private Quaternion startRotation; //ratation ��� ����
    private Vector3 startLocalScale; //scale ��� ���� 

    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startLocalScale = transform.localScale;

        //rigidbody ���� ������Ʈ�� �����Ƿ�.
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
