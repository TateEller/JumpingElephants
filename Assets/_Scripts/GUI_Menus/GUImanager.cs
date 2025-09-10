using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUImanager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI livesText;
    int totalCoinAmount = 0;

    PlayerStats stats;

    private void Start()
    {
        stats = PlayerStats.Instance;

        //update level text
        levelText.text = (" Level: " + stats.level);
        //update coin amount and text
        totalCoinAmount = stats.coins;
        coinText.text = (" Coins: " + totalCoinAmount);
        //update lives text
        livesText.text = (" Lives: " + stats.lives);
    }
    public void CollectCoin()
    {
        //increase coin total
        totalCoinAmount++;
        //update text
        coinText.text = (" Coins: " + totalCoinAmount);
        //update SO
        stats.coins = totalCoinAmount;
    }
}
