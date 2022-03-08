using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    public Animator cbAnim;
    public Animator coinAnim;

    private int numCoins = 13;

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    // Start is called before the first frame update
    void Start()
    {
        cbAnim = gameObject.GetComponent<Animator>();

        coinAnim = GameObject.Find("CB Coin").GetComponent<Animator>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (numCoins > 0 && collision.transform.position.y < transform.position.y - 0.8)
        {
            StartCoroutine(RecoilBlock());

            StartCoroutine(RecoilCoin());

            GameManager.Coins++;

            numCoins -= 1;
        }
        else if (numCoins == 0)
        {
            spriteRenderer.sprite = newSprite;
        }
    }

    IEnumerator RecoilBlock()   //makes coin block move when hit
    {
        cbAnim.SetBool("recoil", true);

        yield return new WaitForSeconds(0.2f);

        cbAnim.SetBool("recoil", false);
    }

    IEnumerator RecoilCoin()     //makes coin appear when coin block is hit
    {
        coinAnim.SetBool("recoil", true);

        yield return new WaitForSeconds(0.2f);

        coinAnim.SetBool("recoil", false);
    }
}
