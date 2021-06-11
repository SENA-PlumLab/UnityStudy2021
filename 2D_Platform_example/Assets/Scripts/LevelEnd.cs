using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string levelToLoad;
    public string levelToUnlock;

    private PlayerController thePlayer;
    private CameraController theCamera;
    private LevelManager theLevelManager;

    public float waitToMove;
    public float waitToLoad;

    private bool movePlayer;

    public Sprite flagOpen;
    private SpriteRenderer theSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraController>();
        theLevelManager = FindObjectOfType<LevelManager>();

        theSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movePlayer)
        {
            thePlayer.myrigidbody.velocity = new Vector3(thePlayer.moveSpeed, thePlayer.myrigidbody.velocity.y, 0);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Application.LoadLevel(levelToLoad);
            //SceneManager.LoadScene(levelToLoad);
            theSpriteRenderer.sprite = flagOpen;
            StartCoroutine("LevelEndCo");
        }
    }

    public IEnumerator LevelEndCo()
    {
        thePlayer.canMove = false; //ĳ���� ���� �Ұ�
        theCamera.followtarget = false; // ī�޶� �������� ����
        theLevelManager.invincible = true; //��������. ��������

        theLevelManager.levelMusic.Stop();
        theLevelManager.gameOverMusic.Play();

        thePlayer.myrigidbody.velocity = Vector3.zero; // x, y, z �������� �����̴� ���� ��� 0���� ��.

        //�÷��� ���� �����ϱ�
        PlayerPrefs.SetInt("CoinCount", theLevelManager.coinCount);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.currentLives);
        PlayerPrefs.SetInt(levelToUnlock, 1); // ���� ������ unlock �ǰ� ��.

        yield return new WaitForSeconds(waitToMove);

        movePlayer = true;

        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(levelToLoad);
    }
}
