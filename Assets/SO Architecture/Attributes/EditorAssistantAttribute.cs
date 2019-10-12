using System;
using UnityEngine;

//[System.AttributeUsage(System.AttributeTargets.Field)]
public class EditorAssistantAttribute : PropertyAttribute
{
    public bool DisplayInspector { get; private set; } = true;
    public Type Type { get; private set; } = null;
    public bool MissingObjectWarning { get; private set; } = false;
    public bool ShowCreateAssetButton { get; private set; } = false;
    public bool HasType => Type != null;
    public EditorAssistantAttribute(bool displayInspector = true)
    {
        DisplayInspector = displayInspector;
    }
    public EditorAssistantAttribute(Type type, bool missingObjectWarning = false, bool showCreateAssetButton = false, bool displayInspector = true)
    {
        DisplayInspector = displayInspector;
        if (type != null)
        {
            Type = type;
            MissingObjectWarning = missingObjectWarning;
            ShowCreateAssetButton = showCreateAssetButton;

            //Debug.Log($"EditorAssistantAttribute({type})");
            //if (type == typeof(UnityEngine.Object))
            //{
            //    Debug.Log("target is type Object");
            //}
            //else return;
            //if (Type == typeof(ScriptableObject))
            //{
            //    Debug.Log("target is type ScriptableObject");
            //}
        }
    }
}
