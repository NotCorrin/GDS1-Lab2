using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < velocity.y)
            rb.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
        else if (col.contacts[0].normal.x != 0)
        {
            rb.velocity = new Vector2(-velocity.x, -velocity.y);
        }
        else
            rb.velocity = new Vector2(velocity.x, -velocity.y);
    }

}
