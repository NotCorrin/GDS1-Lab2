using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public bool runOffEdge = true;
    public bool startFacingLeft = true;
    public Transform graphicsTransform;
    [Space]
    public LayerMask turnAroundLayer;
    public LayerMask groundLayer;
    public Transform turnAroundCheckPoint;
    public Transform edgeCheckPoint;

    private bool facingLeft;
    private float turnCheckRange = 0.02f;
    private float edgeCheckRange = 0.02f;

    private Rigidbody2D rb;
    private Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected virtual void Move() {
        bool groundAhead = Physics2D.OverlapCircle(edgeCheckPoint.position, edgeCheckRange, groundLayer);
        if (groundAhead == false && !runOffEdge) {
            Flip();
        }

        bool turnAround = Physics2D.OverlapCircle(turnAroundCheckPoint.position, turnCheckRange, turnAroundLayer);
		if (turnAround) {
            Flip();
        }

        rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0);

    }

    private void Flip() {
        facingLeft = !facingLeft;
		if (facingLeft) {
            graphicsTransform.localScale = new Vector3(-graphicsTransform.localScale.x, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
            moveSpeed = -moveSpeed;
		} else {
            graphicsTransform.localScale = new Vector3(-graphicsTransform.localScale.x, graphicsTransform.localScale.y, graphicsTransform.localScale.z);
            moveSpeed = -moveSpeed;
        }
    }
}
