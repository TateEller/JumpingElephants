using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrophyDisplayManager : MonoBehaviour
{
    public TrophySO trophy;
    PlayerStats stats;

    GambManager gamba;

    public TextMeshProUGUI costText;
    public Image trophyImage;
    public TextMeshProUGUI infoText;

    public int discountStack;
    int price;

    private void Awake()
    {
        gamba = FindObjectOfType<GambManager>();
        stats = PlayerStats.Instance;
    }
    public void Setup(TrophySO trophy, int discountStack)
    {
        this.trophy = trophy;
        this.discountStack = discountStack;

        // Calculate price here instead of Start()
        int discount = 5 * discountStack;
        price = (int)(trophy.trophieCost * ((100f - discount) / 100f));
        if (price < 5) price = 5;

        costText.text = ($"{price} Coins");
        trophyImage.sprite = trophy.sprite;
        infoText.text = trophy.trophyDesc;
    }

    public void BuyTrophy()
    {
        if(stats.coins >= price)
        {
            //can buy
            gamba.UpdateCoins(-price);
            stats.trophies.Add(trophy);

            //apply trophy
            TrophyApplicator.Instance.ApplyTrophy(trophy.name);

            //remove trophy from shop
            Destroy(gameObject);
        }
    }
}
