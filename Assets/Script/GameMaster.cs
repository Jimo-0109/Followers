using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [Header("UI")]
    public FadeUI fadeUI;
    public GameObject pauseUI;
    public Text followerAmount;
    public GameObject gameOverUI;
    public Text gameOverText; //game over score
    public Text gameHighestScore;

    [Header("Camera")]
    public CameraController cameraController;

    [Header("Game status")]
    public int followerNumber;
    public bool isPause = false;
    public static bool isEndGame = false;
    public String highestScore = "Score";
    bool isUp;

    private void Start()
    {
        followerAmount.text = "0";
        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);
    }


    void Update()
    {
        if (PlayerController.HP <= 0) GameOver();
        if (Input.GetKeyDown(KeyCode.P)) Pause();
    }


    public void Pause()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);

        followerNumber = FollowerSystem.instance.followerAmount;
        followerAmount.text = followerNumber.ToString();
        isPause = true;

    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        isPause = false;
    }

    public void Restart()
    {        
        Time.timeScale = 1;
        isEndGame = false;
        isPause = false;
        fadeUI.FadeTO();
        
    }

    public void Menu()
    {
        fadeUI.FadeTO(0);
    }

    private void GameOver()
    {
        isEndGame = true;
        cameraController.focusPlayer();
        gameOverUI.SetActive(true);
        showHighestScoreUI(SaveHighestScore());
    }

    public float SaveHighestScore()
    {
        float recentHighest = PlayerPrefs.GetFloat(highestScore);
        if (recentHighest < Score.score) 
        {
            recentHighest = Score.score;
            PlayerPrefs.SetFloat(highestScore, recentHighest);
        }

        return recentHighest;
    }

    public void showHighestScoreUI(float score)
    {
        gameOverText.text = Score.score.ToString();
        gameHighestScore.text = "YOUR BEST : " + score;
    }

    public void UpCamera()
    {
        if (isUp)
        {
            CameraController.PosY = 30f;
            isUp = false;
        }
        else {
            CameraController.PosY = 108f;
            isUp = true;
        }
    }
}
