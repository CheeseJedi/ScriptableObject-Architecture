using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(CharacterCollection))]
    public class CharacterCollectionEditor : CollectionEditor
    {
        private GUIContent _createBtnContent = new GUIContent("Create Character");
        private GUIContent _removeBtnContent = new GUIContent("Remove Character");
        protected override void OnEnable()
        {
            base.OnEnable();
            _reorderableList.displayAdd = false;
            _reorderableList.displayRemove = false;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(_createBtnContent, EditorStyles.miniButtonLeft))
            {
                ((CharacterCollection)target).CreateCharacter();
            }
            EditorGUI.BeginDisabledGroup(Target.Count < 1 || _reorderableList.index == -1);
            if(GUILayout.Button(_removeBtnContent, EditorStyles.miniButtonRight))
            {
                ((CharacterCollection)target).RemoveCharacter(_reorderableList.index);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
        }
        protected override void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect = SOArchitecture_EditorUtility.GetReorderableListElementFieldRect(rect);
            SerializedProperty property = CollectionItemsProperty.GetArrayElementAtIndex(index);

            EditorGUI.BeginDisabledGroup(DISABLE_ELEMENTS);

            Rect objectFieldRect = new Rect(rect) { height = SOArchitecture_EditorUtility.STD_LINE_HEIGHT };
            GenericPropertyDrawer.DrawPropertyDrawer(objectFieldRect, new GUIContent
                ("Character " + index), Target.Type, property, _noPropertyDrawerWarningGUIContent);

            Rect subInspectorRect = new Rect(rect)
            {
                height = rect.height - SOArchitecture_EditorUtility.STD_LINE_HEIGHT
                    - SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT,
                y = rect.y + SOArchitecture_EditorUtility.STD_LINE_HEIGHT
                    + SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT
            };
            DrawSubInspector(subInspectorRect, property);

            EditorGUI.EndDisabledGroup();
        }
        protected override float GetElementHeight(int index)
        {
            SerializedProperty property = CollectionItemsProperty.GetArrayElementAtIndex(index);
            if (property == null) return SOArchitecture_EditorUtility.STD_LINE_HEIGHT
                    + SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT;
            return GetSubInspectorHeight(property) + SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT;
        }
        private void DrawSubInspector(Rect position, SerializedProperty property)
        {
            if (property?.objectReferenceValue == null)
            {
                const string NULL_OBJECT_MESSAGE = "Null Object";
                EditorGUI.LabelField(position, new GUIContent(NULL_OBJECT_MESSAGE));
                return;
            }
            using (SerializedProperty propertyObject =
                new SerializedObject(property.objectReferenceValue).GetIterator())
            {
                propertyObject.Next(true);
                propertyObject.NextVisible(false);
                EditorGUI.indentLevel++;
                while (propertyObject.NextVisible(false))
                {
                    position.height = EditorGUI.GetPropertyHeight(propertyObject);
                    EditorGUI.PropertyField(position, propertyObject);
                    position.y += EditorGUI.GetPropertyHeight(propertyObject)
                        + SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT;
                }
                EditorGUI.indentLevel--;
                if (GUI.changed)
                {
                    propertyObject.serializedObject.ApplyModifiedProperties();
                }
            }
        }
        private float GetSubInspectorHeight(SerializedProperty property)
        {
            float height = 0;
            if (property?.objectReferenceValue == null)
            {
                return SOArchitecture_EditorUtility.STD_LINE_HEIGHT
                    + SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT;
            }
            using (var propertyObject = new SerializedObject(property.objectReferenceValue).GetIterator())
            {
                propertyObject.Next(true);
                while (propertyObject.NextVisible(false))
                {
                    height += EditorGUI.GetPropertyHeight(propertyObject)
                        + SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT;
                }
            }
            return height;
        }
    }
}
