using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrophyApplicator : MonoBehaviour
{
    public static TrophyApplicator Instance;

    internal bool canApplyHealthTrophy = true;

    PlayerStats stats;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        stats = PlayerStats.Instance;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //this runs when a new scene is loaded
        Debug.Log("LOADED NEW SCENE");

        foreach (TrophySO t in stats.trophies)
        {
            ApplyTrophy(t.trophyName);
        }
    }

    internal void ApplyTrophy(string name)
    {
        //Scene 0 - Start Menu
        //Scene 1 - Main Gameplay
        //Scene 2 - Gamble/Store
        //Scene 3 - Trophy Room
        //Scene 4 - Test Scene

        Debug.Log($"Apply trophy {name}");

        switch (name)
        {
            case ("DiscountTrophy"):
                if(SceneManager.GetActiveScene().buildIndex == 2)
                {
                    //adds discount to trophy prices
                    FindObjectOfType<TrophyStore>().disountStack++;
                    Debug.Log("Apply DiscountTrophy");
                }
                break;
            case ("AppleTrophy"):
                if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    //adds another apple to slot machine
                    FindObjectOfType<SlotMachine>().spriteList.Add(FindObjectOfType<SlotMachine>().spriteList[0]);
                }
                break;
            case ("BlueberryTrophy"):
                if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    //adds another blueberry to slot machine
                    FindObjectOfType<SlotMachine>().spriteList.Add(FindObjectOfType<SlotMachine>().spriteList[1]);
                }
                break;
            case ("CoinTrophy"):
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    //spawns more coins per level
                    FindAnyObjectByType<LevelSpawner>().coinTrophy++;
                    //decrease spikes between coins
                    FindAnyObjectByType<LevelSpawner>().coinSpawnDis--;
                }
                break;
            case ("TrapTrophy"):
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    //spawns less traps per level
                    FindAnyObjectByType<LevelSpawner>().trapTrophy++;
                }
                break;
            case ("LemonTrophy"):
                {
                    //this trophy does nothing lol
                    break;
                }
            case ("HeartTrophy"):
                {
                    if (SceneManager.GetActiveScene().buildIndex == 1 && canApplyHealthTrophy)
                    {
                        canApplyHealthTrophy = false;
                        //give the player an extra life at the start of each level
                        stats.lives++;
                    }
                    break;
                }
        }

    }
}
