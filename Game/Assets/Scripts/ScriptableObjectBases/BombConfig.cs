// Date   : 01.06.2019 09:46
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BombConfig ", menuName = "ScriptableObjects/New BombConfig")]
public class BombConfig : ScriptableObject
{

    [SerializeField]
    private string configName;
    public string Name { get { return configName; } }

    public float Speed = 5f;
    public float LifeTime = 0.5f;
    public float DropInterval = 0.2f;

    [SerializeField]
    private Bomb prefab;
    public Bomb Prefab { get { return prefab; } }


}