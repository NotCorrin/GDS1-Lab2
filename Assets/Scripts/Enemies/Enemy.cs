using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Enemy : MonoBehaviour {
    public float moveSpeed;
    public bool runOffEdge = true;
    public bool startFacingLeft = true;
    public Transform graphicsTransform;
    [Space]
    public LayerMask turnAroundLayer;
    public LayerMask groundLayer;
    public Transform turnAroundCheckPoint;
    public Transform edgeCheckPoint;
    public float distActivationFromCamera = 5;

    protected bool facingLeft;
    protected bool activated = false;
    private float turnCheckRange = 0.019f;
    private float edgeCheckRange = 0.019f;
    private float distToCamera;

    [Space]
    public SpriteRenderer spriteRend;

    protected bool isDead = false;

    protected Rigidbody2D rb;
    protected Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Grab the x distance from the camera
        distToCamera = transform.position.x - Camera.main.transform.position.x;

        // Calculate the distance to the edge of the camera
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        // If the enemy is within a certain distance to the camera
        if(distToCamera - widthOrtho < distActivationFromCamera) {
            activated = true;
		}

        // Move if activated
        if (activated == true) {
            if (isDead == false) {
                Move();
            }
        }
    }

    protected virtual void Move() {
        // Check if there is a ground ahead of the enemy
        bool groundAhead = Physics2D.OverlapCircle(edgeCheckPoint.position, edgeCheckRange, groundLayer);
        
        // If there IS ground ahead of the enemy...
        // runOffEdge if true, flip if its not
        if (groundAhead == false && !runOffEdge) {
            Flip();
        }

        // Check if there is a wall/enemy/pipe infront of the enemy
        bool turnAround = Physics2D.OverlapCircle(turnAroundCheckPoint.position, turnCheckRange, turnAroundLayer);
        Collider2D other = Physics2D.OverlapCircle(turnAroundCheckPoint.position, turnCheckRange, turnAroundLayer);
        // If there is... flip
        if ((gameObject.tag == "Shell") == false) {
            if (turnAround && other.tag != "Shell") {
                Flip();
            }
		} else {
            if (turnAround && other.tag != "Enemy") {
                Flip();
            }
        }

        // The current velocity
        rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0);

    }

    protected void Flip() {
        // If called, reverse 'facingLeft'
        facingLeft = !facingLeft;
        
        // and flip the sprite and moveSpeed
		if (facingLeft) {
            graphicsTransform.localScale = new Vector3(-graphicsTransform.localScale.x, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
            moveSpeed = -moveSpeed;
		} else {
            graphicsTransform.localScale = new Vector3(-graphicsTransform.localScale.x, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
            moveSpeed = -moveSpeed;
        }
    }
    public abstract void OnHit();
}
