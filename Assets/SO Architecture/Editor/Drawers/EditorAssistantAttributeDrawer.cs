using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace ScriptableObjectArchitecture
{
    [CustomPropertyDrawer(typeof(EditorAssistantAttribute))]
    public class EditorAssistantAttributeDrawer : PropertyDrawer
    {
        // Based on multiple solutions available online, however the following formed the base of this drawer:
        // User: Deadcow_ at: https://forum.unity.com/threads/object-property-editor-issues.388877/
        private const string NO_ASSET_MESSAGE = "Please create a new asset or link an existing asset of the type: {0}";
        private const string CREATE_BUTTON_LABEL = "Create Asset";
        private const float STD_LINE_HEIGHT = 16;
        private const float STD_LINE_SPACER_HEIGHT = 2;
        private const float HELP_BOX_HEIGHT = 40;
        private const float CREATE_BUTTON_HEIGHT = 16;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorAssistantAttribute attrib = attribute as EditorAssistantAttribute;
            if (attrib == null)
            {
                Debug.LogError("attrib was null.");
            }
            bool createAssetButtonClicked = false;
            position.height = STD_LINE_HEIGHT;
            Rect foldOutPos = new Rect(position);
            foldOutPos.width = EditorGUIUtility.labelWidth; //= 5; 
            if (property.objectReferenceValue != null && attrib.DisplayInspector)
            {
                DrawFoldout(foldOutPos, property);
            }
            else
            {
                property.isExpanded = false;
            }
            DrawPropertyField(position, property, label);
            position.y += STD_LINE_HEIGHT + STD_LINE_SPACER_HEIGHT;
            if (property.objectReferenceValue == null)
            {
                Rect helpBoxRect = new Rect(position);
                if (attrib.MissingObjectWarning)
                {
                    //helpBoxRect.y += STD_LINE_HEIGHT + STD_LINE_SPACER_HEIGHT;
                    helpBoxRect.height = HELP_BOX_HEIGHT;
                    DrawHelpBox(helpBoxRect, attrib);
                }
                if (attrib.ShowCreateAssetButton)
                {
                    Rect buttonRect = new Rect(helpBoxRect);
                    if (attrib.MissingObjectWarning)
                    {
                        buttonRect.y += HELP_BOX_HEIGHT + STD_LINE_SPACER_HEIGHT;
                    }
                    buttonRect.height = CREATE_BUTTON_HEIGHT;
                    createAssetButtonClicked = DrawCreateAssetButton(buttonRect, attrib);
                }
            }
            if (property.objectReferenceValue != null && property.isExpanded)
            {
                DrawSubInspector(position, property);
            }
            if (createAssetButtonClicked)
            {
                CreateNewAsset(property, attrib);
            }
            if (GUI.changed)
            {
                property.serializedObject.ApplyModifiedProperties();
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            EditorAssistantAttribute attrib = attribute as EditorAssistantAttribute;

            float basePropertyHeight = base.GetPropertyHeight(property, label);
            if (property.objectReferenceValue == null)
            {
                float result = basePropertyHeight;
                if (attrib.MissingObjectWarning)
                {
                    result += HELP_BOX_HEIGHT + STD_LINE_SPACER_HEIGHT;
                }
                if (attrib.ShowCreateAssetButton)
                {
                    result += CREATE_BUTTON_HEIGHT + STD_LINE_SPACER_HEIGHT;
                }
                if (attrib.MissingObjectWarning || attrib.ShowCreateAssetButton)
                {
                    result += STD_LINE_SPACER_HEIGHT;
                }
                return result;
            }
            if (property.isExpanded)
            {
                float height = 0; // -STD_LINE_SPACER_HEIGHT;
                var propertyObject = new SerializedObject(property.objectReferenceValue).GetIterator();
                propertyObject.Next(true);
                while (propertyObject.NextVisible(false))
                {
                    height += EditorGUI.GetPropertyHeight(propertyObject) + STD_LINE_SPACER_HEIGHT;
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
            EditorGUI.PropertyField(position, property);
        }
        private void DrawHelpBox(Rect position, EditorAssistantAttribute attrib)
        {
            EditorGUI.HelpBox(position, string.Format(NO_ASSET_MESSAGE, attrib.Type), MessageType.Warning);
        }
        private bool DrawCreateAssetButton(Rect position, EditorAssistantAttribute attrib)
        {
            return (GUI.Button(position, string.Format(CREATE_BUTTON_LABEL, attrib.Type), EditorStyles.miniButton));
        }
        private void DrawSubInspector(Rect position, SerializedProperty property) //, EditorAssistantAttribute attrib)
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
                    position.y += EditorGUI.GetPropertyHeight(propertyObject) + STD_LINE_SPACER_HEIGHT;
                }
                EditorGUI.indentLevel--;
                if (GUI.changed)
                {
                    propertyObject.serializedObject.ApplyModifiedProperties();
                }
            }
        }
        private void CreateNewAsset(SerializedProperty property, EditorAssistantAttribute attrib)
        {
            MethodInfo method = attrib.Type.GetMethod("CreateAsset", BindingFlags.Public | BindingFlags.Static);
            if (method == null) Debug.LogWarning("EditorAssistantAttributeDrawer: Creating new asset failed - unable to find a public CreateAsset() method for the specified type.");
            else
            {
                ScriptableObject newObj = (ScriptableObject)method.Invoke(attrib.Type, null);
                if (newObj == null) Debug.LogError("EditorAssistantAttributeDrawer: Creating new asset failed - returned object was null.");
                else
                {
                    //Create the asset.
                    property.objectReferenceValue = newObj; // ScriptableObjectUtility.CreateAsset<>(); //  <attrib.Type.GetType().Name>();
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
        }
    }
}
