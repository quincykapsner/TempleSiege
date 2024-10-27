using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public Statue statue;

    public void GameOver(string cause) {
        gameOverScreen.ShowGameOverScreen(statue.getScore(), cause); 
    }

    // Start is called before the first frame update
    void Start()
    {
        statue = FindObjectOfType<Statue>();
        gameOverScreen.HideGameOverScreen();
    }
}
