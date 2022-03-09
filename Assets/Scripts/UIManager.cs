using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Listener
{
    [SerializeField] GameObject scoreUI;
    [SerializeField] GameObject coinsUI;
    [SerializeField] GameObject timeUI;

    void Start()
    {
        GameManager.AddListener(this);

        if (!scoreUI)
        {
            scoreUI = GameObject.Find("Score");
        }
        if (!coinsUI)
        {
            coinsUI = GameObject.Find("Coins");
        }
        if (!timeUI)
        {
            timeUI = GameObject.Find("Time");
        }
    }

    void Update()
    {
        if (GameManager.CurrentGameState == GameManager.GameState.playing)
        {
            // Update the time in UI
            timeUI.GetComponent<Text>().text = (GameManager.GameTime).ToString("000");
        }
    }

    public override void OnScoreChange()
    {
        base.OnScoreChange();
        // Update the score in UI
        scoreUI.GetComponent<Text>().text = GameManager.Score.ToString("000000");
    }

    public override void OnCoinGet()
    {
        base.OnCoinGet();
        // Update the coins in UI
        coinsUI.GetComponent<Text>().text = GameManager.Coins.ToString("00");
    }
}
