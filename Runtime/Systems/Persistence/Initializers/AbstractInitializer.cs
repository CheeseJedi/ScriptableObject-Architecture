
namespace ScriptableObjectArchitecture
{
    public abstract class AbstractInitializer : SOArch_BaseScriptableObject
    {
        #region SOA Integration
        private const string DEFAULT_DEVELOPER_DESCRIPTION =
            "Default description for a class derived from AbstractInitializer. Click to edit.";
        protected virtual void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        #endregion
        public abstract void Initialize();
    }
}
