
namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    public class PersistableVariable<T> : PersistableSOAScriptableObject<BaseVariable<T>>
    {
        public T Value;

        protected override void PopulateTemplateInternal()
        {
            Name = _typedObject.name;
            UniqueId = _typedObject.PersistenceId.Value;
            TemplateType = typeof(PersistableVariable<T>).AssemblyQualifiedName;
            Value = _typedObject.Value;
        }

        protected override void PopulateObjectInternal()
        {
            if (_typedObject.name == Name)
            {
                _typedObject.Value = Value;
            }
        }
    }
}
