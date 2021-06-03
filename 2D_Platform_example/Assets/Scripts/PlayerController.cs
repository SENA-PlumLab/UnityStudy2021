using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private float activeMoveSpeed;

    private Rigidbody2D myrigidbody;

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;

    private Animator myAnim;

    public Vector3 respawnPosition;

    public LevelManager theLevelManager;

    public GameObject stompBox;

    public float knockbackForce;
    public float knockbackLength;
    private float knockbackCounter;

    public float invincibilityLength;
    private float invincibilityCounter;

    public AudioSource jumpSound;
    public AudioSource hurtSound;

    private bool onPlatform;
    public float onPlatformSpeedModifier;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        respawnPosition = transform.position;

        theLevelManager = FindObjectOfType<LevelManager>();

        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
                                            //üũ ������ / üũ ���� / üũ�� ���̾� (�����ȿ� üũ�� ���̾ ��������)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //knockback
        if(knockbackCounter <= 0)  //�¿�����,������ �˹��� �ƴ� ���¿��� ����
        {
            if (onPlatform)
            {
                activeMoveSpeed = moveSpeed * onPlatformSpeedModifier;
            }
            else
            {
                activeMoveSpeed = moveSpeed;
            }

            if (Input.GetAxisRaw("Horizontal") > 0f) //Right ���
            {
                myrigidbody.velocity = new Vector3(activeMoveSpeed, myrigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f); // ������ ���� ���� ��, scale�� 1, 1, 1�� ��

            }
            else if (Input.GetAxisRaw("Horizontal") < 0f) // Left ����
            {
                myrigidbody.velocity = new Vector3(-activeMoveSpeed, myrigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f); // ������ ���� ���� ��, scale�� -1, 1, 1�� �� (�¿����)
            }
            else
            {
                myrigidbody.velocity = new Vector3(0f, myrigidbody.velocity.y, 0f);
            }

            //���� ����ִ���
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myrigidbody.velocity = new Vector3(myrigidbody.velocity.x, jumpSpeed, 0f);
                jumpSound.Play();
            }
        }

        if(knockbackCounter > 0 ) //�˹��ؾ��� ��
        {
            knockbackCounter -= Time.deltaTime;

            if(transform.localScale.x > 0) //�÷��̾ ������ ���� ���� ��
            {
                //�������� ƨ����
                myrigidbody.velocity = new Vector3(-knockbackForce, knockbackForce, 0f); 
            }
            else
            {
                myrigidbody.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
            }
        }
        
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }

        if(invincibilityCounter <= 0)
        {
            theLevelManager.invincible = false;
        }


        myAnim.SetFloat("Speed", Mathf.Abs(myrigidbody.velocity.x));
        // Mathf.abs -> ���� ������
        // �������: �ִϸ��̼� �Ķ���Ϳ� ���ǵ�<0.1�̸� ���ְ� �ߴµ�,
        // velocity������ �������̸� �������� �����̴� ���̱� ����.
        myAnim.SetBool("Grounded", isGrounded);

        if(myrigidbody.velocity.y < 0) //�������� ���� ������
        {
            stompBox.SetActive(true); //stompBox Ȱ��ȭ
        } else
        {
            stompBox.SetActive(false); // ���� �ö�ų� �Ȱ����� ���� stompBox ��Ȱ��ȭ
        }
    }


    private void OnTriggerEnter2D(Collider2D others) 
    {
        if(others.tag == "KillPlane")
        {
            //gameObject.SetActive(false);

            //transform.position = respawnPosition;

            theLevelManager.Respawn();

            //theLevelManager.healthCount = 0;

        }

        if(others.tag == "Checkpoint")
        {
            respawnPosition = others.transform.position; 
            //����� x, y, z ���� respawnPosition�̶�� vector3��ü�� ����
        }


    }

    public void Knockback()
    {
        knockbackCounter = knockbackLength;
        invincibilityCounter = invincibilityLength;
        theLevelManager.invincible = true;
    }

    
    //Collider2D�� Collision2D�� �ٸ���
    void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
            //player�� ��ġ �θ� other(MovingPlatform�±� ���� ��)���� ����

            onPlatform = true;
        } 
    
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
            //player ��ġ �θ� ����
            onPlatform = false;
        }
    }
}
