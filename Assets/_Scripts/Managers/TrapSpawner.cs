using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    public GameObject trapPrefab;
    public Vector3 startSpawn = new Vector3(10, -2.25f, 0);
    public int minBetween = 3;
    public int maxBetween = 6;
    public int levelNum = 0;
    public int trapNum = 0;

    public List<GameObject> traps;

    void Start()
    {
        levelNum++;
        trapNum = 5 + (levelNum * 2);
        SpawnTraps(trapNum);
    }

    void SpawnTraps(int amount)
    {
        float lastTrapX = 0;
        for (int i = 0; i < amount; i++)
        {
            GameObject tempTrap = Instantiate(trapPrefab);
            tempTrap.transform.position = startSpawn;

            if (lastTrapX != 0)
            {
                int rand = Random.Range(minBetween, maxBetween);
                Vector3 newPos = new(lastTrapX + rand, -2.25f, 0);
                tempTrap.transform.position = newPos;
            }

            lastTrapX = tempTrap.transform.position.x;
            traps.Add(tempTrap);
        }
        Debug.LogError("****THIS IS IN USE***");
    }
}
