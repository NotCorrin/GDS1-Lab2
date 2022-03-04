using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    /*
     * Updating any variables will call the appropriate listener function
     * CurrentPlayuerState and CurrentGameState are read-only: use the following methods to manipulate them
     * GameManager.StartGame();
     * GameManager.GameOver();
     * GameManager.GameWin();
     * GameManager.ResetGame();
     * GameManager.PickPpMushroom();
     * GameManager.PickUpFireFlower();
     * GameManager.PickUpStar();
     * GameManager.PlayerHurt();
     * GameManager.KillPlayer();
     * GameManager.RestartLevel();
     * GameManager.Respawn();
     */


    public virtual void OnGameStart()
    {

    }

    public virtual void OnGameOver()
    {

    }

    public virtual void OnGameWin()
    {

    }

    public virtual void OnGameReset()
    {

    }

    public virtual void OnLevelRestarted()
    {

    }

    public virtual void OnGamePaused()
    {

    }

    public virtual void OnScoreChange()
    {

    }

    public virtual void OnLifeChange()
    {

    }

    public virtual void OnPlayerStateChanged()
    {

    }

    /*public virtual void OnTimeChange()
    {

    }*/
}
