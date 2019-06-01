// Date   : 01.06.2019 11:34
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private GameObject world;
    [SerializeField]
    private GameObject canvas;

    public void StartMission(int mission) {
        canvas.SetActive(false);
        world.SetActive(true);
        AudioPlayer.main.StopMenuMusic();
        Debug.Log("Start");
    }

}
