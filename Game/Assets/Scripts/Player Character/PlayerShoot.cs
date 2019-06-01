// Date   : 01.06.2019 08:27
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    
    private ProjectileShooter shooter;

    [SerializeField]
    private HotkeyMap hotkeyMap;

    [SerializeField]
    float fireRate = 10;

    [SerializeField]
    float dispersion = 0.05f;
    
    float lastShot = 0;

    void Start()
    {
        shooter = GetComponentInChildren<ProjectileShooter>();
    }


    void Update()
    {
        if (hotkeyMap.GetKey(GameAction.Shoot))
        {
            shooter.Shoot((Vector2)transform.right);
            /*
            if (lastShot + 1.0/fireRate < Time.time)
            {
                float offset = Random.Range(-dispersion, dispersion);
                shooter.Shoot((Vector2)transform.right + offset * (Vector2)transform.up);
                lastShot = Time.time;
            }
            */
        }
    }
}
