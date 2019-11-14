using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System.Collections.Generic;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(BaseCollection), true)]
    public class CollectionEditor : UnityEditor.Editor
    {
        protected BaseCollection Target => (BaseCollection)target;
        protected SerializedProperty DeveloperDescriptionProperty
            => serializedObject.FindProperty(SOArchitecture_EditorUtility.DESCRIPTION_PROPERTY_NAME);
        protected SerializedProperty PersistenceIdProperty
            => serializedObject.FindProperty(SOArchitecture_EditorUtility.PERSISTENCE_ID_PROPERTY_NAME);
        protected SerializedProperty CollectionItemsProperty
            => serializedObject.FindProperty(SOArchitecture_EditorUtility.COLLECTION_LIST_PROPERTY_NAME);
        protected SerializedProperty SelectedItemIndexProperty
            => serializedObject.FindProperty(SOArchitecture_EditorUtility.SELECTED_ITEM_INDEX_PROPERTY_NAME);
        protected ReorderableList _reorderableList;

        protected string[] _excludedPropertyNames =
        {
            SOArchitecture_EditorUtility.SCRIPT_PROPERTY_NAME,
            SOArchitecture_EditorUtility.DESCRIPTION_PROPERTY_NAME,
            SOArchitecture_EditorUtility.PERSISTENCE_ID_PROPERTY_NAME,
            SOArchitecture_EditorUtility.COLLECTION_LIST_PROPERTY_NAME,
            SOArchitecture_EditorUtility.SELECTED_ITEM_INDEX_PROPERTY_NAME
        };

        // UI - ReorderableList
        protected const bool DISABLE_ELEMENTS = false;
        protected const bool ELEMENT_DRAGGABLE = true;
        protected const bool LIST_DISPLAY_HEADER = true;
        protected const bool LIST_DISPLAY_ADD_BUTTON = true;
        protected const bool LIST_DISPLAY_REMOVE_BUTTON = true;

        protected GUIContent _titleGUIContent;
        protected GUIContent _noPropertyDrawerWarningGUIContent;
        protected GUIContent _noUniqueIdPropertyDrawerWarningGUIContent =
            new GUIContent(string.Format(SOArchitecture_EditorUtility.NO_PROPERTY_WARNING_FORMAT, typeof(PersistenceId).Name));
        protected GUIContent _UniqueIdContent = new GUIContent("Persistence Id");

        protected virtual void OnEnable()
        {
            _titleGUIContent = new GUIContent(string.Format(
                SOArchitecture_EditorUtility.COLLECTION_TITLE_FORMAT, Target.Type.Name));
            _noPropertyDrawerWarningGUIContent = new GUIContent(
                string.Format(SOArchitecture_EditorUtility.NO_PROPERTY_WARNING_FORMAT, Target.Type));

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
            GUILayout.Space(SOArchitecture_EditorUtility.STD_LINE_HEIGHT);
            EditorGUILayout.PropertyField(PersistenceIdProperty, _UniqueIdContent);
            if (Target.IsAutoSorted)
            {
                EditorGUILayout.HelpBox(SOArchitecture_EditorUtility.COLLECTION_SORTED_LABEL, MessageType.None);
            }
            if (Target.IsSelectedItemTracked)
            {
                EditorGUILayout.PropertyField(SelectedItemIndexProperty);
            }
            _reorderableList.DoLayoutList();
            DrawPropertiesExcluding(serializedObject, _excludedPropertyNames);
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
            if (property == null) return SOArchitecture_EditorUtility.STD_LINE_HEIGHT +
                SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT;
            return EditorGUI.GetPropertyHeight(property, true) +
                SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT;
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