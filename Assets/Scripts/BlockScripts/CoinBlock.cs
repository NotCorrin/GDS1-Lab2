using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : Block
{
    [SerializeField] protected int numCoins = 13;
    [SerializeField] private Animator coinAnim;

    // Start is called before the first frame update

    // Update is called once per frame

    protected override void ActivateBlock()
    {
        //StartCoroutine(RecoilCoin());

        coinAnim.SetTrigger("recoil");

        GameManager.Coins++;

        numCoins -= 1;

        Debug.Log("Coin Block Hit. Coins: " + numCoins);
    }

    protected override bool IsValid()
    {
        return numCoins > 0;
    }

}
