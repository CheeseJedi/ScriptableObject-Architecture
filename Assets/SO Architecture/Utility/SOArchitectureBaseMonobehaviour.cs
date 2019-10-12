using UnityEngine;

namespace ScriptableObjectArchitecture
{
    /// <summary>
    /// Base class for SOArchitecture assets
    /// Implements developer descriptions
    /// </summary>
    public abstract class SOArchitectureBaseMonobehaviour : MonoBehaviour
    {
        public const string BASE_DEFAULT_DEVELOPER_DESCRIPTION = "Default description for a class derived from SOArchitectureBaseMonoBehaviour.";
#pragma warning disable 0414
        [SerializeField]
        private DeveloperDescription DeveloperDescription = new DeveloperDescription(BASE_DEFAULT_DEVELOPER_DESCRIPTION);
#pragma warning restore
    } 
}