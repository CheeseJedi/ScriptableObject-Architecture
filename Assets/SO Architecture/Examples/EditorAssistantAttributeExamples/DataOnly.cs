using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu]
public class DataOnly : ExampleScriptableObjectBase
{
    public static DataOnly CreateAsset() =>
        EditorAssistantUtility.CreateAsset<DataOnly>();

    [Header("Value Types")]
    public string stringData = "some string data.";
    public float floatData = 0.95f;
    public int intData = 42;
    public bool boolData;

}
