using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ExitButton() {
        Application.Quit();
        Debug.Log("Game closed"); // for editor
    }

    public void StartButton() {
        SceneManager.LoadScene("Start");
    }
}
