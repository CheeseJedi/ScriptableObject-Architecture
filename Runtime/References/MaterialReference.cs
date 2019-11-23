using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class MaterialReference : BaseReference<Material, MaterialVariable>
	{
	    public MaterialReference() : base() { }
	    public MaterialReference(Material value) : base(value) { }
	}
}
