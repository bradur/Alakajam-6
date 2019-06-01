// Date   : 01.06.2019 23:36
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class BossTransition : MonoBehaviour
{

    [SerializeField]
    private RuntimeBool bossVisible;

    [SerializeField]
    private Animator animator;

    void Update() {
        if (bossVisible.Toggle) {
            bossVisible.Toggle = false;
            BeginTransition();
        }
    }

    void BeginTransition()
    {
        //Time.timeScale = 0f;
        animator.enabled = true;
    }

    public void StopTime() {
        Time.timeScale = 0f;
    }

    public void EndTransition()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }




}
