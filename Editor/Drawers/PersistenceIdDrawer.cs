using UnityEngine;
using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomPropertyDrawer(typeof(PersistenceId))]
    public class PersistenceIdDrawer : PropertyDrawer
    {
        private const float ENABLE_DISABLE_BUTTON_WIDTH = 50f;
        private readonly GUIContent enableButton_Content = new GUIContent("Enable");
        private readonly GUIContent newIdButton_Content = new GUIContent("New Id");
        private readonly GUIContent disableButton_Content = new GUIContent("Disable");
        private readonly GUIContent uniqueIdLabel_Content = new GUIContent("Unique Id");
        private const string PERSISTENCE_ENABLED_PROPNAME = "_persistenceEnabled";
        private SerializedProperty _persistenceEnabledProp;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SOArch_BaseScriptableObject obj = (SOArch_BaseScriptableObject)property.serializedObject.targetObject;
            if (!obj.IsPersistable) return;

            _persistenceEnabledProp = property.FindPropertyRelative(PERSISTENCE_ENABLED_PROPNAME);
            bool persistenceEnabledPropValue = _persistenceEnabledProp.boolValue;

            Rect foldOutPos = new Rect(position)
            {
                width = EditorGUIUtility.labelWidth,
                height = SOArchitecture_EditorUtility.STD_LINE_HEIGHT
            };
            Rect disableButtonPos = new Rect(position)
            {
                width = ENABLE_DISABLE_BUTTON_WIDTH,
                height = SOArchitecture_EditorUtility.STD_LINE_HEIGHT,
                x = position.width - ENABLE_DISABLE_BUTTON_WIDTH + 12
            };
            Rect newIdButtonPos = new Rect(disableButtonPos)
            {
                x = disableButtonPos.x - ENABLE_DISABLE_BUTTON_WIDTH
            };
            Rect enableButtonPos = new Rect(newIdButtonPos)
            {
                x = newIdButtonPos.x - ENABLE_DISABLE_BUTTON_WIDTH
            };

            if (obj.PersistenceId == null || property == null)
            {
                property.isExpanded = false;
                EditorGUI.LabelField(foldOutPos, label);
            }
            else
            {
                DrawFoldout(foldOutPos, property, label);
            }

            EditorGUI.BeginDisabledGroup(persistenceEnabledPropValue);
            if (GUI.Button(enableButtonPos, enableButton_Content, EditorStyles.miniButtonLeft))
            {
                obj.PersistenceId.EnablePersistence();
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(persistenceEnabledPropValue); // additional condition needed here?
            if (GUI.Button(newIdButtonPos, newIdButton_Content, EditorStyles.miniButtonMid))
            {
                obj.PersistenceId.GenerateNewId();
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!persistenceEnabledPropValue);
            if (GUI.Button(disableButtonPos, disableButton_Content, EditorStyles.miniButtonRight))
            {
                obj.PersistenceId.DisablePersistence();
                //property.isExpanded = false;
            }
            EditorGUI.EndDisabledGroup();

            if (property != null && property.isExpanded)
            {
                SerializedProperty _stringValueProp = property.FindPropertyRelative("_stringValue");
                Rect uniqueIdPos = new Rect(position)
                {
                    height = SOArchitecture_EditorUtility.STD_LINE_HEIGHT,
                    y = position.y + SOArchitecture_EditorUtility.STD_LINE_HEIGHT + 
                        SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT
                };
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.PropertyField(uniqueIdPos, _stringValueProp, uniqueIdLabel_Content, true);
                EditorGUI.EndDisabledGroup();
            }
        }
        private void DrawFoldout(Rect position, SerializedProperty property, GUIContent guiContent)
        {
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, guiContent, toggleOnLabelClick: true);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SOArch_BaseScriptableObject obj = (SOArch_BaseScriptableObject)property.serializedObject.targetObject;
            if (!obj.IsPersistable) return 0f;
            if (property.isExpanded)
            {
                return SOArchitecture_EditorUtility.STD_LINE_HEIGHT * 2 + SOArchitecture_EditorUtility.STD_LINE_SPACER_HEIGHT;
            }
            return base.GetPropertyHeight(property, label);
        }
    }
}