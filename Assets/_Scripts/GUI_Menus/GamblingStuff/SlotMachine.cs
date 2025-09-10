using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    List<Image> slots = new List<Image>();

    public List<Sprite> spriteList = new List<Sprite>();
    bool spinning = false;

    GambManager gamba;
    private void Start()
    {
        gamba = FindObjectOfType<GambManager>();

        //assigns slots
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>());
        }

        //random slot images
        foreach (Image slot in slots)
        {
            slot.sprite = spriteList[Random.Range(0, spriteList.Count)];
        }
    }

    public void PlaySlot()
    {
        if (gamba.Stats.coins >= 5 && !spinning)
        {
            StartCoroutine(SpinSlots());
            gamba.UpdateCoins(-5);  //cost to play
            gamba.SlotMessage("-5 Coins", Color.red);
        }
        else if (spinning)
        {
            gamba.SlotMessage("Already Spinning", Color.red);
        }
        else
        {
            //not enough coins to play
            Debug.Log("--==Not Enough Coins==--");
            gamba.SlotMessage("Not Enough Coins", Color.red);
        }

    }

    IEnumerator SpinSlots()
    {
        Debug.Log("--==Start Spin==--");
        spinning = true;

        float spinTime = 2f;
        float timeSpun = 0f;

        while (timeSpun < spinTime)
        {
            foreach (Image slot in slots)
            {
                slot.sprite = spriteList[Random.Range(0, spriteList.Count)];
            }

            timeSpun += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        foreach (Image slot in slots)
        {
            slot.sprite = spriteList[Random.Range(0, spriteList.Count)];
        }

        Debug.Log("--==Finish Spin==--");
        CheckSprites();
    }

    void CheckSprites()
    {
        List<Image> slotsSprites = new List<Image>();

        foreach (Image slot in slots)
        {
            slotsSprites.Add(slot);
        }

        if (slotsSprites[0].sprite == slotsSprites[1].sprite && slotsSprites[0].sprite == slotsSprites[2].sprite)
        {
            //1.56% chance (gpt)
            Debug.Log("--==Three Matches==--");
            gamba.UpdateCoins(25);   //coins won
            gamba.SlotMessage("+25 Coins", Color.green);
        }
        else if (slotsSprites[0].sprite == slotsSprites[1].sprite ||
            slotsSprites[0].sprite == slotsSprites[2].sprite ||
            slotsSprites[1].sprite == slotsSprites[2].sprite)
        {
            //21.97% chance (gpt)
            Debug.Log("--==Two Matches==--");
            gamba.UpdateCoins(10);   //coins won
            gamba.SlotMessage("+10 Coins", Color.green);
        }
        else
        {
            Debug.Log("--==No Matches==--");
            gamba.SlotMessage("No Prize", Color.red);
        }

        spinning = false;
    }
}
