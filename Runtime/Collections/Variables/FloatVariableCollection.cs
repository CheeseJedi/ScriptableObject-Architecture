using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "FloatVariableCollection.asset",
        menuName = SOArchitecture_Utility.VARIABLE_COLLECTION_SUBMENU + "FloatVariable",
        order = 180)]
    public class FloatVariableCollection : Collection<FloatVariable>
    {
        public override bool IsPersistable => true;
    }
}
