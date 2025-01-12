﻿using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "SByteVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "sbyte",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 15)]
    public class SByteVariable : BaseVariable<sbyte>
    {
        public static SByteVariable CreateAsset() => EditorAssistantUtility.CreateAsset<SByteVariable>();
        public override bool Clampable { get { return true; } }
        protected override sbyte ClampValue(sbyte value)
        {
            if (value.CompareTo(MinClampValue) < 0)
            {
                return MinClampValue;
            }
            else if (value.CompareTo(MaxClampValue) > 0)
            {
                return MaxClampValue;
            }
            else
            {
                return value;
            }
        }
    } 
}