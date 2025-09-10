using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyStore : MonoBehaviour
{
    public GameObject shopPrefab;
    public GameObject[] pedestal;

    TrophySO[] allTrophies;
    List<TrophySO> availableTrophies;

    public int disountStack = 0;

    private void Awake()
    {
        allTrophies = Resources.LoadAll<TrophySO>("Trophies");
        availableTrophies = new List<TrophySO>(allTrophies);
    }

    private void Start()
    {
        RerollShop();
    }

    TrophySO GetRandTrophy()
    {
        int ran = Random.Range(0, availableTrophies.Count);

        return availableTrophies[ran];
    }

    public void RerollShop()
    {
        //get the item displays
        pedestal = new GameObject[] { transform.GetChild(0).gameObject, transform.GetChild(1).gameObject };

        //add random trophy to display slot
        foreach (GameObject slot in pedestal)
        {
            if (availableTrophies.Count > 0)
            {
                TrophySO temp = GetRandTrophy();
                TrophyDisplayManager tempTro = Instantiate(shopPrefab, slot.transform).GetComponent<TrophyDisplayManager>();
                tempTro.Setup(temp, this.disountStack);

                availableTrophies.Remove(temp);
            }
        }
    }
}
