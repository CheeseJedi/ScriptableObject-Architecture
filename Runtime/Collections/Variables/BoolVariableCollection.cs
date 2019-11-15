using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "BoolVariableCollection.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_COLLECTION_SUBMENU + "BoolVariable",
	    order = 180)]
	public class BoolVariableCollection : Collection<BoolVariable>
	{
	}
}
