// Date   : 01.06.2019 09:34
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerDropBomb : MonoBehaviour {
    
    private BombDropper dropper;

    [SerializeField]
    private HotkeyMap hotkeyMap;
    
    private Rigidbody2D rb2D;

    [SerializeField]
    private RuntimeBool playerControlsEnabled;

    void Start()
    {
        dropper = GetComponentInChildren<BombDropper>();
        rb2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (playerControlsEnabled.Toggle) {
            if (hotkeyMap.GetKeyDown(GameAction.DropBomb))
            {
                dropper.Drop((Vector2)dropper.transform.position, rb2D.velocity);
            }
        }
    }
}
