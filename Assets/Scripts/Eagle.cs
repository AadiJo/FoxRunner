using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public static Vector2 initialCoords;
    public Rigidbody2D rb;
    public GameObject player;
    public static bool isUnder = false;
    //public Enemy enemy;

    private void Awake()
    {
        initialCoords.x = transform.position.x;
        initialCoords.y = transform.position.y;
    }

    private void Update()
    {
        if (transform.position.y > initialCoords.y)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 12)
        {
            if (!isUnder)
            {

                if (Mathf.Abs(transform.position.x - player.transform.position.x) >= 0.5)
                {
                    rb.velocity = new Vector2(0, -3);
                }

                else
                {
                    rb.velocity = new Vector2(4, -3);
                }

                if (player.transform.position.y >= transform.position.y)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 4);
                }

                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                }
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - initialCoords.x) >= 10)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            

            if (transform.position.y <= initialCoords.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, 3);
            }



        }

        //if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 1 && player.transform.position.y > transform.position.y)
        //{
            //isUnder = true;
            //enemy.m_FacingRight = !enemy.m_FacingRight;
            //rb.velocity = new Vector2(-rb.velocity.x, 0);
        //}

        //else
        //{
            //isUnder = false;
        //}
    }
}

