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

    //�÷��̾� x��ǥ �ʿ�
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

        theBoss.transform.position = rightPoint.position; //���� ù ������ rightpoint ��ġ����
        bossRight = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (theLevelManager.respawnCoActive) // player�� respawn �Ѵٸ�
        {
            bossActive = false;
            waitingForRespawn = true;
        }

        if(waitingForRespawn && !theLevelManager.respawnCoActive)
        {
            //player ������ ������ ��ٸ��鼭 Boss�� ���� �͵� �ٽ� �ʱ�ȭ

            theBoss.SetActive(false);
            leftPlatform.SetActive(false);
            rightPlatform.SetActive(false);

            timeBetweenDrops = timeBetweenDropStore;  // if(takeDamage)����  timeBetweenDrops = timeBetweenDrops/2f; �̹Ƿ�....
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
            theCamera.followtarget = false; //player ����ٴ��� ���ϰ� ��.
            theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, new Vector3(transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z), theCamera.smoothing*Time.deltaTime);
            
            theBoss.SetActive(true);

            if (dropCount > 0)
            {
                dropCount -= Time.deltaTime;
            } else
            {
                //x���� leftpoint~rightpoint ������ ���������� ����. y, z�� �״�� ����.
                dropSawSpawnPoint.position = new Vector3(Random.Range(leftPoint.position.x, rightPoint.position.x),
                                                            dropSawSpawnPoint.position.y, dropSawSpawnPoint.position.z);

                //x���� �÷��̾��� x��. y, z�� �״�� ����.
                //dropSawSpawnPoint.position = new Vector3(thePlayer.myrigidbody.position.x,
                //                                            dropSawSpawnPoint.position.y, dropSawSpawnPoint.position.z);

                Instantiate(dropSaw, dropSawSpawnPoint.position, dropSawSpawnPoint.rotation);

                dropCount = timeBetweenDrops;
            }

            if (bossRight) //������ �����ʿ� ������
            {
                if(platformCount > 0)
                {
                    platformCount -= Time.deltaTime;
                }
                else
                {
                    rightPlatform.SetActive(true);
                }
            } else //������ ���ʿ� ������
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

            if (takeDamage) //stompEnemy���� true�� ������
            {
                currnetHealth -= 1;

                if(currnetHealth <= 0) //���� �ǰ� 0 ���ϸ�
                {
                    levelExit.SetActive(true);

                    theCamera.followtarget = true;

                    gameObject.SetActive(false); // ���� ��Ȱ��ȭ
                }

                if (bossRight) //���� �̵� 
                {
                    theBoss.transform.position = leftPoint.position;
                }
                else
                {
                    theBoss.transform.position = rightPoint.position;
                }

                bossRight = !bossRight; //  true�̸� false�� �ǰ�, false�̸� true�� �ȴ�.

                rightPlatform.SetActive(false);
                leftPlatform.SetActive(false);
                platformCount = waitForPlatforms; // ���� ���ְ� �ٽ� ���� ���� ī��Ʈ ����

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
