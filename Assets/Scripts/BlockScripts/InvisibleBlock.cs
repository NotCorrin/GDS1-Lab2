using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBlock : Block
{
    public int numHit = 1;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame

    protected override void ActivateBlock()
    {
        spriteRenderer.enabled = true;
        IsActive = false;
    }

    protected override bool IsValid()
    {
        return IsActive;
    }
}
