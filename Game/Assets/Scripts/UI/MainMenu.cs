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

    public void StartMission(int mission) {
        gameObject.SetActive(false);
    }

}
