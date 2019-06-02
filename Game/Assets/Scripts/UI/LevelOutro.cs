// Date   : 02.06.2019 13:24
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelOutro : MonoBehaviour
{

    [SerializeField]
    private RuntimeBool playerDied;

    [SerializeField]
    private RuntimeBool enemyDied;

    [SerializeField]
    private RuntimeInt levelsFinished;

    [SerializeField]
    private ConfigInt levelNumber;

    [SerializeField]
    private Text endText;

    [SerializeField]
    private string failText = "You died!";
    [SerializeField]
    private string successText = "Well done!";

    private bool started = false;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerDied.Toggle = false;
        enemyDied.Toggle = false;
    }

    void Update()
    {
        if (!started && (playerDied.Toggle || enemyDied.Toggle))
        {
            started = true;
            endText.text = playerDied.Toggle ? failText : successText;
            animator.enabled = true;

            if (enemyDied.Toggle && !playerDied.Toggle)
            {
                if (levelsFinished.Count < levelNumber.Value)
                {
                    levelsFinished.Count = levelNumber.Value;
                }
            }
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
