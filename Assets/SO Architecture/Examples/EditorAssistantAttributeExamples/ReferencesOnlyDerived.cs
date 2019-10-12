using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu]
public class ReferencesOnlyDerived : ReferencesOnly
{
    public new static ReferencesOnlyDerived CreateAsset() =>
        EditorAssistantUtility.CreateAsset<ReferencesOnlyDerived>();

    [Header("Derived Class Fields")]
    [EditorAssistant(typeof(DataOnly), missingObjectWarning: true, showCreateAssetButton: true, displayInspector: true)]
    [Tooltip("Should create DataOnly assets, accept assets of derived types, and not assets of other types.")]
    public DataOnly dataOnly = default;

    [Tooltip("Should create DataOnlyDerived assets, accept assets of derived types, and not assets of other types.")]
    [EditorAssistant(typeof(DataOnlyDerived), missingObjectWarning: true, showCreateAssetButton: true, displayInspector: true)]
    public DataOnlyDerived dataOnlyDerived = default;

    [Tooltip("Should create ReferencesOnly assets, accept assets of derived types, and not assets of other types.")]
    [EditorAssistant(typeof(ReferencesOnly), missingObjectWarning: true, showCreateAssetButton: true, displayInspector: true)]
    public ReferencesOnly referencesOnly = default;

    [Tooltip("Should create ReferencesOnlyDerived assets, accept assets of derived types, and not assets of other types.")]
    [EditorAssistant(typeof(ReferencesOnlyDerived), missingObjectWarning: true, showCreateAssetButton: true, displayInspector: true)]
    public ReferencesOnlyDerived referencesOnlyDerived = default;

}
