using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorAssistantTestMonoBehaviour : MonoBehaviour
{
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

    //[Header("EditorAssistant - Inspector Only")]
    //[EditorAssistant]
    //public ExampleScriptableObjectBase inspectorOnly = default;

    //[Header("EditorAssistant - Warning Only")]
    //[EditorAssistant(typeof(ExampleScriptableObjectBase), missingObjectWarning: true, displayInspector: false)]
    //public ExampleScriptableObjectBase warningOnly = default;

    //[Header("EditorAssistant - Button Only")]
    //[EditorAssistant(typeof(ExampleScriptableObjectBase), missingObjectWarning: false, showCreateAssetButton: true, displayInspector: false)]
    //public ExampleScriptableObjectBase buttonOnly = default;

    //[Header("EditorAssistant - Warning with Button")]
    //[EditorAssistant(typeof(ExampleScriptableObjectBase), missingObjectWarning: true, showCreateAssetButton: true, displayInspector: false)]
    //public ExampleScriptableObjectBase warningWithButton = default;

    //[Header("EditorAssistant - Inspector and Warning with Button")]
    //[EditorAssistant(typeof(ExampleScriptableObjectBase), missingObjectWarning: true, showCreateAssetButton: true)]
    //public ExampleScriptableObjectBase inspectorWithWarningAndButton = default;
}
