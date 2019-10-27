using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class PersistableCollection<T> : Collection<T>, IPersistable 
        where T : IPersistable
    {
        //#region SO-System Integration
        //public static PersistableCollection<T> CreateAsset() => EditorAssistantUtility.CreateAsset<PersistableCollection<T>>();
        //#endregion
        #region IPersistable implementation
        [Header("Persistence")]
        [SerializeField, Tooltip("Whether this object is enabled for persistence")]
        private bool _persistenceEnabled = true;
        [SerializeField, Tooltip("Persistence Guid - must be unique.")]
        private string _persistenceGuid;
        public string Name => name;
        public bool PersistenceEnabled => _persistenceEnabled;
        public string PersistenceGuid
        {
            get
            {
                if (string.IsNullOrEmpty(_persistenceGuid))
                {
                    AssignGuid();
                }
                return _persistenceGuid;
            }
        }
        public bool HasChildObjects => _list.Count > 0;
        private void AssignGuid()
        {
            _persistenceGuid = System.Guid.NewGuid().ToString();
        }
        public abstract void Deserialise(object input);
        public abstract object Serialise();
        #endregion
    }
}
