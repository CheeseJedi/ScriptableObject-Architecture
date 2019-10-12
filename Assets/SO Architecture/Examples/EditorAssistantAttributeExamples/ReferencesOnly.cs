using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu]
public class ReferencesOnly : ExampleScriptableObjectBase
{
    public static ReferencesOnly CreateAsset() =>
        EditorAssistantUtility.CreateAsset<ReferencesOnly>();

    [Header("Reference Types")]
    [EditorAssistant(typeof(GameObject), missingObjectWarning: true, showCreateAssetButton: false, displayInspector: true)]
    public GameObject gameObjectData = default;
    [EditorAssistant(typeof(ExampleScriptableObjectBase), missingObjectWarning: true, showCreateAssetButton: true, displayInspector: true)]
    public ExampleScriptableObjectBase scriptableObjectData = default;
}
