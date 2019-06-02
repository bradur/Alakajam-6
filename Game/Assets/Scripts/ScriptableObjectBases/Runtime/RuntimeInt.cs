// Date   : 02.06.2019 15:01
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RuntimeInt ", menuName = "ScriptableObjects/New RuntimeInt")]
public class RuntimeInt : ScriptableObject
{

    [SerializeField]
    private string configName;
    public string Name { get { return configName; } }

    public int Count = 0;
    public int Target = 0;

    public bool Accomplished { get { return Target > 0 && Count >= Target; } }

}