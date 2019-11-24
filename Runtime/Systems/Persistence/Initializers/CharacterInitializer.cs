using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(fileName = "CharacterInitializer.asset",
        menuName = SOArchitecture_Utility.PERSISTENCE_SUBMENU + "Initializers/Character",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS)]
    public class CharacterInitializer : AbstractInitializer
    {
        #region SOA Integration
        private const string DEFAULT_DEVELOPER_DESCRIPTION =
            "An Initializer that creates a new character when the persistence system initially creates a missing settings file.";
        protected override void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        #endregion
        [Header("Character Initializer")]
        [SerializeField, Tooltip("The Character Collection to create an initial character in.")]
        private CharacterCollection _characterCollection = default;
        public override void Initialize()
        {
            if (_characterCollection != null)
            {
                Character newCharacter = _characterCollection.CreateCharacter();
                Debug.Log($"{System.DateTime.UtcNow}: New character '{newCharacter.Name}'created.");
            }
        }
    }
}
