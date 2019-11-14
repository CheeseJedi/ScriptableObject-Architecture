using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class PersistableBoolVariableCollection : PersistableCollection<BoolVariable>
    {
        protected override void PopulateTemplateInternal()
        {
            base.PopulateTemplateInternal();
            TemplateType = typeof(PersistableBoolVariableCollection).AssemblyQualifiedName;
        }
        protected override bool PerformTypeCheck()
        {
            if (TemplateType != typeof(PersistableBoolVariableCollection).AssemblyQualifiedName)
            {
                Debug.LogError($"{nameof(PersistableBoolVariableCollection)}.PerformTypeCheck: FAILED! - Template type mismatch.\n" +
                    $"Expected: {typeof(PersistableBoolVariableCollection).AssemblyQualifiedName}\nGot        : {TemplateType}");
                return false;
            }
            else return true;
        }
    }
}
