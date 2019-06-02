// Date   : 01.06.2019 23:39
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {

    [SerializeField]
    private RuntimeBool bossVisible;

    private bool hasBeenSeen = false;

    [SerializeField]
    private float visibleDistance = 5f;

    [SerializeField]
    private RuntimeVector3 playerPosition;

    void BecameVisible()
    {
        Debug.Log("Boss visible!");
        if (!hasBeenSeen) {
            bossVisible.Toggle = true;
            hasBeenSeen = true;
        }
    }

    void Update () {
        if (!hasBeenSeen) {
            float distance = Vector2.Distance(playerPosition.Value, transform.position);
            Debug.Log(distance);
            if (distance <= visibleDistance) {
                BecameVisible();
            }
        }
    }
}
