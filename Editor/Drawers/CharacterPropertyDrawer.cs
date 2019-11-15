using UnityEngine;
using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomPropertyDrawer(typeof(Character))]
    public class CharacterPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = SOArchitecture_EditorUtility.STD_LINE_HEIGHT;
            Rect foldOutPos = new Rect(position)
            {
                width = EditorGUIUtility.labelWidth //= 5; 
            };
            if (property.objectReferenceValue != null)
            {
                DrawFoldout(foldOutPos, property);
            }
            else
            {
                property.isExpanded = false;
            }
            DrawPropertyField(position, property, label);
            position.y += SOArchitecture_EditorUtility.STD_LINE_HEIGHT 
                + SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT;
            if (property.objectReferenceValue != null && property.isExpanded)
            {
                DrawSubInspector(position, property);
            }
            if (GUI.changed)
            {
                property.serializedObject.ApplyModifiedProperties();
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float basePropertyHeight = base.GetPropertyHeight(property, label);
            if (property.objectReferenceValue == null)
            {
                return basePropertyHeight;
            }
            if (property.isExpanded)
            {
                float height = 0;
                var propertyObject = new SerializedObject(property.objectReferenceValue).GetIterator();
                propertyObject.Next(true);
                while (propertyObject.NextVisible(false))
                {
                    height += EditorGUI.GetPropertyHeight(propertyObject) 
                        + SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT;
                }
                return height;
            }
            return basePropertyHeight;
        }
        private void DrawFoldout(Rect position, SerializedProperty property)
        {
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none, toggleOnLabelClick: true);
        }
        private void DrawPropertyField(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label);
        }
        private void DrawSubInspector(Rect position, SerializedProperty property)
        {
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


        ////public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        ////{
        ////    position.height = GetPropertyHeight(property, label);

        ////    EditorGUI.PropertyField(position, property);

        ////    if (property.objectReferenceValue == null)
        ////        return;

        ////    var modifier = property.objectReferenceValue as Character;
        ////    var fields = modifier.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
        ////    var sObj = new SerializedObject(property.objectReferenceValue);

        ////    position.x += 20f;
        ////    position.xMax -= 38f;
        ////    foreach (var field in fields)
        ////    {
        ////        position.y += position.height;
        ////        EditorGUI.PropertyField(position, sObj.FindProperty(field.Name));
        ////    }

        ////    sObj.ApplyModifiedProperties();

        ////}
        ////public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        ////{
        ////    if (property.objectReferenceValue == null)
        ////        return base.GetPropertyHeight(property, label);

        ////    var modifier = property.objectReferenceValue as Character;
        ////    var fields = modifier.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        ////    return base.GetPropertyHeight(property, label) * (float)(fields.Length + 1);
        ////}
    }
}
