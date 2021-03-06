using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : Listener
{
    [SerializeField]
    private float jumpspeed = 1, acc = 1, maxfallspeed = 1, speed = 5, runspeed = 8.5f;
    public float curmaxspeed;
    public float move;
    public bool grounded;
    public Rigidbody2D rb;
    [SerializeField]
    private SpriteRenderer rend;
    [SerializeField]
    private BoxCollider2D col;    
    [SerializeField]
    private Animator anim;
    public Sprite[] big_sprites;
    private bool capoff;
    private bool facing;
    public float someRadius = 0.5f;
    public bool isLocked;
    public bool runaway;
    // Start is called before the first frame update
    public override void OnPlayerStateChanged()
    {
        if(GameManager.CurrentPlayerState == GameManager.PlayerState.dead) 
        {
            GameManager.ResetPlayer(); 
            SceneManager.LoadScene(1);
        }
        else if(GameManager.CurrentPlayerState == GameManager.PlayerState.normal) Shrink();
        else Grow();
    }

    void Grow()
    {
        col.size = new Vector2(1,2);

        anim.SetInteger("size", (int)GameManager.CurrentPlayerState);
        anim.SetTrigger("Grow");
    }   

    void Shrink()
    {
        col.size = Vector2.one;

        anim.SetInteger("size", (int)GameManager.CurrentPlayerState);
        anim.SetTrigger("Shrink");
    }

    void Awake()
    {
        GameManager.AddListener(this);
        //GameManager.listenerList.Add(this);
        col.size = GameManager.CurrentPlayerState == GameManager.PlayerState.normal?Vector2.one:new Vector2(1,2);
        anim.SetInteger("size", (int)GameManager.CurrentPlayerState);
    }

    void Start()
    {
        //GameManager.listenerList.Add(this);
        GameManager.AddListener(this);
    }

    void Update()
    {
        if(isLocked) 
        {
            if(runaway) 
            {
                rb.AddForce(1 * acc * Vector3.right * Time.deltaTime, ForceMode2D.Impulse);
                rb.gravityScale = 1;
            }
            else 
            {
                rb.gravityScale = 0;
            }
            return;
        }
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
                rb.AddForce(Mathf.Sign(rb.velocity.x) * (acc-Mathf.Abs(rb.velocity.x)) * 0.15f * Vector2.left); //go back
                if(Mathf.Abs(rb.velocity.x) < 0.1f) rb.velocity = Vector2.zero; //please for the love of god stop
            }
        }

        //jump
        if(Input.GetKeyDown(KeyCode.W) && (grounded == true) && rb.velocity.y > -0.5f)
        {
            //Debug.Log(GameManager.listenerList.Count);
            rb.AddForce((jumpspeed + Mathf.Pow(Mathf.Abs(rb.velocity.x), 1.6f)) * Vector2.up * 0.5f, ForceMode2D.Impulse);
            Invoke("Jump", Time.deltaTime);
            grounded = false;
            anim.SetBool("isJumping", true);
        }
        if((Input.GetKeyUp(KeyCode.W) || rb.velocity.y < 0) && grounded == false) capoff = true;
        if(capoff) {
            rb.AddForce(Vector2.down * 6);
            if(rb.velocity.y > 0)rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.9f);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)) Grow();
        if(Input.GetKeyDown(KeyCode.Alpha2)) Shrink();
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        // Checks if the current pos is at the left of the camera

        if (pos.x - someRadius < Camera.main.transform.position.x - widthOrtho)
        {
            pos.x = Camera.main.transform.position.x - widthOrtho + someRadius;
            if (rb.velocity.x < 0) rb.velocity = new Vector2(0, rb.velocity.y);
        }

        transform.position = pos;

        //Animation
        rend.flipX = !facing;
        anim.SetFloat("xspeed", Mathf.Abs(rb.velocity.x)/2);
        anim.SetBool("isSliding", Mathf.Sign(rb.velocity.x) == -move && Mathf.Abs(rb.velocity.x) > 1.5f);

        if(transform.position.y < -6) SceneManager.LoadScene(1);
    }

    void Jump()
    {
        rb.AddForce((jumpspeed + Mathf.Pow(Mathf.Abs(rb.velocity.x), 1.6f)) * Vector2.up * 0.5f, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Fireball(Clone)") return;
        if(isLocked) runaway = true;
        float height = Mathf.FloorToInt(collision.contacts[1].point.y*1000)/1000f;
        if (Mathf.FloorToInt(collision.contacts[0].point.y*1000)/1000f == Mathf.FloorToInt(collision.contacts[1].point.y*1000)/1000f && height < transform.position.y + 0.1f)
        {
            Debug.Log(collision.gameObject.name);
            grounded = true;
            anim.SetBool("isJumping", false);
        }
        capoff = false;
    }
}
