using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCoin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Coins++;
        GameManager.Score += 200;

        Destroy(gameObject);
    }
}
