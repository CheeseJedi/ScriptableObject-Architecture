using UnityEngine;

namespace ScriptableObjectArchitecture
{
    /// <summary>
    /// Base class for SOArchitecture assets
    /// Implements developer descriptions
    /// </summary>
    public abstract class SOArchitectureBaseObject : ScriptableObject
    {
        public const string BASE_DEFAULT_DEVELOPER_DESCRIPTION = "Default description for a class derived from SOArchitectureBaseObject. "
            + "Redefine the string constant DEFAULT_DEVELOPER_DESCRIPTION to change this text.";
#pragma warning disable 0414
        [SerializeField]
        protected DeveloperDescription DeveloperDescription = new DeveloperDescription(BASE_DEFAULT_DEVELOPER_DESCRIPTION);
#pragma warning restore
    } 
}