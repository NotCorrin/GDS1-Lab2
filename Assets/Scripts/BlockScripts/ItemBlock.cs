using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : Block
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void ActivateBlock()
    {
        IsActive = false;
    }

    protected override bool IsValid()
    {
        return IsActive;
    }

    // Update is called once per frame
}
