using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(CallbackDistributorSystem), false)]
    public class CallbackDistributorEditor : UnityEditor.Editor
    {
        private CallbackDistributorSystem Target => (CallbackDistributorSystem)target;
        private SerializedProperty DeveloperDescriptionProperty =>
            serializedObject.FindProperty(DESCRIPTION_PROPERTY_NAME);
        private SerializedProperty HostedSystemsProperty =>
            serializedObject.FindProperty(HOSTEDSYSTEMS_PROPERTY_NAME);
        private SerializedProperty BackBurnerProperty =>
            serializedObject.FindProperty(BACKBURNER_PROPERTY_NAME);
        // Property Names
        private const string DESCRIPTION_PROPERTY_NAME = "DeveloperDescription";
        private const string HOSTEDSYSTEMS_PROPERTY_NAME = "HostedSystems";
        private const string BACKBURNER_PROPERTY_NAME = "BackBurner";
        // Lists
        private ReorderableList _hostedSystemsList;
        private ReorderableList _backBurnerList;
        // UI
        private const bool DISABLE_ELEMENTS = false;
        private const bool ELEMENT_DRAGGABLE = true;
        private const bool LIST_DISPLAY_HEADER = true;
        private const bool LIST_DISPLAY_ADD_BUTTON = true;
        private const bool LIST_DISPLAY_REMOVE_BUTTON = true;
        // Headers
        private const string HOSTED_SYSTEMS_HEADER = "Hosted ScriptableObjectSystems";
        private const string BACK_BURNER_HEADER = "Back Burner";
        // Titles
        private const string HOSTED_SYSTEMS_TITLE = "List (ScriptableObjectSystem)";
        private const string BACK_BURNER_TITLE = "List (ScriptableObject)";
        // GUI Content
        private GUIContent _hostedSystemsGUIContent;
        private GUIContent _backBurnerGUIContent;
        private GUIContent _noPropertyDrawerWarningGUIContent;
        // General
        private const string NO_PROPERTY_WARNING_FORMAT = "No PropertyDrawer for type [{0}]";
        private void OnEnable()
        {
            _hostedSystemsGUIContent = new GUIContent(HOSTED_SYSTEMS_TITLE);
            _backBurnerGUIContent = new GUIContent(BACK_BURNER_TITLE);
            _noPropertyDrawerWarningGUIContent = new GUIContent
                (string.Format(NO_PROPERTY_WARNING_FORMAT, Target.GetType()));

            _hostedSystemsList = new ReorderableList(serializedObject, HostedSystemsProperty,
                ELEMENT_DRAGGABLE, LIST_DISPLAY_HEADER, 
                LIST_DISPLAY_ADD_BUTTON, LIST_DISPLAY_REMOVE_BUTTON)
            {
                drawHeaderCallback = DrawHostedSystemsHeader,
                drawElementCallback = DrawHostedSystemsElement,
            };

            _backBurnerList = new ReorderableList(serializedObject, BackBurnerProperty,
                ELEMENT_DRAGGABLE, LIST_DISPLAY_HEADER, 
                LIST_DISPLAY_ADD_BUTTON, LIST_DISPLAY_REMOVE_BUTTON)
            {
                drawHeaderCallback = DrawBackBurnerHeader,
                drawElementCallback = DrawBackBurnerElement,
            };
        }
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(DeveloperDescriptionProperty);
            GUILayout.Space(16);

            EditorGUILayout.LabelField(HOSTED_SYSTEMS_HEADER, EditorStyles.boldLabel);
            _hostedSystemsList.DoLayoutList();
            GUILayout.Space(5);

            EditorGUILayout.LabelField(BACK_BURNER_HEADER, EditorStyles.boldLabel);
            _backBurnerList.DoLayoutList();

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
        private void DrawHostedSystemsHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, _hostedSystemsGUIContent);
        }
        private void DrawBackBurnerHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, _backBurnerGUIContent);
        }
        private void DrawHostedSystemsElement(Rect rect, int index, bool isActive, bool isFocused)
            => DrawElement(rect, index, isActive, isFocused, typeof(ScriptableObjectSystem), HostedSystemsProperty);
        private void DrawBackBurnerElement(Rect rect, int index, bool isActive, bool isFocused)
            => DrawElement(rect, index, isActive, isFocused, typeof(ScriptableObject), BackBurnerProperty);
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
