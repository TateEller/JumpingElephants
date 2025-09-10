using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    LevelSpawner lvlSpawn;
    GameManager gm;

    private void Start()
    {
        lvlSpawn = FindObjectOfType<LevelSpawner>();
        gm = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collision is player
        if (collision.CompareTag("Player"))
        {
            //call CollectCoin
            GUImanager gui = FindObjectOfType<GUImanager>();
            gui.CollectCoin();
            gm.coinThisRound++;

            //remove coin from lists
            lvlSpawn.coinList.Remove(gameObject);
            gm.coinList.Remove(gameObject);

            //destroy coin
            Destroy(gameObject);
        }
    }
}
