using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIService : MonoBehaviour
{
    public static UIService instance;
    public GameObject GameOverPanel;
    public GameObject YouWinPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    private static int Score;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        Score = 0;
        scoreText.text = "Score: " + Score.ToString();
    }
    public void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextRound()
    {
        YouWinPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PlayerService.instace.GetPlayerController().PlayerDied(false);
        PlayerService.instace.CreatePlayer();
        TileMapController.instance.LoadLevel();
    }


    public void StartTheGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowGameOverScreen()
    {
        GameOverPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);
        finalScoreText.text = "Your Final Score is: " + Score.ToString();
    }
    public void ShowWinScreen()
    {
        if (YouWinPanel)
            YouWinPanel.SetActive(true);
    }
    public void UpdateScore()
    {
        Score += 10;
        scoreText.text = "Score: " + Score.ToString();
    }
}
