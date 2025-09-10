using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSlotMachine : MonoBehaviour
{
    List<GameObject> slots = new List<GameObject>();

    public List<Color> colorList = new List<Color>();
    bool spinning = false;

    GambManager gamba;
    private void Start()
    {
        gamba = FindObjectOfType<GambManager>();

        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject slot in slots)
        {
            slot.GetComponent<Renderer>().material.color = colorList[Random.Range(0, colorList.Count)];
        }
    }
    private void Update()
    {

    }

    public void PlaySlot()
    {
        if(gamba.Stats.coins >= 5 && !spinning)
        {
            StartCoroutine(SpinSlots());
            gamba.UpdateCoins(-5);  //cost to play
            gamba.SlotMessage("-5 Coins", Color.red);
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
            foreach (GameObject slot in slots)
            {
                slot.GetComponent<Renderer>().material.color = colorList[Random.Range(0, colorList.Count)];
            }

            timeSpun += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        foreach (GameObject slot in slots)
        {
            slot.GetComponent<Renderer>().material.color = colorList[Random.Range(0, colorList.Count)];
        }

        Debug.Log("--==Finish Spin==--");
        CheckColors();
    }

    void CheckColors()
    {
        List<Material> slotsColor = new List<Material>();

        foreach (GameObject slot in slots)
        {
            slotsColor.Add(slot.GetComponent<Renderer>().material);
        }

        if (slotsColor[0].color == slotsColor[1].color && slotsColor[0].color == slotsColor[2].color)
        {
            //1.56% chance (gpt)
            Debug.Log("--==Three Matches==--");
            gamba.UpdateCoins(25);   //coins won
            gamba.SlotMessage("+25 Coins", Color.green);
        }
        else if (slotsColor[0].color == slotsColor[1].color || 
            slotsColor[0].color == slotsColor[2].color || 
            slotsColor[1].color == slotsColor[2].color)
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
