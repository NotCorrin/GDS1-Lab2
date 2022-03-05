using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float gravity = 1, jumpspeed = 1, maxfallspeed = 1.5f, acc = 1, speed = 5, runspeed = 8.5f, curmaxspeed;
    public Vector2 curspeed;
    public float move;
    public bool grounded;
    public SpriteRenderer rend;
    public BoxCollider2D col;
    // Start is called before the first frame update
    void Update()
    {
        curmaxspeed = (Input.GetKey(KeyCode.LeftShift)?speed:runspeed);
        move = Input.GetAxisRaw("Horizontal");
        curspeed.x += move * acc * curmaxspeed;
        if(Mathf.Abs(curspeed.x) > curmaxspeed || (move == 0 && Mathf.Abs(curspeed.x) > Time.deltaTime * acc)) 
        {
            curspeed.x -= Mathf.Sign(curspeed.x) * acc * 0.5f;
            if(Mathf.Abs(curspeed.x) < acc) curspeed.x = 0;
        }
        curspeed.x = Mathf.Clamp(curspeed.x, -curmaxspeed, curmaxspeed);
        if(grounded == false) curspeed.y -= gravity;
        if(Input.GetKeyDown(KeyCode.W) && grounded == true)
        {
            curspeed.y = jumpspeed;
            grounded = false;
        }
        curspeed.y = Mathf.Clamp(curspeed.y, -maxfallspeed, jumpspeed);
        transform.position += (Vector3)curspeed * Time.deltaTime;
        while(Physics2D.OverlapBox((Vector2)transform.position + col.offset, col.size * transform.localScale, 0))
        {
            curspeed.y = 0;
            grounded = true;
            transform.position += Vector3.up * 0.02f;           
        }
    }
    void LateUpdate()
    {
        rend.flipX = curspeed.x < 0;
    }
}
