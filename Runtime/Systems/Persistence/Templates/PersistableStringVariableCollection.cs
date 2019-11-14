using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class PersistableStringVariableCollection : PersistableCollection<StringVariable>
    {
        protected override void PopulateTemplateInternal()
        {
            base.PopulateTemplateInternal();
            TemplateType = typeof(PersistableStringVariableCollection).AssemblyQualifiedName;
        }
        protected override bool PerformTypeCheck()
        {
            if (TemplateType != typeof(PersistableStringVariableCollection).AssemblyQualifiedName)
            {
                Debug.LogError($"{nameof(PersistableStringVariableCollection)}.PerformTypeCheck: FAILED! - Template type mismatch.\n" +
                    $"Expected: {typeof(PersistableStringVariableCollection).AssemblyQualifiedName}\nGot        : {TemplateType}");
                return false;
            }
            else return true;
        }

    }
}
