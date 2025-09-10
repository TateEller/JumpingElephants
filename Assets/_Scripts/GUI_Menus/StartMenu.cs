using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public GameObject startMeun, optionsMenu;
    public Button continueButton;
    public GameObject statBoxes;
    public TextMeshProUGUI coinText, levelText;
    public PlayerStats PlayerStatsSO;

    private void Start()
    {
        //Default to start open and hide options
        startMeun.SetActive(true);
        optionsMenu.SetActive(false);

        if (PlayerStatsSO.level <= 1 && PlayerStatsSO.lives == 3)
        {
            //no run to continue
            continueButton.interactable = false;
            statBoxes.SetActive(false);
        }
        else
        {
            continueButton.interactable = true;
            statBoxes.SetActive(true);
            coinText.text = $" Coins: {PlayerStatsSO.coins}";
            levelText.text = $" Level: {PlayerStatsSO.level}";
        }
    }

    public void ContinueButton()
    {
        Debug.Log("--==Resume Game==--");

        //Load next scene (most likely level 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void StartButton()
    {
        Debug.Log("--==New Game==--");
        PlayerStatsSO.lives = 3;
        PlayerStatsSO.level = 1;
        PlayerStatsSO.coins = 0;
        PlayerStatsSO.trophies.Clear();
        //Load next scene (most likely level 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsButton()
    {
        //hide start menu(play, options, quit) and show options
        startMeun.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void BackButton()
    {
        //hide options menu (return to default start menu)
        startMeun.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        //quits the game
        Debug.Log("--==Quit Game==--");
        Application.Quit();
    }
}
