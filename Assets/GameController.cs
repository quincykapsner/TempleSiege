using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameOverScreen gameOverScreen;

    public void GameOver() {
        gameOverScreen.ShowGameOverScreen(14); // 14 just for testing
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.HideGameOverScreen();
    }
}
