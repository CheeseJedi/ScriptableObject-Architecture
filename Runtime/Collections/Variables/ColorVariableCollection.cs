using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
        fileName = "ColorVariableCollection.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_COLLECTION_SUBMENU + "Structs/ColorVariable",
	    order = 180)]
	public class ColorVariableCollection : Collection<ColorVariable>
	{
        public override bool IsSelectedItemTracked => true;
        public override string GetNameOfItem(int index)
        {
            return _list[index].name;
        }
    }
}
