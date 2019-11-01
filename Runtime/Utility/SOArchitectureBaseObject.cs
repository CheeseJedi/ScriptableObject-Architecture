using UnityEngine;

namespace ScriptableObjectArchitecture
{
    /// <summary>
    /// Base class for SOArchitecture assets
    /// Implements developer descriptions
    /// </summary>
    public abstract class SOArchitectureBaseObject : ScriptableObject
    {
        public const string BASE_DEFAULT_DEVELOPER_DESCRIPTION = "Default description for a class derived from SOArchitectureBaseObject. Click to edit.";
        [SerializeField]
        protected DeveloperDescription DeveloperDescription = new DeveloperDescription(BASE_DEFAULT_DEVELOPER_DESCRIPTION);
    } 
}
