using UnityEngine;
using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(AbstractSerializer), true)]
    public class AbstractSerializerEditor : UnityEditor.Editor
    {
        private readonly string[] _excludedPropertyNames =
            { "m_Script", DESCRIPTION_PROPERTY_NAME };
        protected const string DESCRIPTION_PROPERTY_NAME = "DeveloperDescription";
        protected AbstractSerializer Target => (AbstractSerializer)target;
        protected SerializedProperty DeveloperDescriptionProperty
        {
            get { return serializedObject.FindProperty(DESCRIPTION_PROPERTY_NAME); }
        }
        protected const float STD_LINE_HEIGHT = 16;
        protected const float STD_LINE_SPACER_HEIGHT = 2;
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(DeveloperDescriptionProperty);
            GUILayout.Space(STD_LINE_HEIGHT);
            EditorGUILayout.HelpBox(Target.GetFeatureListText(), MessageType.None);
            DrawPropertiesExcluding(serializedObject, _excludedPropertyNames);
            GUILayout.Space(10);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
