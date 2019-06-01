// Date   : 01.06.2019 08:27
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField]
    private ProjectileShooter shooter;

    [SerializeField]
    private HotkeyMap hotkeyMap;

    void Start()
    {

    }


    void Update()
    {
        if (hotkeyMap.GetKeyDown(GameAction.Shoot))
        {
            shooter.Shoot((Vector2)transform.position, (Vector2)transform.right);
        }
    }
}
