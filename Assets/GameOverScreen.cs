using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;

    public void ShowGameOverScreen(int score) {
        gameObject.SetActive(true);
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
