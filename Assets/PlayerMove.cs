using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    public float jumpspeed = 1, acc = 1, maxfallspeed = 1, speed = 5, runspeed = 8.5f, curmaxspeed;
    public float move;
    public bool grounded;
    public Rigidbody2D rb;
    public bool capoff;
    public SpriteRenderer rend;
    // Start is called before the first frame update
    void Update()
    {
        curmaxspeed = (Input.GetKey(KeyCode.LeftShift)?runspeed:speed);
        move = Input.GetAxisRaw("Horizontal");
        rb.AddForce(move * acc * Vector3.right * Time.deltaTime, ForceMode2D.Impulse);
        //if(Mathf.Sign(curspeed.x) > curmaxspeed || (move == 0 && Mathf.Sign(curspeed.x) > Time.deltaTime * acc)) curspeed.x -= Time.deltaTime * Mathf.Sign(curspeed.x) * acc;
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -curmaxspeed, curmaxspeed), Mathf.Clamp(rb.velocity.y, -maxfallspeed, jumpspeed));
        if(grounded == true)
        {
            if(Mathf.Abs(rb.velocity.x) > curmaxspeed || (move == 0 && Mathf.Abs(rb.velocity.x) > Time.deltaTime * acc)) 
            {
                //rb.velocity -= Mathf.Sign(rb.velocity.x) * acc * 0.5f * Vector2.right;
                rb.AddForce(Mathf.Sign(rb.velocity.x) * acc * 0.5f * Vector2.left);
                if(Mathf.Abs(rb.velocity.x) < 0.1f) rb.velocity = Vector2.zero;
            }
        }
        if(Input.GetKeyDown(KeyCode.W) && grounded == true)
        {
            rb.AddForce((jumpspeed + Mathf.Abs(rb.velocity.x) * 3) * Vector2.up, ForceMode2D.Impulse);
            grounded = false;
        }
        if((Input.GetKeyUp(KeyCode.W) || rb.velocity.y < 0) && grounded == false) capoff = true;
        if(capoff) {
            rb.AddForce(Vector2.down * 4);
            if(rb.velocity.y > 0)rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.9f);
        }
    }
    void LateUpdate()
    {
        rend.flipX = rb.velocity.x < 0;
    }
    void OnCollisionEnter2D()
    {
        grounded = true;
        capoff = false;
    }
}
