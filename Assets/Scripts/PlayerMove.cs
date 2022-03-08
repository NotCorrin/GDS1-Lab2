using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float jumpspeed = 1, acc = 1, maxfallspeed = 1, speed = 5, runspeed = 8.5f;
    public float curmaxspeed;
    public float move;
    public bool grounded;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private SpriteRenderer rend;
    private bool capoff;
    private bool facing;
    public float someRadius = 0.5f;
    // Start is called before the first frame update
    void Update()
    {
        curmaxspeed = (Input.GetKey(KeyCode.LeftShift)?runspeed:speed); //check if running
        move = Input.GetAxisRaw("Horizontal"); //input
        if(move == 1) facing = true;
        else if (move == -1) facing = false;

        rb.AddForce(move * acc * Vector3.right * Time.deltaTime, ForceMode2D.Impulse); //output
        //if(Mathf.Sign(curspeed.x) > curmaxspeed || (move == 0 && Mathf.Sign(curspeed.x) > Time.deltaTime * acc)) curspeed.x -= Time.deltaTime * Mathf.Sign(curspeed.x) * acc;
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -curmaxspeed, curmaxspeed), Mathf.Clamp(rb.velocity.y, -maxfallspeed, jumpspeed)); //clamp velocity to walk/run speed
        if(grounded == true)
        {
            if(Mathf.Abs(rb.velocity.x) < 0.1f) rb.velocity = Vector2.zero; //stop moving

            if(Mathf.Abs(rb.velocity.x) > curmaxspeed || (move == 0 && Mathf.Abs(rb.velocity.x) > Time.deltaTime * acc)) //if not moving or over speed limit
            {
                //rb.velocity -= Mathf.Sign(rb.velocity.x) * acc * 0.5f * Vector2.right;
                if(Mathf.Abs(rb.velocity.x) < 0.1f) rb.velocity = Vector2.zero; //stop moving
                rb.AddForce(Mathf.Sign(rb.velocity.x) * acc * 0.5f * Vector2.left); //go back
                if(Mathf.Abs(rb.velocity.x) < 0.1f) rb.velocity = Vector2.zero; //please for the love of god stop
            }
        }

        //jump
        if(Input.GetKeyDown(KeyCode.W) && grounded == true)
        {
            rb.AddForce((jumpspeed + Mathf.Pow(Mathf.Abs(rb.velocity.x), 1.6f)) * Vector2.up * 0.5f, ForceMode2D.Impulse);
            Invoke("Jump", Time.deltaTime);
            grounded = false;
        }
        if((Input.GetKeyUp(KeyCode.W) || rb.velocity.y < 0) && grounded == false) capoff = true;
        if(capoff) {
            rb.AddForce(Vector2.down * 6);
            if(rb.velocity.y > 0)rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.9f);
        }
    }
    void LateUpdate()
    {
        Vector3 pos = transform.position;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        // Checks if the current pos is at the left of the camera
        //Debug.Log(-widthOrtho);

        if (pos.x - someRadius < Camera.main.transform.position.x - widthOrtho)
        {
            pos.x = Camera.main.transform.position.x - widthOrtho + someRadius;
            if (rb.velocity.x < 0) rb.velocity = new Vector2(0, rb.velocity.y);
        }

        transform.position = pos;

        rend.flipX = !facing;
    }
    void Jump()
    {
        rb.AddForce((jumpspeed + Mathf.Pow(Mathf.Abs(rb.velocity.x), 1.6f)) * Vector2.up * 0.5f, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        float height = collision.contacts[1].point.y;
        if (collision.contacts[0].point.y == height && height < transform.position.y)
        {
            grounded = true;
        }
        capoff = false;
    }
}
