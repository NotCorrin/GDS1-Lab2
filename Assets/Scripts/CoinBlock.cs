using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : Block
{
    private int numCoins = 13;
    [SerializeField] private Animator coinAnim;

    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();

        coinAnim = GameObject.Find("CB Coin").GetComponent<Animator>();
    }

    // Update is called once per frame

    protected override void ActivateBlock()
    {
        StartCoroutine(RecoilCoin());

        GameManager.Coins++;

        numCoins -= 1;

        Debug.Log("Coin Block Hit. Coins: " + numCoins);
    }

    protected override bool IsValid()
    {
        return numCoins > 0;
    }

    IEnumerator RecoilCoin()     //makes coin appear when coin block is hit
    {
        coinAnim.SetBool("recoil", true);

        yield return new WaitForSeconds(0.2f);

        coinAnim.SetBool("recoil", false);
    }

}
