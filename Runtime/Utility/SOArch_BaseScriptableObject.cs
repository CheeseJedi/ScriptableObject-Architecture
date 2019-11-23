using UnityEngine;

namespace ScriptableObjectArchitecture
{
    /// <summary>
    /// Base class for SOArchitecture assets
    /// Implements developer descriptions and persistence.
    /// </summary>
    public abstract class SOArch_BaseScriptableObject : ScriptableObject
    {
        public const string BASE_DEFAULT_DEVELOPER_DESCRIPTION 
            = "Default description for a class derived from SOArch_BaseScriptableObject. Click to edit.";
        [SerializeField, Tooltip("Developer description - click within the text field to edit.")]
        protected DeveloperDescription DeveloperDescription 
            = new DeveloperDescription(BASE_DEFAULT_DEVELOPER_DESCRIPTION);
        [SerializeField, Tooltip("Persistence settings.")]
        protected PersistenceId _persistenceId = default;
        /// <summary>
        /// Used to determine whether the object has a persistence template and can be persisted.
        /// Override this if the derived class has a suitable persistence template (model) class
        /// and, if necessary, extension methods to map between the two.
        /// </summary>
        public virtual bool IsPersistable => false;
        public PersistenceId PersistenceId => _persistenceId;
    } 
}
