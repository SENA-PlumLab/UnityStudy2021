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

    //리셋할 오브젝트들 담는 배열
    private ResetOnRespawn[] objectToReset;

    public bool invincible; //무적상태확인

    public Text livesText;
    public int startingLives; //시작 목숨
    public int currentLives; //현재 목숨

    public AudioSource levelMusic;
    public AudioSource gameOverMusic;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();

        coinText.text = "Coins: " + coinCount;

        healthCount = maxHealth;

        objectToReset = FindObjectsOfType<ResetOnRespawn>();

        currentLives = startingLives;
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
                //목숨이 남아있다면 리스폰
                StartCoroutine("RespawnCo");
            }
            else
            {
                thePlayer.gameObject.SetActive(false);
                gameOverScreen.SetActive(true); //게임오버

                levelMusic.volume = levelMusic.volume / 2f;
                //levelMusic.Stop();
                //gameOverMusic.Play();

            }
        }
        
    }

    public IEnumerator RespawnCo()
    {
        thePlayer.gameObject.SetActive(false);

        // 오브젝트, 오브젝트를 둘 곳(position), 오브젝트의 rotation
        Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

        //yield 게임 잠시 멈추기... 
        yield return new WaitForSeconds(waitToRespawn);

        //체력 회복
        healthCount = maxHealth;
        respawning = false;
        UpdateHeartMeter();

        //코인 초기화, text 업데이트
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

        //Coin 보유량 업데이트
        //한 프레임마다 체크할 필요 없으므로. update()에 넣을 필요도 없음
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



    //하트 그림 업데이트
    //한 프레임마다 체크할 필요 없으므로. update()에 넣을 필요도 없음
    public void UpdateHeartMeter()
    {
        switch (healthCount)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return; //함수 자체를 종료하게 함.
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

            default:  //음수값일 때
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
