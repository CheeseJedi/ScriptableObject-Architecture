using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "CharacterCollection.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Character",
        order = 200)]
    public class CharacterCollection : Collection<Character>
    {
        public override bool IsPersistable => true;
        public override bool IsSelectedItemTracked => true;
        public override string GetNameOfItem(int index) => _list[index].Name;
        public Character CreateCharacter()
        {
            Character newCharacter = CreateInstance<Character>();
            Add(newCharacter);
            return newCharacter;
        }
        public void RemoveCharacter(int index) => RemoveAt(index);
    }
}
