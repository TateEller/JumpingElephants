using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapReset : MonoBehaviour
{
    GameManager gm;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collision is from player, lose the game
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit trap");
            gm.LoseGame();
        }
    }
}
