using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public GameObject winScreen, loseScreen, pauseMenu;
    public TextMeshProUGUI loseText;
    GameManager gm;
    public PlayerStats stats;
    void Start()
    {
        stats = PlayerStats.Instance;

        //hide all info screens
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        pauseMenu.SetActive(false);

        gm = FindObjectOfType<GameManager>();
    }

    public void ReachedTheEnd() { winScreen.SetActive(true); }
    public void HitTrap() 
    {
        if(stats.lives > 1)
        {
            loseText.text = "You Lose";
        }
        else
        {
            //out of lives
            loseText.text = "Out of Lives";
        }
        loseScreen.SetActive(true); 
    }
    public void NextLevelButton()
    {
        //Loads next level
        gm.gamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log($"Load Level {stats.level}");
    }
    public void TryAgainButton()
    {
        //Resets level
        gm.gamePaused = false;

        if (stats.lives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            //out of lives
            //reset stats then load scene
            stats.lives = 3;
            stats.level = 1;
            stats.coins = 0;
            //reset trophies?

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void ReturnToTitleButton()
    {
        //Returns to Title scene
        gm.gamePaused = false;
        SceneManager.LoadScene("StartMenu");
    }
    public void PauseGame()
    {
        //show pause screen
        pauseMenu.SetActive(true);
        //stop movement
        gm.Movement(false);
        Time.timeScale = 0f;
        //update pause status
        gm.gamePaused = true;
    }
    public void ResumeGame()
    {
        //hide pause screen
        pauseMenu.SetActive(false);
        //resume movement
        gm.Movement(true);
        Time.timeScale = 1f;
        //update pause status
        gm.gamePaused = false;
    }

    public void GambleButton()
    {
        //Load gamble scene
        gm.gamePaused = false;
        SceneManager.LoadScene("GambleScene");
    }

    public void TrophyRoomButton()
    {
        //Load TrophyRoom
        gm.gamePaused = false;
        SceneManager.LoadScene("TrophyRoom");
    }
}
