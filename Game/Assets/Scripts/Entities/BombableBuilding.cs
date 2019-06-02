// Date   : 01.06.2019 15:41
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombableBuilding : MonoBehaviour {

    [SerializeField]
    private GameObject explosionEffect;

    private BoxCollider2D boxCollider2D;

    [SerializeField]
    private float effectDuration = 1;

    private float effectTimer = 0f;

    private bool effectInEffect = false;

    [SerializeField]
    private bool mustBeBombed = false;

    [SerializeField]
    private Sprite destroyedSprite;

    [SerializeField]
    private List<GameObject> deletableObjects;

    [SerializeField]
    private RuntimeInt objectivesAccomplished;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject prefabToSpawnAfterDeath;

    void Start () {
        if  (mustBeBombed) {
            objectivesAccomplished.Target += 1;
        }
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update () {
        if (effectInEffect) {
            effectTimer += Time.deltaTime;
            if (effectTimer > effectDuration) {
                spriteRenderer.sprite = destroyedSprite;
                foreach(GameObject obj in deletableObjects) {
                    Destroy(obj);
                }
            }
        }
    }

    void Kill() {
        boxCollider2D.enabled = false;
        if  (mustBeBombed) {
            objectivesAccomplished.Count += 1;
        }
        effectInEffect = true;
        if (prefabToSpawnAfterDeath != null) {
            Instantiate(prefabToSpawnAfterDeath);
        }
        AudioPlayer.main.PlaySound(GameEvent.BuildingExplode);
        explosionEffect.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D collision2D) {
        if (collision2D.gameObject.tag == "Bomb") {
            Kill();
        }
    }
}
