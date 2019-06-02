// Date   : 01.06.2019 11:34
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    void Start() {
        AudioPlayer.main.UnPauseMenuMusic();
    }

    public void StartMission(int mission) {
        AudioPlayer.main.PauseMenuMusic();
        SceneManager.LoadScene(mission);
    }

}
