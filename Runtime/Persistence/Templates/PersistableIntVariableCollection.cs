using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class PersistableIntVariableCollection : PersistableCollection<IntVariable>
    {
        protected override void PopulateTemplateInternal()
        {
            base.PopulateTemplateInternal();
            TemplateType = typeof(PersistableIntVariableCollection).AssemblyQualifiedName;
        }
        protected override bool PerformTypeCheck()
        {
            if (TemplateType != typeof(PersistableIntVariableCollection).AssemblyQualifiedName)
            {
                Debug.LogError($"{nameof(PersistableIntVariableCollection)}.PerformTypeCheck: FAILED! - Template type mismatch.\n" +
                    $"Expected: {typeof(PersistableIntVariableCollection).AssemblyQualifiedName}\nGot        : {TemplateType}");
                return false;
            }
            else return true;
        }
    }
}
