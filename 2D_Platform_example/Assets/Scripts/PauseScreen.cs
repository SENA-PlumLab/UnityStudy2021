using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public string levelSelect;
    public string mainMenu;

    private LevelManager theLevelManager;

    public GameObject thePauseScreen;

    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause")) //escŰ�� ������
        {
            if (Time.timeScale == 0f) //�ð��� ���������� (= Pause �����̸�)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0; // �ð� ����

        thePauseScreen.SetActive(true); //pause screen ǥ��
        thePlayer.canMove = false; //���ۺҰ�
        theLevelManager.levelMusic.Pause(); //���� ����
    }

    public void ResumeGame()
    {
        thePauseScreen.SetActive(false);

        Time.timeScale = 1f; //�ð� ���� ȸ��
        thePlayer.canMove = true; //���۰���
        theLevelManager.levelMusic.Play(); //�������
    }

    public void LevelSelect()
    {
        PlayerPrefs.SetInt("CoinCount", 0);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.startingLives);

        Time.timeScale = 1f; //�ð� ���� ȸ��
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitToMeinMenu()
    {
        Time.timeScale = 1f; //�ð� ���� ȸ��
        SceneManager.LoadScene(mainMenu);
    }
}
