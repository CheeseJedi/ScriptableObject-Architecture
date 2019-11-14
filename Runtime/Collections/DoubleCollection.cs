using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "DoubleCollection.asset",
        menuName = SOArchitecture_Utility.ADVANCED_COLLECTION_SUBMENU + "double",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 8)]
    public class DoubleCollection : Collection<double>
    {
    } 
}