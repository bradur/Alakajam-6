// Date   : 01.06.2019 23:36
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class BossTransition : MonoBehaviour
{

    [SerializeField]
    private RuntimeBool bossCloseToPlayer;

    [SerializeField]
    private RuntimeBool muteSounds;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private RuntimeInt objectivesAccomplished;

    private GameObject enemy;

    private bool enemySpawned = false;

    private bool done = false;

    [SerializeField]
    private GameObject radar;

    void Start()
    {
        bossCloseToPlayer.Toggle = false;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {
        if (!done)
        {
            if (bossCloseToPlayer.Toggle)
            {
                bossCloseToPlayer.Toggle = false;
                BeginTransition();
            }
            if (!enemySpawned && objectivesAccomplished.Accomplished)
            {
                SpawnEnemy();
            }
        }

    }

    void SpawnEnemy()
    {
        radar.SetActive(true);
        enemySpawned = true;
        enemy.transform.GetChild(0).gameObject.SetActive(true);
    }

    void BeginTransition()
    {
        //Time.timeScale = 0f;
        animator.enabled = true;
    }

    public void StopTime()
    {
        muteSounds.Toggle = true;
        AudioPlayer.main.GameMusicToBossMusic();
        Time.timeScale = 0f;
    }

    public void EndTransition()
    {
        done = true;
        muteSounds.Toggle = false;
        Time.timeScale = 1f;
        //gameObject.SetActive(false);
    }




}
