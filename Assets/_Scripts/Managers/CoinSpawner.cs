using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    TrapSpawner trapS;
    public GameObject coinPrefab;
    int totalCoins;


    void Start()
    {
        trapS = FindAnyObjectByType<TrapSpawner>();

        totalCoins = trapS.levelNum * 2 + 2;
        Debug.Log($"Total/max coins {totalCoins}");
        SpawnCoins(totalCoins);
    }

    void SpawnCoins(int max)
    {
        GameObject[] trap = new GameObject[totalCoins];

        /*
        //adds random coins to random spots
        for (int i = 0; i < trap.Length; i++)
        {
            int ranTrap = Random.Range(0, trapS.traps.Count);

            GameObject tempCoin = Instantiate(coinPrefab);
            Vector3 trapVector = trapS.traps[ranTrap].transform.position;
            trapVector.x += 2.25f;
            tempCoin.transform.position = trapVector;
        }   */

        //or add random coins behind other coins
        int coinCount = 1;
        int coinsSpawned = 0;
        do
        {
            int ran = Random.Range(coinCount, coinCount + 2);
            Vector3 coinPos = trapS.traps[ran].transform.position;
            coinPos.x += 2.25f;
            GameObject tempCoin = Instantiate(coinPrefab);
            tempCoin.transform.position = coinPos;
            coinCount = ran;
            coinsSpawned++;
        }
        while (coinCount < max);

        Debug.LogError("****THIS SCRIPT IS IN USE****");

        Debug.Log($"Coins spawned {coinsSpawned}");
    }
}
