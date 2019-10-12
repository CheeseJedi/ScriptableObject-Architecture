using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataOnlyDerived : DataOnly
{
    public new static DataOnlyDerived CreateAsset() =>
        EditorAssistantUtility.CreateAsset<DataOnlyDerived>();

    [Header("Derived Class Fields")]
    public bool someBool;
    public int someInt = 99;
    public float someFloat = 12.34f;
}
