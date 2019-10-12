using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(VariableResetterSystem))]
    public class VariableResetterEditor : UnityEditor.Editor
    {
        private VariableResetterSystem Target => (VariableResetterSystem)target;
        private SerializedProperty DeveloperDescriptionProperty =>
            serializedObject.FindProperty(DESCRIPTION_PROPERTY_NAME);
        private SerializedProperty CallbackOnProperty =>
            serializedObject.FindProperty(CALLBACK_ON_PROPERTY_NAME);
        private SerializedProperty VariablesToResetProperty =>
            serializedObject.FindProperty(VARIABLESTORESET_PROPERTY_NAME);
        private const string DESCRIPTION_PROPERTY_NAME = "DeveloperDescription";
        private const string CALLBACK_ON_PROPERTY_NAME = "_CallbackOn";
        private const string VARIABLESTORESET_PROPERTY_NAME = "VariablesToReset";
        private ReorderableList _variablesToResetList;
        private const bool DISABLE_ELEMENTS = false;
        private const bool ELEMENT_DRAGGABLE = true;
        private const bool LIST_DISPLAY_HEADER = true;
        private const bool LIST_DISPLAY_ADD_BUTTON = true;
        private const bool LIST_DISPLAY_REMOVE_BUTTON = true;
        private const string VARIABLES_TO_RESET_HEADER = "Reset Variables";
        private const string VARIABLES_TO_RESET_TITLE = "List (BaseVariable)";
        private GUIContent _variablesToResetGUIContent;
        private GUIContent _noPropertyDrawerWarningGUIContent;
        private const string NO_PROPERTY_WARNING_FORMAT = "No PropertyDrawer for type [{0}]";
        private void OnEnable()
        {
            _variablesToResetGUIContent = new GUIContent(VARIABLES_TO_RESET_TITLE);
            _noPropertyDrawerWarningGUIContent = new GUIContent
                (string.Format(NO_PROPERTY_WARNING_FORMAT, Target.GetType()));

            _variablesToResetList = new ReorderableList(serializedObject, VariablesToResetProperty,
                ELEMENT_DRAGGABLE, LIST_DISPLAY_HEADER, 
                LIST_DISPLAY_ADD_BUTTON, LIST_DISPLAY_REMOVE_BUTTON)
            {
                drawHeaderCallback = DrawVariablesToResetHeader,
                drawElementCallback = DrawVariablesToResetElement,
            };
        }
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(DeveloperDescriptionProperty);
            GUILayout.Space(16);
            EditorGUILayout.PropertyField(CallbackOnProperty);
            EditorGUILayout.LabelField(VARIABLES_TO_RESET_HEADER, EditorStyles.boldLabel);
            _variablesToResetList.DoLayoutList();
            if (GUILayout.Button("Apply Default Values"))
            {
                Target.ResetVariables();
            }
            GUILayout.Space(5);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
        private void DrawVariablesToResetHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, _variablesToResetGUIContent);
        }
        private void DrawVariablesToResetElement(Rect rect, int index, bool isActive, bool isFocused)
            => DrawElement(rect, index, isActive, isFocused, typeof(BaseVariable), VariablesToResetProperty);
        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused, 
            System.Type type, SerializedProperty targetListProperty)
        {
            rect = SOArchitecture_EditorUtility.GetReorderableListElementFieldRect(rect);
            SerializedProperty property = targetListProperty.GetArrayElementAtIndex(index);

            EditorGUI.BeginDisabledGroup(DISABLE_ELEMENTS);

            GenericPropertyDrawer.DrawPropertyDrawer(rect, new GUIContent("Element " + index), 
                type, property, _noPropertyDrawerWarningGUIContent);

            EditorGUI.EndDisabledGroup();
        }
    }
}