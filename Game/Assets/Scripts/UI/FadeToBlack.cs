// Date   : 02.06.2019 13:11
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class FadeToBlack : MonoBehaviour {

    [SerializeField]
    private MainMenu menu;

    public void LoadMission() {
        menu.LoadMission();
    }
}
