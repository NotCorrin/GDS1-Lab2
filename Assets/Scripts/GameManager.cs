using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState { start, playing, gameOver, gameWin };
    static GameState currentGameState;

    enum PlayerState { normal, mushroom, fireflower, star };
    static private PlayerState currentPlayerState;

    static private List<Listener> listenerList = new List<Listener>();

    static private int score;
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

    static private int lives;
    static public int Lives
    {
        get => lives;
        set
        {
            lives = value;
            foreach (Listener listener in listenerList)
            {
                listener.OnLifeChange();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void AddListener(Listener listener)
    {
        listenerList.Add(listener);
    }
}
