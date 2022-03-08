using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBlock : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator anim;

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

    IEnumerator RecoilBlock()
    {
        anim.SetBool("recoil", true);

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.enabled = true;
        anim.SetBool("recoil", false);
    }
}
