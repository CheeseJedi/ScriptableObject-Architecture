using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "UShortCollection.asset",
        menuName = SOArchitecture_Utility.ADVANCED_COLLECTION_SUBMENU + "ushort",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 18)]
    public class UShortCollection : Collection<ushort>
    {
    } 
}