using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(VariableResetterSystem))]
    public class VariableResetterEditor : CollectionEditor
    {
        private VariableResetterSystem TypedTarget => (VariableResetterSystem)target;
        private GUIContent _resetButtonText = new GUIContent("Apply Default Values");
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUI.BeginDisabledGroup(disabled: TypedTarget == null || !(TypedTarget.Count > 0));
            if (GUILayout.Button(_resetButtonText))
            {
                TypedTarget.ResetVariables();
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}