using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Rigidbody2D mushroom;
    public float mushroomSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        mushroom = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mushroom.velocity = new Vector2(mushroomSpeed, mushroom.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }
        else
        mushroomSpeed *= -1;
    }

}
