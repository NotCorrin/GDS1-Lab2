using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 velocity;
    [SerializeField] float bounceVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = velocity;
        
    }

    // Update is called once per frame
    void Update()
    {
        /* if (rb.velocity.y == 0)
             rb.velocity = rb.velocity + new Vector2(0, bounceVelocity);
             */
        rb.velocity = new Vector2(velocity.x, rb.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
        else if (col.GetContact(0).point.y > transform.position.y - 0.49)
        {
            velocity = new Vector2(-velocity.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = rb.velocity + new Vector2(0, bounceVelocity);
        }
            
    }

}
