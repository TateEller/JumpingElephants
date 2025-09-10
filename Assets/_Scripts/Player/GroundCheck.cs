using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if on ground, you are able to jump
        if (collision.CompareTag("Ground"))
        {
            transform.parent.GetComponent<PlayerJump>().onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if you leave the ground, you can no longer jump
        if (collision.CompareTag("Ground"))
        {
            transform.parent.GetComponent<PlayerJump>().onGround = false;
        }
    }
}
