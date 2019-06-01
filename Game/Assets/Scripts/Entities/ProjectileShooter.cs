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

    public Projectile Shoot(Vector2 direction)
    {
        Projectile newProjectile = config.Prefab.GetPooledInstance<Projectile>();
        newProjectile.transform.position = bulletOrigin.transform.position;
        newProjectile.Shoot(config.LifeTime, direction, config.Speed);
        triplane.TriggerMuzzleFlash();
        return newProjectile;
    }
}
