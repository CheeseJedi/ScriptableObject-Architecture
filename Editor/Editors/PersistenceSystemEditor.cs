using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(PersistenceSystem))]
    public class PersistenceSystemEditor : CollectionEditor
    {
        private PersistenceSystem TypedTarget => (PersistenceSystem)target;
        private GUIContent _saveButtonContent = new GUIContent("Save");
        private GUIContent _loadButtonContent = new GUIContent("Load");
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            EditorGUILayout.HelpBox(TypedTarget.GetCachedPath(createMissingDirectories: false), MessageType.None);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUI.BeginDisabledGroup(TypedTarget.SerializerAsset == null);
            if (GUILayout.Button(_saveButtonContent, EditorStyles.miniButtonLeft)) TypedTarget.Save();
            if (GUILayout.Button(_loadButtonContent, EditorStyles.miniButtonRight)) TypedTarget.Load();
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
            if (!string.IsNullOrEmpty(TypedTarget.LastMessage))
            {
                GUILayout.Space(5);
                EditorGUILayout.HelpBox(TypedTarget.LastMessage, MessageType.Info);
            }
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                TypedTarget.NotifyOfEditorChange(); // Triggers file name regeneration.
                Target.Raise();
            }
        }
    }
}
