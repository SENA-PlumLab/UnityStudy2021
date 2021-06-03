using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{

    public float moveSpeed;
    private bool canMove;


    private Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            // �������� ������
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
        }
    }

    // �÷��̾ ������ ��� �����̰��ְ� ��.
    private void OnBecameVisible()
    {
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "KillPlane")
        {
            //Destroy(gameObject);

            gameObject.SetActive(false);
        }
    }

    //�Ⱥ��� ���� �������� �ʰ� ��
    private void OnEnable()
    {
        canMove = false;
    }
}
