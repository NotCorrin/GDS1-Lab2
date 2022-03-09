using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : Block
{
    private int numCoins = 13;
    [SerializeField] private Animator coinAnim;

    // Start is called before the first frame update

    void Start()
    {
        coinAnim = GameObject.Find("CB Coin").GetComponent<Animator>();
    }

    // Update is called once per frame

    protected override void ActivateBlock()
    {
        //StartCoroutine(RecoilCoin());

        coinAnim.SetTrigger("recoil");

        Debug.Log("Coin Block Activated");

        GameManager.Coins++;

        numCoins -= 1;

        Debug.Log("Coin Block Hit. Coins: " + numCoins);
    }

    protected override bool IsValid()
    {
        return numCoins > 0;
    }

}
