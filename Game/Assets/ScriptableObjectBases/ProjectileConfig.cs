// Date   : 01.06.2019 08:17
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ProjectileConfig ", menuName = "ScriptableObjects/New ProjectileConfig")]
public class ProjectileConfig : ScriptableObject
{

    [SerializeField]
    private string configName;
    public string Name { get { return configName; } }

    public float Speed = 5f;
    public float LifeTime = 0.5f;

    [SerializeField]
    private Projectile prefab;
    public Projectile Prefab { get { return prefab; } }

}