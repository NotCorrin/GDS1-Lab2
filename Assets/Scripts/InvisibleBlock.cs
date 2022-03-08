using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBlock : Block
{
    public int numHit = 1;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (numHit == 1)
        {
            StartCoroutine(RecoilBlock());
            numHit = 0;
        }
    }

    protected override void ActivateBlock()
    {
        spriteRenderer.enabled = true;
    }

    protected override bool IsValid()
    {
        return IsActive;
    }

    IEnumerator RecoilBlock()
    {
        anim.SetBool("recoil", true);

        yield return new WaitForSeconds(0.2f);

        anim.SetBool("recoil", false);
    }
}
