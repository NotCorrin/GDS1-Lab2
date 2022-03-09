using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Listener
{
    // Start is called before the first frame update
    [SerializeField] static protected Scene currentLevel;

    void Awake()
    {
        GameManager.AddListener(this);

    }
    
    static public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevel.buildIndex);
    }

    public override void OnGameStart()
    {
        SceneManager.LoadScene("World");
        currentLevel = SceneManager.GetActiveScene();
    }

    public override void OnGameReset()
    {
        SceneManager.LoadScene("Menu");
    }

    public override void OnGameOver()
    {
        SceneManager.LoadScene("Menu");
    }

    public override void OnLevelRestarted()
    {
        RestartLevel();
    }
}
