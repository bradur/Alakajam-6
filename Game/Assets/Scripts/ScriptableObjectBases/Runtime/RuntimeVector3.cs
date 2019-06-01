using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuntimeVector3 ", menuName = "ScriptableObjects/New RuntimeVector3")]
public class RuntimeVector3 : ScriptableObject
{
    [SerializeField]
    private Vector3 _value;
    public Vector3 Value { get { return _value; } set { _value = value; } }
}
