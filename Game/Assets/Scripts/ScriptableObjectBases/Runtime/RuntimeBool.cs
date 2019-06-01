// Date   : 01.06.2019 23:40
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RuntimeBool ", menuName = "ScriptableObjects/New RuntimeBool")]
public class RuntimeBool : ScriptableObject
{

    [SerializeField]
    private string configName;
    public string Name { get { return configName; } }

    public bool Toggle = false;

}