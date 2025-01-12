﻿using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "BoolVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "bool",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 5)]
    public sealed class BoolVariable : BaseVariable<bool>
    {
        public static BoolVariable CreateAsset() => EditorAssistantUtility.CreateAsset<BoolVariable>();
        public override bool IsPersistable => true;
    }
}
