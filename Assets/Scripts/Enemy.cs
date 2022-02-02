using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public GameObject Player;
    public Rigidbody2D rb;
    public bool m_FacingRight = false;


    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - Player.transform.position.x) < 6)
        {
            animator.SetBool("Moving", true);
            if (Player.transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-7, rb.velocity.y);
                if (m_FacingRight)
                {
                    Flip();
                }
            }

            if (Player.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(7, rb.velocity.y);
                if (!m_FacingRight)
                {
                    Flip();
                }
            }

            if (Mathf.Abs(transform.position.x - Player.transform.position.x) <= 1)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                
            }
        }

        else
        {
            
            //animator.SetBool("Moving", false);
        }

        if (transform.position.y < -28f)
        {
            Destroy(gameObject);
        }
    }


    private void Death()
    {
        Destroy(this);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
