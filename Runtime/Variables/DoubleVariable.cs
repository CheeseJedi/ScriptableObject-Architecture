﻿using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "DoubleVariable.asset",
        menuName = SOArchitecture_Utility.ADVANCED_VARIABLE_SUBMENU + "double",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 8)]
    public class DoubleVariable : BaseVariable<double>
    {
        public static DoubleVariable CreateAsset() => EditorAssistantUtility.CreateAsset<DoubleVariable>();
        public override bool Clampable { get { return true; } }
        protected override double ClampValue(double value)
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