// Date   : 01.06.2019 11:34
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject fadeToBlack;

    void Start() {
        AudioPlayer.main.UnPauseMenuMusic();
    }

    int nextMission = 0;
    public void StartMission(int mission) {
        nextMission = mission;
        AudioPlayer.main.PauseMenuMusic();
        fadeToBlack.SetActive(true);
    }

    public void LoadMission() {
        SceneManager.LoadScene(nextMission);
    }

}
