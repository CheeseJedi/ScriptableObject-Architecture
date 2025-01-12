﻿using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "IntVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "int",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 4)]
    public class IntVariable : BaseVariable<int>
    {
        public static IntVariable CreateAsset() => EditorAssistantUtility.CreateAsset<IntVariable>();
        public override bool IsPersistable => true;
        public override bool Clampable { get { return true; } }
        protected override int ClampValue(int value)
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
