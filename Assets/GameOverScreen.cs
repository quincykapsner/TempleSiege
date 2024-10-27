using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public Text causeText;

    public void ShowGameOverScreen(int score, string cause) {
        gameObject.SetActive(true);
        causeText.text = cause; 
        pointsText.text = "BARRIER " + score.ToString() + "% CHARGED";
    }

    public void HideGameOverScreen() {
        gameObject.SetActive(false);
    }

    public void RestartButton() {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenuButton() {
        SceneManager.LoadScene("MainMenu");
    }
}
