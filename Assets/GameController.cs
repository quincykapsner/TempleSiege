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
        statue.chargeRate = 0; // stop charging
        gameOverScreen.ShowGameOverScreen(statue.getScore(), cause); 
    }

    public void Win() {
        SceneManager.LoadScene("Credits");
    }

    // Start is called before the first frame update
    void Start()
    {
        statue = FindObjectOfType<Statue>();
        gameOverScreen.HideGameOverScreen();
    }
}
