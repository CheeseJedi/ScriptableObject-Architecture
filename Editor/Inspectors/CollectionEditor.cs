using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(BaseCollection), true)]
    public class CollectionEditor : UnityEditor.Editor
    {
        protected BaseCollection Target { get { return (BaseCollection)target; } }
        protected SerializedProperty DeveloperDescriptionProperty
        {
            get { return serializedObject.FindProperty(DESCRIPTION_PROPERTY_NAME); }
        }
        protected SerializedProperty CollectionItemsProperty
        {
            get { return serializedObject.FindProperty(LIST_PROPERTY_NAME);}
        }
        protected SerializedProperty SelectedItemIndexProperty
        {
            get { return serializedObject.FindProperty(SELECTED_ITEM_INDEX_PROPERTY_NAME); }
        }
        protected ReorderableList _reorderableList;

        // UI
        protected const bool DISABLE_ELEMENTS = false;
        protected const bool ELEMENT_DRAGGABLE = true;
        protected const bool LIST_DISPLAY_HEADER = true;
        protected const bool LIST_DISPLAY_ADD_BUTTON = true;
        protected const bool LIST_DISPLAY_REMOVE_BUTTON = true;

        protected GUIContent _titleGUIContent;
        protected GUIContent _noPropertyDrawerWarningGUIContent;

        protected const string TITLE_FORMAT = "List ({0})";
        protected const string NO_PROPERTY_WARNING_FORMAT = "No PropertyDrawer for type [{0}]";

        // Property Names
        protected const string LIST_PROPERTY_NAME = "_list";
        protected const string SELECTED_ITEM_INDEX_PROPERTY_NAME = "_selectedItemIndex";
        protected const string DESCRIPTION_PROPERTY_NAME = "DeveloperDescription";
        protected const string COLLECTIONSORTED_LABEL = "INFO: This collection is automatically re-sorted";
        protected const float STD_LINE_HEIGHT = 16;
        protected const float STD_LINE_SPACER_HEIGHT = 2;

        protected virtual void OnEnable()
        {
            _titleGUIContent = new GUIContent(string.Format(TITLE_FORMAT, Target.Type));
            _noPropertyDrawerWarningGUIContent = new GUIContent(string.Format(NO_PROPERTY_WARNING_FORMAT, Target.Type));

            _reorderableList = new ReorderableList(
                serializedObject,
                CollectionItemsProperty,
                ELEMENT_DRAGGABLE,
                LIST_DISPLAY_HEADER,
                LIST_DISPLAY_ADD_BUTTON,
                LIST_DISPLAY_REMOVE_BUTTON)
            {
                drawHeaderCallback = DrawHeader,
                elementHeightCallback = GetElementHeight,
                drawElementCallback = DrawElement,
            };
        }
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(DeveloperDescriptionProperty);
            GUILayout.Space(STD_LINE_HEIGHT);
            if (Target.IsAutoSorted)
            {
                EditorGUILayout.HelpBox(COLLECTIONSORTED_LABEL, MessageType.None);
            }
            if (Target.IsSelectedItemTracked)
            {
                EditorGUILayout.PropertyField(SelectedItemIndexProperty);
            }
            _reorderableList.DoLayoutList();
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                Target.Raise();
            }
        }
        protected virtual void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, _titleGUIContent);
        }
        protected virtual float GetElementHeight(int index)
        {
            SerializedProperty property = CollectionItemsProperty.GetArrayElementAtIndex(index);
            if (property == null) return STD_LINE_HEIGHT + STD_LINE_SPACER_HEIGHT;
            return EditorGUI.GetPropertyHeight(property) + STD_LINE_SPACER_HEIGHT;
        }
        protected virtual void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect = SOArchitecture_EditorUtility.GetReorderableListElementFieldRect(rect);
            SerializedProperty property = CollectionItemsProperty.GetArrayElementAtIndex(index);

            EditorGUI.BeginDisabledGroup(DISABLE_ELEMENTS);

            GenericPropertyDrawer.DrawPropertyDrawer(rect, new GUIContent("Element " + index), 
                Target.Type, property, _noPropertyDrawerWarningGUIContent);

            EditorGUI.EndDisabledGroup();
        }
    }
}