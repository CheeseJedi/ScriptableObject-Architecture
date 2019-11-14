using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class PersistableCharacterCollection : PersistableCollection<Character>
    {
        protected override void PopulateTemplateInternal()
        {
            base.PopulateTemplateInternal();
            TemplateType = typeof(PersistableCharacterCollection).AssemblyQualifiedName;
        }
        protected override bool PerformTypeCheck()
        {
            if (TemplateType != typeof(PersistableCharacterCollection).AssemblyQualifiedName)
            {
                Debug.LogError($"{nameof(PersistableCharacterCollection)}.PerformTypeCheck: FAILED! - Template type mismatch.\n" +
                    $"Expected: {typeof(PersistableCharacterCollection).AssemblyQualifiedName}\nGot        : {TemplateType}");
                return false;
            }
            else return true;
        }
    }
}
