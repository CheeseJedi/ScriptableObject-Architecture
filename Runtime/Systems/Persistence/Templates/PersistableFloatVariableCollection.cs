using ScriptableObjectArchitecture;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class PersistableFloatVariableCollection : PersistableCollection<FloatVariable>
    {
        protected override void PopulateTemplateInternal()
        {
            base.PopulateTemplateInternal();
            TemplateType = typeof(PersistableFloatVariableCollection).AssemblyQualifiedName;
        }
        protected override bool PerformTypeCheck()
        {
            if (TemplateType != typeof(PersistableFloatVariableCollection).AssemblyQualifiedName)
            {
                Debug.LogError($"{nameof(PersistableFloatVariableCollection)}.PerformTypeCheck: FAILED! - Template type mismatch.\n" +
                    $"Expected: {typeof(PersistableFloatVariableCollection).AssemblyQualifiedName}\nGot        : {TemplateType}");
                return false;
            }
            else return true;
        }
    }
}
