using UnityEngine;

namespace ScriptableObjectArchitecture
{
    /// <summary>
    /// Base class for SOArchitecture assets
    /// Implements developer descriptions
    /// </summary>
    public abstract class SOArch_BaseMonoBehaviour : MonoBehaviour
    {
        public const string BASE_DEFAULT_DEVELOPER_DESCRIPTION = "Default description for a class derived from SOArch_BaseMonoBehaviour. Click to edit.";
        [SerializeField]
        protected DeveloperDescription DeveloperDescription = new DeveloperDescription(BASE_DEFAULT_DEVELOPER_DESCRIPTION);
    }
}