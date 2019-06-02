// Date   : 01.06.2019 23:39
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {

    [SerializeField]
    private RuntimeBool bossVisible;

    private bool hasBeenSeen = false;

    void OnBecameVisible()
    {
        Debug.Log("Boss visible!");
        if (!hasBeenSeen) {
            bossVisible.Toggle = true;
            hasBeenSeen = true;
        }
    }
}
