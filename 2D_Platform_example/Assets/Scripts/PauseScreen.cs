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
        if (Input.GetButtonDown("Pause")) //esc키를 누르면
        {
            if (Time.timeScale == 0f) //시간이 멈춰있으면 (= Pause 상태이면)
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
        Time.timeScale = 0; // 시간 멈춤

        thePauseScreen.SetActive(true); //pause screen 표시
        thePlayer.canMove = false; //조작불가
        theLevelManager.levelMusic.Pause(); //음악 멈춤
    }

    public void ResumeGame()
    {
        thePauseScreen.SetActive(false);

        Time.timeScale = 1f; //시간 정상 회복
        thePlayer.canMove = true; //조작가능
        theLevelManager.levelMusic.Play(); //음악재생
    }

    public void LevelSelect()
    {
        PlayerPrefs.SetInt("CoinCount", 0);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.startingLives);

        Time.timeScale = 1f; //시간 정상 회복
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitToMeinMenu()
    {
        Time.timeScale = 1f; //시간 정상 회복
        SceneManager.LoadScene(mainMenu);
    }
}
