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
    private RuntimeBool muteSounds;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private RuntimeInt objectivesAccomplished;

    private GameObject enemy;

    private bool enemySpawned = false;

    void Start() {
        bossVisible.Toggle = false;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update() {
        if (bossVisible.Toggle) {
            bossVisible.Toggle = false;
            BeginTransition();
        }
        if (!enemySpawned && objectivesAccomplished.Accomplished) {
            enemySpawned = true;
            enemy.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void BeginTransition()
    {
        //Time.timeScale = 0f;
        animator.enabled = true;
    }

    public void StopTime() {
        muteSounds.Toggle = true;
        AudioPlayer.main.GameMusicToBossMusic();
        Time.timeScale = 0f;
    }

    public void EndTransition()
    {
        muteSounds.Toggle = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }




}
