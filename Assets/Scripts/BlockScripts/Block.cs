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

    protected virtual void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        anim = gameObject.GetComponent<Animator>();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsValid() && IsActive && collision.transform.position.y < transform.position.y - 0.08f)
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
        anim.SetTrigger("recoil");
    }


}
