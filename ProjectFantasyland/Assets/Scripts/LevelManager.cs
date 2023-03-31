using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject pauseScreen;
    
    public void levels(int index) {
        SceneManager.LoadScene(index);
    }

     //Replay Current Level
    public void Replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single); 
    }

    public void Resume() {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause() {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void startMenu() {
        SceneManager.LoadScene("StartMenu");
    }

    public void exitGame() {
        Application.Quit();
        print("Exited Game");
    }
}