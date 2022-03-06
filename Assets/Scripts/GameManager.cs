using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { start, playing, paused, gameOver, gameWin };
    static private GameState currentGameState;
    static public GameState CurrentGameState
    {
        get => currentGameState;
    }


    public enum PlayerState { normal, mushroom, fireflower, star, dead };
    static private PlayerState currentPlayerState;
    static public PlayerState CurrentPlayerState
    {
        get => currentPlayerState;
    }

    static private List<Listener> listenerList = new List<Listener>();

    static private int score; //Property for score
    public int Score
    {
        get => score;
        set
        {
            score = value;
            foreach (Listener listener in listenerList)
            {
                listener.OnScoreChange();
            }
        }
    }

    static private int lives; //Property for lives
    static public int Lives
    {
        get => lives;
        set
        {
            if (lives > value)
            {
                lives = value;
                foreach (Listener listener in listenerList)
                {
                    listener.OnLifeLost();
                }
            }
            else
            {
                lives = value;
                foreach (Listener listener in listenerList)
                {
                    listener.OnLifeGet();
                }
            }

        }
    }

    static private int coins;
    static private int coinLives;
    static public int Coins
    {
        get => coins;
        set
        {
            coins = value;
            foreach (Listener listener in listenerList)
            {
                listener.OnCoinGet();
            }

            if (coins > (coinLives+1) * 100)
            {
                coinLives++;
                Lives++;
            }
        }
    }


    static private float gameTime; //Property for time
    static public float GameTime
    {
        get => gameTime;
        set
        {
            gameTime = value;

            //Listener pattern, but time updates every frame anyways so pointless

            /*
            foreach (Listener listener in listenerList)
            {
                listener.OnTimeChange();
            }
            */
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.playing)
        {
            GameTime -= 0.4f * Time.deltaTime;

            if (GameTime < 0)
            {
                KillPlayer();
            }
        }
    }

    //Method for listeners to add themselves
    static public void AddListener(Listener listener)
    {
        listenerList.Add(listener);
    }

    static public void RemoveListener(Listener listener)
    {
        listenerList.Remove(listener);
    }

    //Methods which set game state
    static public void StartGame()
    {
        currentGameState = GameState.playing;
        Time.timeScale = 1;

        gameTime = 400.0f;

        foreach (Listener listener in listenerList)
        {
            listener.OnGameStart();
        }
    }

    static public void GameOver()
    {
        currentGameState = GameState.gameOver;

        foreach (Listener listener in listenerList)
        {
            listener.OnGameOver();
        }
    }

    static public void GameWin()
    {
        currentGameState = GameState.gameWin;

        foreach (Listener listener in listenerList)
        {
            listener.OnGameWin();
        }
    }

    static public void ResetGame()
    {
        currentGameState = GameState.start;
        coinLives = 0;
        coins = 0;
        lives = 3;
        score = 0;
        ResetPlayer();
        Time.timeScale = 1;

        foreach (Listener listener in listenerList)
        {
            listener.OnGameReset();
        }
    }

    static public void PauseGame()
    {
        currentGameState = GameState.paused;
        Time.timeScale = 0;

        foreach (Listener listener in listenerList)
        {
            listener.OnGameReset();
        }
    }

    static public void RestartLevel()
    {
        currentGameState = GameState.paused;
        Time.timeScale = 1;

        foreach (Listener listener in listenerList)
        {
            listener.OnLevelRestarted();
        }
    }

    // Methods that control player state

    public static void PickUpMushroom()
    {
        currentPlayerState = PlayerState.mushroom;

        foreach (Listener listener in listenerList)
        {
            listener.OnPlayerStateChanged();
        }
    }

    public static void PickUpFireFlower()
    {
        currentPlayerState = PlayerState.fireflower;

        foreach (Listener listener in listenerList)
        {
            listener.OnPlayerStateChanged();
        }
    }

    public static void PickUpStar()
    {
        currentPlayerState = PlayerState.star;

        foreach (Listener listener in listenerList)
        {
            listener.OnPlayerStateChanged();
        }
    }

    public static void PlayerHurt()
    {
        if (currentPlayerState == PlayerState.fireflower || currentPlayerState == PlayerState.mushroom)
        {
            currentPlayerState = PlayerState.normal;

            foreach (Listener listener in listenerList)
            {
                listener.OnPlayerStateChanged();
            }
        }
        else if (currentPlayerState == PlayerState.normal)
        {
            KillPlayer();
        }

    }

    public static void KillPlayer()
    {
        currentPlayerState = PlayerState.dead;

        PauseGame();

        foreach (Listener listener in listenerList)
        {
            listener.OnPlayerStateChanged();
        }
    }

    public static void ResetPlayer()
    {
        currentPlayerState = PlayerState.normal;

        foreach (Listener listener in listenerList)
        {
            listener.OnPlayerStateChanged();
        }
    }

    public static void Respawn()
    {
        Lives -= 1;

        if (Lives < 0)
        {
            GameOver();
        }
        else
        {
            RestartLevel();
            StartGame();
        }

        ResetPlayer();
    }

}
