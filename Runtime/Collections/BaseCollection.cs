using System.Collections;
using UnityEngine;
using Type = System.Type;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseCollection : GameEventBase, IList
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
        [SerializeField]
        protected int _selectedItemIndex = -1;
        public int SelectedItemIndex
        {
            get => _selectedItemIndex;
            set
            {
                if (_selectedItemIndex != value)
                {
                    if (value > List.Count - 1)
                    {
                        Debug.LogWarning("Collection.SelectedItemIndex: Unable to set, index out of range.");
                        return;
                    }
                    _selectedItemIndex = value;
                    Raise();
                }
            }
        }
        public object SelectedItem
        {
            get
            {
                if (_selectedItemIndex == -1) return default;
                return List[_selectedItemIndex];
            }
            set
            {
                if (!List[_selectedItemIndex].Equals(value))
                {
                    if (!List.Contains(value))
                    {
                        Debug.LogWarning("Collection.SelectedItem: Unable to set, item not in collection.");
                        return;
                    }
                    _selectedItemIndex = List.IndexOf(value);
                    Raise();
                }
            }
        }
        public int Count => List.Count;
        protected abstract IList List { get; }
        public abstract Type Type { get; }
        /// <summary>
        /// Whether the list is kept sorted - note only applies to
        /// classes that have implemented an overridden Sort method.
        /// </summary>
        public virtual bool IsAutoSorted => false;
        public virtual bool IsReadOnly => false;

        public bool IsFixedSize => List.IsFixedSize;
        public bool IsSynchronized => List.IsSynchronized;
        public object SyncRoot => List.SyncRoot;

        private const string DEFAULT_DEVELOPER_DESCRIPTION = "Default description for a Collection. Click to edit.";
#pragma warning disable IDE0051 // Remove unused private members
        private void Awake()
#pragma warning restore IDE0051 // Remove unused private members
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        public int Add(object obj)
        {
            if (!List.Contains(obj))
            {
                int result = List.Add(obj);
                if (IsAutoSorted)
                {
                    Sort();
                }
                Raise();
                return result;
            }
            return -1;
        }
        public void Remove(object obj)
        {
            if (List.Contains(obj))
            {
                List.Remove(obj);
                Raise();
            }
        }
        public void Clear()
        {
            List.Clear();
            _selectedItemIndex = -1;
            Raise();
        }
        public bool Contains(object obj) => List.Contains(obj);
        public int IndexOf(object obj) => List.IndexOf(obj);
        public void RemoveAt(int index)
        {
            if (_selectedItemIndex == index)
            {
                SelectedItemIndex--;
            }
            List.RemoveAt(index);
            Raise();
        }
        public void Insert(int index, object obj)
        {
            List.Insert(index, obj);
            Raise();
        }
        public void CopyTo(object[] array, int index) => List.CopyTo(array, index);
        public void CopyTo(System.Array array, int index) => List.CopyTo(array, index);
        IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();
        /// <summary>
        /// Sort method stub - to be overridden by derived collections that
        /// need to have their contents sorted when an item is added or removed.
        /// </summary>
        /// <remarks>
        /// Note that the typed Collection's Add and Remove methods is preferred
        /// over directly modifying the contents via the Collection's List
        /// property - the latter will not trigger a Sort operation.
        /// </remarks>
        public virtual void Sort() => Debug.LogWarning
            ($"{name}(BaseCollection).Sort: was called, but has no override.");
        /// <summary>
        /// Gets the next element including wrapping/cycling back to
        /// element zero if at the end of the list.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <param name="reverse">Whether the direction is reversed.</param>
        /// <returns>The next element in the list.</returns>
        public object GetNextInCycle(object element, bool reverse = false)
        {
            int currentIndex = List.IndexOf(element);
            int nextIndex = (reverse ? List.Count + currentIndex - 1 : currentIndex + 1) % List.Count;
            return List[nextIndex];
        }
        public override string ToString() => "BaseCollection<object>(" + Count + ")";
        public virtual string GetNameOfItem(int index) => $"[{index:00}]_{List[index].ToString()}";

    }
}
