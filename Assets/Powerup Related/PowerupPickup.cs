using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FireFlower")
        {
            Destroy(collision.gameObject);
            GameManager.PickUpFireFlower();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mushroom")
        {
            Destroy(collision.gameObject);
            GameManager.PickUpMushroom();
        }

        if (collision.gameObject.tag == "OneUp")
        {
            Destroy(collision.gameObject);
            GameManager.Lives++;
        }

        if (collision.gameObject.tag == "Star")
        {
            Destroy(collision.gameObject);
            GameManager.PickUpStar();
        }
    }
}
