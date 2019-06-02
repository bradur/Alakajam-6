using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigInt ", menuName = "ScriptableObjects/New ConfigInt")]
public class ConfigInt : ScriptableObject
{
    [SerializeField]
    private int value;
    public int Value { get { return value; } }
}