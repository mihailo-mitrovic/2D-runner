using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject MainMenuButton;

    private void Awake()
    {
        MainMenuButton.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        scoreText.text = "Time: " + Time.timeSinceLevelLoad.ToString("0") + "\nLives: " + player.GetComponent<PlayerController>().lives;
        if (player.GetComponent<PlayerController>().gameState == GameState.GameWon)
        {
            Time.timeScale = 0;
            gameOverText.text = "GAME OVER \n YOU WIN!";
            MainMenuButton.SetActive(true);
        }
        if (player.GetComponent<PlayerController>().gameState == GameState.GameLost)
        {
            Time.timeScale = 0;
            gameOverText.text = "GAME OVER \n YOU LOSE!";
            MainMenuButton.SetActive(true);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
