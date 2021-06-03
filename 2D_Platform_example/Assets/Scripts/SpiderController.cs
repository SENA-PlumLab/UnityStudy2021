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
            // 왼쪽으로 움직임
            myRigidBody.velocity = new Vector3(-moveSpeed, myRigidBody.velocity.y, 0f);
        }
    }

    // 플레이어가 떠나도 계속 움직이고있게 함.
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

    //안보일 때는 움직이지 않게 함
    private void OnEnable()
    {
        canMove = false;
    }
}
