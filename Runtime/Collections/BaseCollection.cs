using System.Collections;
using Type = System.Type;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseCollection : SOArchitectureBaseObject, IEnumerable
    {
        public object this[int index]
        {
            get
            {
                return List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        public int Count { get { return List.Count; } }

        public abstract IList List { get; }
        public abstract Type Type { get; }

        /// <summary>
        /// Whether the list is kept sorted - note only applies to
        /// classes that have implemented an overridden Sort method.
        /// </summary>
        public virtual bool IsAutoSorted => false;
        private const string DEFAULT_DEVELOPER_DESCRIPTION = "Default description for a Collection. Click to edit.";
        private void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }
        public bool Contains(object obj)
        {
            return List.Contains(obj);
        }

        /// <summary>
        /// Sort method stub - to be overridden by derived collections that
        /// need to have their contents sorted when an item is added or removed.
        /// </summary>
        /// <remarks>
        /// Note that the typed Collection's Add and Remove methods is preferred
        /// over directly modifying the contents via the Collection's List
        /// property - the latter will not trigger a Sort operation.
        /// </remarks>
        public virtual void Sort() => UnityEngine.Debug.LogWarning
            ($"{name}(BaseCollection).Sort: was called, but has no override.");
    }
}
