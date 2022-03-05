using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Listener
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.AddListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    static public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public override void OnGameOver()
    {
        //Load game over scene
    }

    public override void OnLevelRestarted()
    {
        RestartLevel();
    }
}
