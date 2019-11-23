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
            newCharacter.name = newCharacter.Name;
            newCharacter.PersistenceId.EnablePersistence();
            Add(newCharacter);
            if (SelectedItemIndex == -1)
            {
                SelectedItemIndex = 0;
            }
            return newCharacter;
        }
        public void RemoveCharacter(int index) => RemoveAt(index);

        protected void RemoveMissingCharacters()
        {
            for (int i = Count - 1; i > -1; i--)
            {
                if (this[i] == null) RemoveAt(i);
            }
        }

        protected void OnEnable() => RemoveMissingCharacters();
        protected void OnDisable() => RemoveMissingCharacters();

    }
}
