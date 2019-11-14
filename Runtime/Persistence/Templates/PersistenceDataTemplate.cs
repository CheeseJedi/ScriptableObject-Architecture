using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    public class PersistenceDataTemplate : PersistableCollection<SOArch_BaseScriptableObject>
    {
        #region Core Data
        public string Version;
        #endregion
        protected override void PopulateTemplateInternal()
        {
            base.PopulateTemplateInternal();
            Version = ((PersistenceSystem)_typedObject).versionValue;
            TemplateType = typeof(PersistenceDataTemplate).AssemblyQualifiedName;
        }
        protected override bool PerformTypeCheck()
        {
            if (TemplateType != typeof(PersistenceDataTemplate).AssemblyQualifiedName)
            {
                Debug.LogError($"{nameof(PersistenceDataTemplate)}.PerformTypeCheck: FAILED! - Template type mismatch.\n" +
                    $"Expected: {typeof(PersistenceDataTemplate).AssemblyQualifiedName}\nGot        : {TemplateType}");
                return false;
            }
            else return true;
        }
    }
}
