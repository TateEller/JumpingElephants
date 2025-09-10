using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TrophyRoomSpawner : MonoBehaviour
{
    public GameObject pedestalPrefab;

    public PlayerStats stats;

    private void Start()
    {
        SpawnPedestals(stats.trophies.Count);
    }

    void SpawnPedestals(int amountToSpawn)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject tempPede = Instantiate(pedestalPrefab, this.transform);
            tempPede.transform.GetChild(0).GetComponent<Image>().sprite = stats.trophies[i].sprite;
            tempPede.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = stats.trophies[i].trophyDesc;
        }
    }
}
