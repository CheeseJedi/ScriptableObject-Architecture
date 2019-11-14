using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "SByteCollection.asset",
        menuName = SOArchitecture_Utility.ADVANCED_COLLECTION_SUBMENU + "sbyte",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 15)]
    public class SByteCollection : Collection<sbyte>
    {
    } 
}