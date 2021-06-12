using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool bossActive;

    public float timeBetweenDrops;
    private float timeBetweenDropStore;
    private float dropCount;

    public float waitForPlatforms;
    private float platformCount;

    public Transform leftPoint;
    public Transform rightPoint;
    public Transform dropSawSpawnPoint;

    public GameObject dropSaw;

    public GameObject theBoss;
    public bool bossRight;

    public GameObject rightPlatform;
    public GameObject leftPlatform;

    public bool takeDamage;

    public int startingHealth;
    private int currnetHealth;

    public GameObject levelExit;

    private CameraController theCamera;
    private LevelManager theLevelManager;

    public bool waitingForRespawn;

    //플레이어 x좌표 필요
    //private PlayerController thePlayer;


    // Start is called before the first frame update
    void Start()
    {
        dropCount = timeBetweenDrops;
        timeBetweenDropStore = timeBetweenDrops;
        platformCount = waitForPlatforms;
        currnetHealth = startingHealth;

        theCamera = FindObjectOfType<CameraController>();
        theLevelManager = FindObjectOfType<LevelManager>();
        //thePlayer = FindObjectOfType<PlayerController>();

        theBoss.transform.position = rightPoint.position; //보스 첫 등장은 rightpoint 위치에서
        bossRight = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (theLevelManager.respawnCoActive) // player가 respawn 한다면
        {
            bossActive = false;
            waitingForRespawn = true;
        }

        if(waitingForRespawn && !theLevelManager.respawnCoActive)
        {
            //player 리스폰 끝나고 기다리면서 Boss에 관한 것들 다시 초기화

            theBoss.SetActive(false);
            leftPlatform.SetActive(false);
            rightPlatform.SetActive(false);

            timeBetweenDrops = timeBetweenDropStore;  // if(takeDamage)에서  timeBetweenDrops = timeBetweenDrops/2f; 이므로....
            platformCount = waitForPlatforms;
            dropCount = timeBetweenDrops;

            theBoss.transform.position = rightPoint.position;
            bossRight = true;

            currnetHealth = startingHealth;

            theCamera.followtarget = true;

            waitingForRespawn = false;

        }

        if (bossActive)
        {
            theCamera.followtarget = false; //player 따라다니지 못하게 함.
            theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, new Vector3(transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z), theCamera.smoothing*Time.deltaTime);
            
            theBoss.SetActive(true);

            if (dropCount > 0)
            {
                dropCount -= Time.deltaTime;
            } else
            {
                //x값만 leftpoint~rightpoint 사이의 랜덤값으로 설정. y, z는 그대로 유지.
                dropSawSpawnPoint.position = new Vector3(Random.Range(leftPoint.position.x, rightPoint.position.x),
                                                            dropSawSpawnPoint.position.y, dropSawSpawnPoint.position.z);

                //x값만 플레이어의 x값. y, z는 그대로 유지.
                //dropSawSpawnPoint.position = new Vector3(thePlayer.myrigidbody.position.x,
                //                                            dropSawSpawnPoint.position.y, dropSawSpawnPoint.position.z);

                Instantiate(dropSaw, dropSawSpawnPoint.position, dropSawSpawnPoint.rotation);

                dropCount = timeBetweenDrops;
            }

            if (bossRight) //보스가 오른쪽에 있으면
            {
                if(platformCount > 0)
                {
                    platformCount -= Time.deltaTime;
                }
                else
                {
                    rightPlatform.SetActive(true);
                }
            } else //보스가 왼쪽에 있으면
            {
                if (platformCount > 0)
                {
                    platformCount -= Time.deltaTime;
                }
                else
                {
                    leftPlatform.SetActive(true);
                }
            }

            if (takeDamage) //stompEnemy에서 true로 설정함
            {
                currnetHealth -= 1;

                if(currnetHealth <= 0) //보스 피가 0 이하면
                {
                    levelExit.SetActive(true);

                    theCamera.followtarget = true;

                    gameObject.SetActive(false); // 보스 비활성화
                }

                if (bossRight) //보스 이동 
                {
                    theBoss.transform.position = leftPoint.position;
                }
                else
                {
                    theBoss.transform.position = rightPoint.position;
                }

                bossRight = !bossRight; //  true이면 false가 되고, false이면 true가 된다.

                rightPlatform.SetActive(false);
                leftPlatform.SetActive(false);
                platformCount = waitForPlatforms; // 발판 없애고 다시 발판 스폰 카운트 시작

                timeBetweenDrops = timeBetweenDrops/2f;

                takeDamage = false;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            bossActive = true;
        }
    }
}
