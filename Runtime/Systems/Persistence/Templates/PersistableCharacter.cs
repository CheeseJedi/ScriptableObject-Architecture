
namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    public class PersistableCharacter : PersistableSOAScriptableObject<Character>
    {
        public float[] BaseColor;
        public float[] Highlight1;
        public float[] Highlight2;
        public float[] Highlight3;

        protected override void PopulateTemplateInternal()
        {
            Name = _typedObject.Name;
            UniqueId = _typedObject.PersistenceId.Value;
            TemplateType = typeof(PersistableCharacter).AssemblyQualifiedName;

            BaseColor = _typedObject.baseColor.ToFloatArray();
            Highlight1 = _typedObject.highlight1.ToFloatArray();
            Highlight2 = _typedObject.highlight2.ToFloatArray();
            Highlight3 = _typedObject.highlight3.ToFloatArray();
        }

        protected override void PopulateObjectInternal()
        {
            _typedObject.Name = Name;
            _typedObject.PersistenceId.Value = UniqueId;
            _typedObject.baseColor.FromFloatArray(BaseColor);
            _typedObject.highlight1.FromFloatArray(Highlight1);
            _typedObject.highlight2.FromFloatArray(Highlight2);
            _typedObject.highlight3.FromFloatArray(Highlight3);
        }
    }
}
