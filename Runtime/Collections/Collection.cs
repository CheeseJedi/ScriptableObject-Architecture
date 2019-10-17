﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class Collection<T> : BaseCollection, IList<T> // IEnumerable<T>
    {
        public new T this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                _list[index] = value;
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
                    if (value > _list.Count - 1)
                    {
                        Debug.LogWarning("Collection.SelectedItemIndex: Unable to set, index out of range.");
                        return;
                    }
                    _selectedItemIndex = value;
                    Raise();
                }
            }
        }
        [SerializeField]
        protected List<T> _list = new List<T>();
        protected override IList List
        {
            get
            {
                return _list;
            }
        }
        public T SelectedItem
        {
            get
            {
                if (_selectedItemIndex == -1) return default;
                return _list[_selectedItemIndex];
            }
            set
            {
                if (!_list[_selectedItemIndex].Equals(value))
                {
                    if (!_list.Contains(value))
                    {
                        Debug.LogWarning("Collection.SelectedItem: Unable to set, item not in collection.");
                        return;
                    }
                    _selectedItemIndex = _list.IndexOf(value);
                    Raise();
                }
            }
        }
        public override System.Type Type
        {
            get
            {
                return typeof(T);
            }
        }
        public void Add(T obj)
        {
            if (!_list.Contains(obj))
            {
                _list.Add(obj);
                if (IsAutoSorted)
                {
                    Sort();
                }
                Raise();
            }
        }
        public bool Remove(T obj)
        {
            if (_list.Contains(obj))
            {
                _list.Remove(obj);
                if (IsAutoSorted)
                {
                    Sort();
                }
                Raise();
                return true;
            }
            return false;
        }
        public void Clear()
        {
            _list.Clear();
            _selectedItemIndex = -1;
            Raise();
        }
        public bool Contains(T value)
        {
            return _list.Contains(value);
        }
        public int IndexOf(T value)
        {
            return _list.IndexOf(value);
        }
        public void RemoveAt(int index)
        {
            if (index == _selectedItemIndex)
            {
                SelectedItemIndex--;
            }
            _list.RemoveAt(index);
            if (IsAutoSorted)
            {
                Sort();
            }
            Raise();
        }
        public void Insert(int index, T value)
        {
            _list.Insert(index, value);
            Raise();
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }
        public T Find(System.Predicate<T> match)
        {
            return _list.Find(match);
        }
        /// <summary>
        /// Sorts a Collection by the specified function (lambda expression).
        /// </summary>
        /// <typeparam name = "TKey"></typeparam>
        /// <param name = "sorter">The lambda expression to specify how to sort.</param>
        /// <param name = "reverse">Whether the sort is in reverse.</param>
        public void Sort<TKey>(System.Func<T, TKey> sorter, bool reverse = false)
        {
            if (_list.Count < 2) return;
            T selected = SelectedItem;
            if (reverse) _list = _list.OrderByDescending(sorter).ToList();
            else _list = _list.OrderBy(sorter).ToList();
            if (selected == null)
            {
                if (_list.Count > 0) _selectedItemIndex = 0;
                else _selectedItemIndex = -1;
            }
            else
            {
                _selectedItemIndex = _list.IndexOf(selected);
            }
            Raise();
        }
        /// <summary>
        /// Gets the next element including wrapping/cycling back to
        /// element zero if at the end of the list.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <param name="reverse">Whether the direction is reversed.</param>
        /// <returns>The next element in the list.</returns>
        public T GetNextInCycle(T element, bool reverse = false)
        {
            int currentIndex = _list.IndexOf(element);
            int nextIndex = (reverse ? _list.Count + currentIndex - 1 : currentIndex + 1) % _list.Count;
            return _list[nextIndex];
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        public override string ToString()
        {
            return "Collection<" + typeof(T) + ">(" + Count + ")";
        }
        public T[] ToArray() {
            return _list.ToArray();
        }
    } 
}
