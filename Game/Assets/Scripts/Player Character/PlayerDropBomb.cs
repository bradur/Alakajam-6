// Date   : 01.06.2019 09:34
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerDropBomb : MonoBehaviour {

    [SerializeField]
    private BombDropper dropper;

    [SerializeField]
    private HotkeyMap hotkeyMap;

    [SerializeField]
    private Rigidbody2D rb2D;

    void Start()
    {

    }


    void Update()
    {
        if (hotkeyMap.GetKeyDown(GameAction.DropBomb))
        {
            dropper.Drop((Vector2)dropper.transform.position, rb2D.velocity);
        }
    }
}
