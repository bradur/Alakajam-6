// Date   : 01.06.2019 07:40
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;

public class ProjectileShooter : MonoBehaviour
{

    [SerializeField]
    private ProjectileConfig config;

    [SerializeField]
    private Triplane triplane;
    [SerializeField]
    private GameObject bulletOrigin;
    
    [SerializeField]
    float fireRate = 10;

    [SerializeField]
    float dispersion = 0.05f;

    float lastShot = 0;

    public Projectile Shoot(Vector2 direction)
    {

        if (lastShot + 1.0 / fireRate < Time.time)
        {
            lastShot = Time.time;
            var offset = Vector2.Perpendicular(direction) * Random.Range(-dispersion, dispersion);

            Projectile newProjectile = config.Prefab.GetPooledInstance<Projectile>();
            newProjectile.transform.position = bulletOrigin.transform.position;
            newProjectile.Shoot(config.LifeTime, direction + offset, config.Speed);
            triplane.TriggerMuzzleFlash();
            return newProjectile;
        }
        return null;
    }
}
