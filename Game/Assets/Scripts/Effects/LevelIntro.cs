// Date   : 02.06.2019 11:23
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class LevelIntro : MonoBehaviour {


    [SerializeField]
    private RuntimeBool playerControlsEnabled;
    void Start() {
        playerControlsEnabled.Toggle = false;
    }

    public void StartLevel() {
        playerControlsEnabled.Toggle = true;
        gameObject.SetActive(false);
    }
}
