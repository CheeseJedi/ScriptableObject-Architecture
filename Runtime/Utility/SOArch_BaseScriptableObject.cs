using UnityEngine;

namespace ScriptableObjectArchitecture
{
    /// <summary>
    /// Base class for SOArchitecture assets
    /// Implements developer descriptions
    /// </summary>
    public abstract class SOArch_BaseScriptableObject : ScriptableObject
    {
        public const string BASE_DEFAULT_DEVELOPER_DESCRIPTION 
            = "Default description for a class derived from SOArch_BaseScriptableObject. Click to edit.";
        [SerializeField]
        protected DeveloperDescription DeveloperDescription 
            = new DeveloperDescription(BASE_DEFAULT_DEVELOPER_DESCRIPTION);
        [SerializeField]
        protected UniqueId _uniqueId = new UniqueId();
        public UniqueId UniqueId => _uniqueId;
    } 
}
