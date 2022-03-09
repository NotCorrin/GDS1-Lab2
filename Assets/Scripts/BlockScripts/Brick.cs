using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : Block
{

    [SerializeField] private ParticleSystem brickEmitter;
    [SerializeField] Collider2D brickCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void ActivateBlock()
    {
        if (GameManager.CurrentPlayerState != GameManager.PlayerState.normal)
        {
            brickEmitter.Play();
            IsActive = false;

            spriteRenderer.enabled = false;
            brickCollider.enabled = false;
            //Destroy(this.gameObject);
        }
    }

    protected override bool IsValid()
    {
        return IsActive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
