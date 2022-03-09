using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    protected Animator anim;

    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite newSprite;

    protected bool IsActive = true;

    protected List<Enemy> enemiesInContact = new List<Enemy>();

    // Start is called before the first frame update

    protected virtual void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        anim = gameObject.GetComponent<Animator>();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsActive)
        {

            if (collision.gameObject.tag.Equals("Enemy"))
            {
                if (collision.GetContact(0).point.y > transform.position.y + 0.5f)
                {
                    enemiesInContact.Add(collision.gameObject.GetComponent<Enemy>());
                }
            }

            else if (collision.gameObject.tag.Equals("Player"))
            {
                if (IsValid() && IsActive && collision.GetContact(0).point.y < transform.position.y - 0.5f)
                {
                    ActivateBlock();
                    AnimateBlock();

                    if (!IsValid())
                    {
                        DisableBlock();
                    }
                }

            }
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Enemy enemyComponent = collision.gameObject.GetComponent<Enemy>();

            if (enemiesInContact.Contains(enemyComponent))
            {
                enemiesInContact.Remove(enemyComponent);
            }
        }
            
    }

    abstract protected void ActivateBlock();

    protected void KillEnemiesInContact()
    {
        foreach (Enemy enemy in enemiesInContact)
        {
            enemy.OnHit();
        }
    }

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
