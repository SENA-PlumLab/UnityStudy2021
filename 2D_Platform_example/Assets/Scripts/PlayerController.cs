using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private float activeMoveSpeed;

    public bool canMove;

    public Rigidbody2D myrigidbody;

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

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
                                            //체크 기준점 / 체크 범위 / 체크할 레이어 (범위안에 체크할 레이어가 들어오는지)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //knockback
        if(knockbackCounter <= 0 && canMove)  //좌우조작,점프는 넉백이 아닌 상태에서 가능
        {
            if (onPlatform)
            {
                activeMoveSpeed = moveSpeed * onPlatformSpeedModifier;
            }
            else
            {
                activeMoveSpeed = moveSpeed;
            }

            if (Input.GetAxisRaw("Horizontal") > 0f) //Right 양수
            {
                myrigidbody.velocity = new Vector3(activeMoveSpeed, myrigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f); // 우측을 보고 있을 때, scale을 1, 1, 1로 함

            }
            else if (Input.GetAxisRaw("Horizontal") < 0f) // Left 음수
            {
                myrigidbody.velocity = new Vector3(-activeMoveSpeed, myrigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f); // 좌측을 보고 있을 때, scale을 -1, 1, 1로 함 (좌우반전)
            }
            else
            {
                myrigidbody.velocity = new Vector3(0f, myrigidbody.velocity.y, 0f);
            }

            //땅에 닿아있는지
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myrigidbody.velocity = new Vector3(myrigidbody.velocity.x, jumpSpeed, 0f);
                jumpSound.Play();
            }
        }

        if(knockbackCounter > 0 ) //넉백해야할 때
        {
            knockbackCounter -= Time.deltaTime;

            if(transform.localScale.x > 0) //플레이어가 우측을 보고 있을 때
            {
                //왼쪽으로 튕겨짐
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
        // Mathf.abs -> 절댓값 가져옴
        // 사용이유: 애니메이션 파라미터에 스피드<0.1이면 서있게 했는데,
        // velocity에서는 음수값이면 왼쪽으로 움직이는 것이기 때문.
        myAnim.SetBool("Grounded", isGrounded);

        if(myrigidbody.velocity.y < 0) //떨어지고 있을 때에만
        {
            stompBox.SetActive(true); //stompBox 활성화
        } else
        {
            stompBox.SetActive(false); // 위로 올라거나 걷고있을 때는 stompBox 비활성화
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
            //깃발의 x, y, z 값을 respawnPosition이라는 vector3객체에 넣음
        }


    }

    public void Knockback()
    {
        knockbackCounter = knockbackLength;
        invincibilityCounter = invincibilityLength;
        theLevelManager.invincible = true;
    }

    
    //Collider2D와 Collision2D는 다르다
    void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
            //player의 위치 부모를 other(MovingPlatform태그 가진 것)으로 정함

            onPlatform = true;
        } 
    
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
            //player 위치 부모를 없앰
            onPlatform = false;
        }
    }
}
