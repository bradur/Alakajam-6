// Date   : 01.06.2019 15:41
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombableBridgeHotspot : MonoBehaviour {

    [SerializeField]
    private GameObject explosionEffect;

    private BoxCollider2D boxCollider2D;

    [SerializeField]
    private float effectDuration = 1;

    private float effectTimer = 0f;

    private bool effectInEffect = false;

    private bool killed = false;
    
    void Start () {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update () {
        if (effectInEffect) {
            effectTimer += Time.deltaTime;
            if (effectTimer > effectDuration) {
            }
        }
    }

    void Kill() {
        boxCollider2D.enabled = false;
        effectInEffect = true;

        AudioPlayer.main.PlaySound(GameEvent.BuildingExplode);
        explosionEffect.SetActive(true);
        killed = true;
    }

    void OnCollisionEnter2D(Collision2D collision2D) {
        if (collision2D.gameObject.tag == "Bomb") {
            Kill();
        }
    }

    public bool IsKilled()
    {
        return killed;
    }
}
