
namespace ScriptableObjectArchitecture
{
    public static partial class PersistenceExtensions
    {
        public static bool IsPersistable(this object obj) => false;


        // PersistenceSystem
        //public static bool IsPersistable(this PersistenceSystem obj) => true;
        public static PersistenceDataTemplate ToPersistenceTemplate(this PersistenceSystem system)
        {
            PersistenceDataTemplate template = new PersistenceDataTemplate();
            template.PopulateTemplate(system);
            return template;
        }
        public static void FromPersistenceTemplate(this PersistenceSystem system, PersistenceDataTemplate template)
        {
            template.PopulateObject(system);
        }

        // FloatVariableCollection
        //public static bool IsPersistable(this FloatVariableCollection obj) => true;
        public static PersistableFloatVariableCollection ToPersistenceTemplate(this FloatVariableCollection collection)
        {
            PersistableFloatVariableCollection template = new PersistableFloatVariableCollection();
            template.PopulateTemplate(collection);
            return template;
        }
        public static void FromPersistenceTemplate(this FloatVariableCollection collection, PersistableFloatVariableCollection template)
        {
            template.PopulateObject(collection);
        }

        // IntVariableCollection
        //public static bool IsPersistable(this IntVariableCollection obj) => true;
        public static PersistableIntVariableCollection ToPersistenceTemplate(this IntVariableCollection collection)
        {
            PersistableIntVariableCollection template = new PersistableIntVariableCollection();
            template.PopulateTemplate(collection);
            return template;
        }
        public static void FromPersistenceTemplate(this FloatVariableCollection collection, PersistableIntVariableCollection template)
        {
            template.PopulateObject(collection);
        }

        // StringVariableCollection
        //public static bool IsPersistable(this StringVariableCollection obj) => true;
        public static PersistableStringVariableCollection ToPersistenceTemplate(this StringVariableCollection collection)
        {
            PersistableStringVariableCollection template = new PersistableStringVariableCollection();
            template.PopulateTemplate(collection);
            return template;
        }
        public static void FromPersistenceTemplate(this FloatVariableCollection collection, PersistableStringVariableCollection template)
        {
            template.PopulateObject(collection);
        }

        // BoolVariableCollection
        //public static bool IsPersistable(this BoolVariableCollection obj) => true;
        public static PersistableBoolVariableCollection ToPersistenceTemplate(this BoolVariableCollection collection)
        {
            PersistableBoolVariableCollection template = new PersistableBoolVariableCollection();
            template.PopulateTemplate(collection);
            return template;
        }
        public static void FromPersistenceTemplate(this FloatVariableCollection collection, PersistableBoolVariableCollection template)
        {
            template.PopulateObject(collection);
        }

        
        // FloatVariable
        //public static bool IsPersistable(this FloatVariable obj) => true;
        public static PersistableVariable<float> ToPersistenceTemplate(this FloatVariable variable)
        {
            PersistableVariable<float> template = new PersistableVariable<float>();
            template.PopulateTemplate(variable);
            return template;
        }
        public static void FromPersistenceTemplate(this FloatVariable variable, PersistableVariable<float> template)
        {
            template.PopulateObject(variable);
        }
        
        // IntVariable
        //public static bool IsPersistable(this IntVariable obj) => true;
        public static PersistableVariable<int> ToPersistenceTemplate(this IntVariable variable)
        {
            PersistableVariable<int> template = new PersistableVariable<int>();
            template.PopulateTemplate(variable);
            return template;
        }
        public static void FromPersistenceTemplate(this IntVariable variable, PersistableVariable<int> template)
        {
            template.PopulateObject(variable);
        }
        
        // StringVariable
        //public static bool IsPersistable(this StringVariable obj) => true;
        public static PersistableVariable<string> ToPersistenceTemplate(this StringVariable variable)
        {
            PersistableVariable<string> template = new PersistableVariable<string>();
            template.PopulateTemplate(variable);
            return template;
        }
        public static void FromPersistenceTemplate(this StringVariable variable, PersistableVariable<string> template)
        {
            template.PopulateObject(variable);
        }
        
        // BoolVariable
        //public static bool IsPersistable(this BoolVariable obj) => true;
        public static PersistableVariable<bool> ToPersistenceTemplate(this BoolVariable variable)
        {
            PersistableVariable<bool> template = new PersistableVariable<bool>();
            template.PopulateTemplate(variable);
            return template;
        }
        public static void FromPersistenceTemplate(this BoolVariable variable, PersistableVariable<bool> template)
        {
            template.PopulateObject(variable);
        }

        /// ** CHARACTERS **

        // CharacterCollection
        public static bool IsPersistable(this CharacterCollection obj) => true;
        public static PersistableCharacterCollection ToPersistenceTemplate(this CharacterCollection collection)
        {
            PersistableCharacterCollection template = new PersistableCharacterCollection();
            template.PopulateTemplate(collection);
            return template;
        }
        public static void FromPersistenceTemplate(this CharacterCollection collection, PersistableCharacterCollection template)
        {
            template.PopulateObject(collection);
        }

        // Character
        public static bool IsPersistable(this Character obj) => true;
        public static PersistableCharacter ToPersistenceTemplate(this Character character)
        {
            PersistableCharacter template = new PersistableCharacter();
            template.PopulateTemplate(character);
            return template;
        }
        public static void FromPersistenceTemplate(this Character character, PersistableCharacter template)
        {
            template.PopulateObject(character);
        }
    }
}
