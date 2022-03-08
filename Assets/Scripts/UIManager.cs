using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            timeUI.GetComponent<TextMesh>().text = "Time: " + GameManager.GameTime;
        }
    }

    public override void OnScoreChange()
    {
        // Update the score in UI
        scoreUI.GetComponent<TextMesh>().text = "Score: " + GameManager.Score;
    }

    public override void OnCoinGet()
    {
        // Update the coins in UI
        coinsUI.GetComponent<TextMesh>().text = "Coins: " + GameManager.Coins;
    }
}
