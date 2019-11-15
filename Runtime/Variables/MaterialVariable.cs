using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "MaterialVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Material",
	    order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 0)]
	public class MaterialVariable : BaseVariable<Material>
	{
		public static MaterialVariable CreateAsset() => EditorAssistantUtility.CreateAsset<MaterialVariable>();
	}
}
