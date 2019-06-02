// Date   : 02.06.2019 20:47
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    [SerializeField]
    private GameObject container;

    private bool menuOpen = false;

    public void Restart () {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void OpenMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    void OpenMenu() {
        menuOpen = true;
        Time.timeScale = 0f;
        container.SetActive(true);
    }

    public void CloseMenu() {
        menuOpen = false;
        Time.timeScale = 1f;
        container.SetActive(false);
    }


    public void Quit() {
        Application.Quit();
    }

    void Update() {
        if (!menuOpen && Input.GetKeyDown(KeyCode.Q)) {
            OpenMenu();
        }
    }

}
