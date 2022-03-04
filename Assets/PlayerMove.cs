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
    // Start is called before the first frame update
    void Update()
    {
        curmaxspeed = speed;
        move = Input.GetAxisRaw("Horizontal");
        rb.AddForce(move * acc * Vector3.right * Time.deltaTime, ForceMode2D.Impulse);
        //if(Mathf.Sign(curspeed.x) > curmaxspeed || (move == 0 && Mathf.Sign(curspeed.x) > Time.deltaTime * acc)) curspeed.x -= Time.deltaTime * Mathf.Sign(curspeed.x) * acc;
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -curmaxspeed, curmaxspeed), Mathf.Clamp(rb.velocity.y, -maxfallspeed, jumpspeed));
        if(Input.GetKeyDown(KeyCode.W) && grounded == true)
        {
            rb.AddForce(jumpspeed * Vector2.up, ForceMode2D.Impulse);
            grounded = false;
        }
        if((Input.GetKeyUp(KeyCode.W) || rb.velocity.y < 0) && grounded == false) capoff = true;
        if(capoff) rb.AddForce(Vector2.down * 5);
    }
    void OnCollisionEnter2D()
    {
        grounded = true;
        capoff = false;
    }
}
