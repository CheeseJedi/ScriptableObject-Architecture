using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu]
    public class Character : SOArch_BaseScriptableObject, IDisplayable
    {
        public override bool IsPersistable => true;
        private const string DEFAULT_DEVELOPER_DESCRIPTION = "Default description for a character. Click to edit";

        protected virtual void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);

            if (_persistenceId == null)
            {
                _persistenceId = new PersistenceId();
            }
        }

        protected const string DEFAULT_NAME = "Unnamed Character";
        [Header("Name")]
        [SerializeField, Tooltip("The character's name.")]
        protected string characterName = DEFAULT_NAME;

        [Header("Character Colours")]
        public Color baseColor = Color.white;
        public Color highlight1 = Color.red;
        public Color highlight2 = Color.green;
        public Color highlight3 = Color.blue;

        public string Name
        {
            get => characterName;
            set
            {
                if (characterName != value)
                {
                    characterName = value;
                    name = value;
                }
            }
        }

        #region IDisplayable implementation
        public Sprite Icon => null; // No implementation for Icon currently.
        #endregion

        private void OnValidate()
        {
            name = characterName;
        }
    }
}
