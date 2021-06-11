using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float waitToRespawn;
    public PlayerController thePlayer;

    public GameObject deathSplosion;

    public int coinCount;
    private int coinBonusLifeCount;

    public AudioSource coinSound;

    public Text coinText;

    public Image heart1, heart2, heart3;
    public Sprite heartFull, heartHalf, heartEmpty;
    public int maxHealth, healthCount;

    private bool respawning;

    public GameObject gameOverScreen;

    //������ ������Ʈ�� ��� �迭
    private ResetOnRespawn[] objectToReset;

    public bool invincible; //��������Ȯ��

    public Text livesText;
    public int startingLives; //���� ���
    public int currentLives; //���� ���

    public AudioSource levelMusic;
    public AudioSource gameOverMusic;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    
        healthCount = maxHealth;

        objectToReset = FindObjectsOfType<ResetOnRespawn>();

        if (PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }

        if (PlayerPrefs.HasKey("PlayerLives"))
        {
            currentLives = PlayerPrefs.GetInt("PlayerLives");
        }
        else
        {
            currentLives = startingLives;
        }

        coinText.text = "Coins: " + coinCount;

        livesText.text = "Lives x " + currentLives;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthCount <= 0)
        {
            Respawn();
        }

        if(coinBonusLifeCount >= 100)
        {
            currentLives += 1;
            livesText.text = "Lives x " + currentLives;
            coinBonusLifeCount -= 100;
        }
        
    }

    public void Respawn()
    {
        if (!respawning)
        {
            currentLives -= 1;
            livesText.text = "Lives x " + currentLives;

            if (currentLives > 0)
            {
              
         
                respawning = true;
                //����� �����ִٸ� ������
                StartCoroutine("RespawnCo");
            }
            else
            {
                thePlayer.gameObject.SetActive(false);
                gameOverScreen.SetActive(true); //���ӿ���

                levelMusic.volume = levelMusic.volume / 2f;
                //levelMusic.Stop();
                //gameOverMusic.Play();

            }
        }
        
    }

    public IEnumerator RespawnCo()
    {
        thePlayer.gameObject.SetActive(false);

        // ������Ʈ, ������Ʈ�� �� ��(position), ������Ʈ�� rotation
        Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

        //yield ���� ��� ���߱�... 
        yield return new WaitForSeconds(waitToRespawn);

        //ü�� ȸ��
        healthCount = maxHealth;
        respawning = false;
        UpdateHeartMeter();

        //���� �ʱ�ȭ, text ������Ʈ
        coinCount = 0;
        coinText.text = "Coins: " + coinCount;
        coinBonusLifeCount = 0;

        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);

        for(int i=0; i<objectToReset.Length; i++)
        {
            objectToReset[i].gameObject.SetActive(true);
            objectToReset[i].ResetObject();
        }



    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinBonusLifeCount += coinsToAdd;

        //Coin ������ ������Ʈ
        //�� �����Ӹ��� üũ�� �ʿ� �����Ƿ�. update()�� ���� �ʿ䵵 ����
        coinText.text = "Coins: " + coinCount;

        coinSound.Play();
    }

    public void HurtPlayer(int damageToTake)
    {
        if (!invincible)
        {
            healthCount -= damageToTake;
            UpdateHeartMeter();

            thePlayer.Knockback();

            thePlayer.hurtSound.Play();
        }
    }


    public void GiveHealth(int healthToGive)
    {
        healthCount += healthToGive;

        if(healthCount > maxHealth)
        {
            healthCount = maxHealth;
        }
        coinSound.Play();
        UpdateHeartMeter();
    }



    //��Ʈ �׸� ������Ʈ
    //�� �����Ӹ��� üũ�� �ʿ� �����Ƿ�. update()�� ���� �ʿ䵵 ����
    public void UpdateHeartMeter()
    {
        switch (healthCount)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return; //�Լ� ��ü�� �����ϰ� ��.
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                return;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                return;
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            default:  //�������� ��
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;


        }
    }

    public void AddLives(int livesToAdd)
    {
        coinSound.Play();
        currentLives += livesToAdd;
        livesText.text = "Lives x " + currentLives;
    }

}
