using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerStats stats;

    public GameObject trapManager;
    List<GameObject> trapList = new();

    public GameObject coinManger;
    internal List<GameObject> coinList = new();
    internal int coinThisRound = 0;

    public LevelThemes[] levelThemes;
    public GameObject background;
    public GameObject ground;
    public GameObject close;
    public GameObject far;

    GameMenu gameMenu;
    PlayerJump playerJump;

    bool hasWon = false;
    public bool gamePaused = false;

    void Start()
    {
        stats = PlayerStats.Instance;

        gameMenu = FindObjectOfType<GameMenu>();
        playerJump = FindObjectOfType<PlayerJump>();

        ApplyLevelTheme();

        coinThisRound = 0;

        //make sure game is not paused
        //resume movement
        Movement(true);
        Time.timeScale = 1f;
        //update pause status
        gamePaused = false;
    }
    public void CleanHierarchy()
    {
        //----UI cleaning---
        //Add all items tagged "Trap" as children of the trapManager
        trapList.AddRange(GameObject.FindGameObjectsWithTag("Trap"));
        foreach (GameObject trap in trapList)
        {
            trap.transform.SetParent(trapManager.transform);
        }
        //Add all items tagged "Coin" as children of the coinManager
        coinList.AddRange(GameObject.FindGameObjectsWithTag("Coin"));
        foreach (GameObject coin in coinList)
        {
            coin.transform.SetParent(coinManger.transform);
        }
    }
    public void ApplyLevelTheme()
    {
        int ranInt = Random.Range(0, levelThemes.Length);
        LevelThemes ranTheme = levelThemes[ranInt];

        if (levelThemes.Length > 1)
        {
            while (stats.lastLevelInt != 0 && ranInt == stats.lastLevelInt)
            {
                ranInt = Random.Range(0, levelThemes.Length);
                ranTheme = levelThemes[ranInt];
            }
        }
        else
            Debug.LogWarning("One or no themes listed");

        background.GetComponent<SpriteRenderer>().sprite = ranTheme.backgroundSprite;
        ground.GetComponent<SpriteRenderer>().sprite = ranTheme.groundSprite;
        close.GetComponent<SpriteRenderer>().sprite = ranTheme.wallObj1Sprite;
        far.GetComponent<SpriteRenderer>().sprite = ranTheme.wallObj2Sprite;

        stats.lastLevelInt = ranInt;
    }

    void Update()
    {
        //if all traps have been passed
        if(trapManager.transform.childCount == 0)
        {
            if (!hasWon) 
                WinGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if game is paused then resume game, else pause game
            if (gamePaused)
            {
                gameMenu.ResumeGame();
            }
            else
            {
                gameMenu.PauseGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            stats.coins += 10;
            WinGame();
        }
    }

    public void WinGame()
    {
        hasWon = true;

        try
        {
            FindAnyObjectByType<TrophyApplicator>().canApplyHealthTrophy = true;
        }
        catch 
        {
            //no TrophyApplicator
        }

        //clear coin/trap lists
        trapList.Clear();
        coinList.Clear();

        //stop player movement
        Movement(false);

        //show win screen
        gameMenu.ReachedTheEnd();
        stats.level++;
    }

    public void LoseGame()
    {
        if (playerJump.gameRunning)
        {
            //hit trap

            //stop movement
            Movement(false);

            //Show lose screen
            gameMenu.HitTrap();

            //Take away coins from this round and a life
            stats.coins -= coinThisRound;
            stats.lives--;

            if (stats.lives >= 0)
            {
                //out of lifes

            }
        }
    }

    public void Movement(bool status)
    {
        //Start or stop scrolling
        ScrollLeft[] scrollers = GameObject.FindObjectsOfType<ScrollLeft>();
        foreach (ScrollLeft scroller in scrollers)
        {
            scroller.gameRunning = status;
        }
        //Start or stop jump input
        playerJump.gameRunning = status;
    }
}
