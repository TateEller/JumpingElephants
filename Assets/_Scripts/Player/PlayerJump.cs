using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce;
    //canJump gets update from GroundCheck
    internal bool onGround = true;

    public bool gameRunning = true;

    Rigidbody2D rb;
    Animator ani;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if (onGround)
        {
            //idle or run ani
            if (gameRunning)
            {
                ani.SetBool("isRunning", true);

                //if player presses jump buttons
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //apply jump force
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    ani.SetTrigger("doJump");
                }
            }
            else ani.SetBool("isRunning", false);
        }
    }
}
