using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "MaterialCollection.asset",
	    menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Material",
	    order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 0)]
	public class MaterialCollection : Collection<Material>
	{
        public override bool IsSelectedItemTracked => true;
    }
}
