using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;

    public float bounceForce;

    public GameObject deathSplosion;

    // Start is called before the first frame update
    void Start()
    {
        //StmopBox가 Player의 자식이므로
        playerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);

            Instantiate(deathSplosion, collision.transform.position, collision.transform.rotation);

            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, bounceForce, 0f);
        }

        if(collision.tag == "Boss")
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, bounceForce, 0f);
            collision.transform.parent.GetComponent<Boss>().takeDamage = true;
        }

    }
}
