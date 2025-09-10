using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject trapPrefab;
    public Vector3 startSpawn = new Vector3(10, -2.25f, 0);
    public int minBetweenTraps = 5;
    public int maxBetweenTraps = 10;
    public int trapNum = 0;
    public int trapTrophy = 0;
    const int MAX_TRAPS = 25;

    public GameObject coinPrefab;

    public int levelNum = 0;
    public Vector3 coinSpawn = new Vector3(0, 0, 0);
    public int coinNum = 0;
    public int coinSpawnDis = 30;
    public int coinTrophy = 0;

    public List<GameObject> trapList = new List<GameObject>();
    public List<GameObject> coinList = new List<GameObject>();

    GameManager gm;
    PlayerStats stats;

    void Start()
    {
        stats = PlayerStats.Instance;

        //assign gm and match levelNum
        gm = GetComponent<GameManager>();
        levelNum = stats.level;

        //Apply Theme in GameManager

        //Get trapNum and spawn
        trapNum = (levelNum * 2) + Random.Range(3, 5);
        if(trapNum > MAX_TRAPS) { trapNum = MAX_TRAPS; }
        trapNum -= (int)(trapTrophy * 1.5f);
        SpawnTraps(trapNum);

        //get coinNum and spawn
        coinNum = (levelNum) + Random.Range(0, 3);
        coinNum += (int)(coinTrophy * 1.5f);
        SpawnCoins(coinNum);

        //clean the hierarchy
        gm.CleanHierarchy();
    }
    void SpawnTraps(int amount)
    {
        float lastTrapX = 0;
        for (int i = 0; i < amount; i++)
        {
            //create temp and place it at spawn
            GameObject tempTrap = Instantiate(trapPrefab);
            tempTrap.transform.position = startSpawn;

            if (lastTrapX != 0)
            {
                //if not first trap move away from previous trap 
                int rand = Random.Range(minBetweenTraps, maxBetweenTraps);
                Vector3 newPos = new(lastTrapX + rand, -2.25f, 0);
                tempTrap.transform.position = newPos;
            }
            //if first trap, assign as last trap and move on
            lastTrapX = tempTrap.transform.position.x;
            trapList.Add(tempTrap);
        }
    }
    void SpawnCoins(int amountToSpawn)
    {

        int coinsSpawned = 0;
        int ranDistance = 0;
        int spawnedAtSpike = 0;

        do
        {
            //select first spike
            //choose distance to coin (0-3?)
            ranDistance = Random.Range(spawnedAtSpike, spawnedAtSpike + (coinSpawnDis/10));
            if (ranDistance >= trapList.Count)
            {
                ranDistance = trapList.Count - 1;
            }

            Vector3 spawnPos = trapList[ranDistance].transform.position;
            spawnPos.y += 2f;

            //place coin
            GameObject tempCoin = Instantiate(coinPrefab, spawnPos, Quaternion.Euler(0, 0, 0));

            coinList.Add(tempCoin);

            //assign that spike as first spike
            spawnedAtSpike = ranDistance + 1;
            coinsSpawned++;

            //repeat
            //until no more coins or no more spikes
        }
        while (coinsSpawned < amountToSpawn && spawnedAtSpike < (trapList.Count - 1));

        Debug.Log($"**{coinsSpawned} out of {amountToSpawn} coins spawned");
    }
}
