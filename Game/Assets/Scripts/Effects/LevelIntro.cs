// Date   : 02.06.2019 11:23
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class LevelIntro : MonoBehaviour {


    [SerializeField]
    private RuntimeBool playerControlsEnabled;

    [SerializeField]
    private HotkeyMap hotkeyMap;

    private Animator animator;

    private bool started = false;

    void Start() {
        animator = GetComponent<Animator>();
        playerControlsEnabled.Toggle = false;
    }

    public void StartLevel() {
        started = true;
        playerControlsEnabled.Toggle = true;
        gameObject.SetActive(false);
    }

    void Update () {
        if (!started && hotkeyMap.GetKeyDown(GameAction.SkipCutScene)) {
            started = true;
            animator.SetTrigger("End");
        }
    }
}
