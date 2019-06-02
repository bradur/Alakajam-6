// Date   : 01.06.2019 23:39
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {

    [SerializeField]
    private RuntimeBool bossCloseToPlayer;
    [SerializeField]
    private RuntimeBool bossVisible;

    private bool hasBeenSeen = false;

    [SerializeField]
    private float visibleDistance = 5f;

    [SerializeField]
    private RuntimeVector3 playerPosition;

    [SerializeField]
    private RuntimeVector3 bossPosition;

    void BecameVisible()
    {
        if (!hasBeenSeen) {
            bossCloseToPlayer.Toggle = true;
            hasBeenSeen = true;
        }
    }

    void OnBecameVisible() {
        bossVisible.Toggle = true;
    }
    void OnBecameInvisible() {
        bossVisible.Toggle = false;
    }

    void Update () {
        bossPosition.Value = transform.position;
        if (!hasBeenSeen) {
            float distance = Vector2.Distance(playerPosition.Value, transform.position);
            if (distance <= visibleDistance) {
                BecameVisible();
            }
        }
    }
}
