using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "IntVariableCollection.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_COLLECTION_SUBMENU + "Int",
	    order = 180)]
	public class IntVariableCollection : Collection<IntVariable>
	{
        public override bool IsPersistable => true;
    }
}