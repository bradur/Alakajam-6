// Date   : 01.06.2019 11:34
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject fadeToBlack;
    [SerializeField]
    private Image WeirdControlsCheckbox;
    [SerializeField]
    private Image NormalControlsCheckbox;
    [SerializeField]
    private Image InvertedControlsCheckbox;
    [SerializeField]
    private RuntimeBool IsInvertedControls;
    [SerializeField]
    private RuntimeBool IsNormalControls;
    [SerializeField]
    private RuntimeInt LevelsFinished;
    [SerializeField]
    private List<SuccessStamp> levelButtons;

    void Start()
    {
        AudioPlayer.main.UnPauseMenuMusic();
    }

    private void Update()
    {
        WeirdControlsCheckbox.enabled = !IsNormalControls.Toggle;
        NormalControlsCheckbox.enabled = IsNormalControls.Toggle;
        InvertedControlsCheckbox.enabled = IsInvertedControls.Toggle;

        if (LevelsFinished.Count >= levelButtons.Count)
        {
            //GAME FINISHED!
            levelButtons.ForEach(x => x.gameObject.SetActive(true));
            levelButtons.ForEach(x => x.SetSuccess(true));
        }
        else
        {
            levelButtons.ForEach(x => x.gameObject.SetActive(false));
            for (var i = 0; i < LevelsFinished.Count; i++)
            {
                levelButtons[i].SetSuccess(true);
                levelButtons[i].gameObject.SetActive(true);
            }

            levelButtons[LevelsFinished.Count].gameObject.SetActive(true);
            levelButtons[LevelsFinished.Count].SetSuccess(false);
            levelButtons[LevelsFinished.Count].enabled = true;
        }
    }

    int nextMission = 0;
    public void StartMission(int mission)
    {
        nextMission = mission;
        AudioPlayer.main.PauseMenuMusic();
        fadeToBlack.SetActive(true);
    }

    public void LoadMission()
    {
        SceneManager.LoadScene(nextMission);
    }

    public void InvertControls()
    {
        IsInvertedControls.Toggle = !IsInvertedControls.Toggle;
    }

    public void UpIsThrottle()
    {
        IsNormalControls.Toggle = true;
    }

    public void RightIsThrottle()
    {
        IsNormalControls.Toggle = false;
    }
}
