using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "StringVariableCollection.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_COLLECTION_SUBMENU + "StringVariable",
	    order = 180)]
	public class StringVariableCollection : Collection<StringVariable>
	{
        public override bool IsPersistable => true;
    }
}