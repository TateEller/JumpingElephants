using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GambManager : MonoBehaviour
{
    public PlayerStats Stats;
    public TextMeshProUGUI coinText;
    public GameObject background;
    public TextMeshProUGUI slotText;

    private void Start()
    {
        Stats = PlayerStats.Instance;

        coinText.text = ($" Coins: {Stats.coins}");
        slotText.text = "";
        SetRanBackground();
    }

    void SetRanBackground()
    {
        background.GetComponent<Renderer>().material.color = new Color(
               Random.Range(0.3f, 0.8f),
               Random.Range(0.3f, 0.8f),
               Random.Range(0.3f, 0.8f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SetRanBackground();
        }
    }

    public void UpdateCoins(int amount)
    {
        Stats.coins += amount;
        coinText.text = ($" Coins: {Stats.coins}");
    }

    public void KeepPlayingButton()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void TrophyButton()
    {
        SceneManager.LoadScene("TrophyRoom");
    }

    public void SlotMessage(string s, Color c)
    {
        slotText.text = s;
        slotText.color = c;
    }
}
