using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    protected Animator anim;


    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite newSprite;

    protected bool IsActive = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        anim = gameObject.GetComponent<Animator>();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsValid() && IsActive && collision.transform.position.y < transform.position.y - 0.8)
        {
            ActivateBlock();
            AnimateBlock();

            if (!IsValid())
            {
                DisableBlock();
            }
        }
    }

    abstract protected void ActivateBlock();

    protected void DisableBlock()
    {
        spriteRenderer.sprite = newSprite;
        IsActive = false;
    }

    abstract protected bool IsValid();

    protected void AnimateBlock()
    {
        StartCoroutine(RecoilBlock());
    }

    protected IEnumerator RecoilBlock()   //makes coin block move when hit
    {
        anim.SetBool("recoil", true);

        yield return new WaitForSeconds(0.2f);

        anim.SetBool("recoil", false);
    }


}
