using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollLeft : MonoBehaviour
{
    public bool infinteScroll = false;
    public float scrollSpeed = 4f;
    Vector3 scrollVector = new(-1, 0, 0);
    Vector3 startPos = new();
    Vector3 halfPos = new();
    float backgroundWidth;

    public bool gameRunning = true;

    void Start()
    {
        //Make sure the game is running and set scroll speed
        gameRunning = true;
        scrollVector = new Vector3(scrollSpeed, 0, 0);


        if (infinteScroll)
        {
            //Find infinite scroll info(sprite size and pos)
            //Checked on things like background and floor
            startPos = transform.position;
            backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
            halfPos = transform.position;
            halfPos.x -= backgroundWidth / 2;
        }
    }

    void Update()
    {
        if (gameRunning)
        {
            //Move object to the 
            transform.position -= scrollVector * Time.deltaTime;

            if (infinteScroll)
            {
                //Loop infinite scroll so the level looks inifinite
                if (transform.position.x <= halfPos.x)
                {
                    transform.position = startPos;
                }
            }
            else
            {
                if (transform.position.x <= -15)
                {
                    //destory items that travel to far off screen
                    Destroy(gameObject);
                }
            }
        }
    }
}
