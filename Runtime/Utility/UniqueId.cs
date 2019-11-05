using System;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [Serializable]
    public sealed class UniqueId : IEquatable<UniqueId>, IEquatable<Guid>, IEquatable<string>
    {
        public const string EMPTY_GUID = "00000000-0000-0000-0000-000000000000";
        public UniqueId() => SetValue(Guid.NewGuid(), skipReadOnlyCheck: true);
        public UniqueId(string value) => SetValue(value, skipReadOnlyCheck: true);
        public UniqueId(Guid value) => SetValue(value, skipReadOnlyCheck: true);
        public string Value
        {
            get
            {
                if (_stringValue == EMPTY_GUID || string.IsNullOrEmpty(_stringValue))
                {
                    SetValue(Guid.NewGuid(), skipReadOnlyCheck: true);
                }
                return _stringValue;
            }
            set => SetValue(value);
        }
        public bool ReadOnly { get; set; } = true;
        private void SetValue(Guid value, bool skipReadOnlyCheck = false)
        {
            if (!skipReadOnlyCheck && ReadOnly)
            {
                Debug.LogException(
                    new InvalidOperationException($"{nameof(UniqueId)}: " +
                    $"Attempted to set UniqueId value when ReadOnly is true."));
                return;
            }
            _guidValue = value;
            _stringValue = _guidValue.ToString();
        }
        private void SetValue(string value, bool skipReadOnlyCheck = false)
        {
            if (!skipReadOnlyCheck && ReadOnly)
            {
                Debug.LogException(
                    new InvalidOperationException($"{nameof(UniqueId)}: " +
                    $"Attempted to set UniqueId value when ReadOnly is true."));
                return;
            }
            _stringValue = value;
            _guidValue = new Guid(_stringValue);
        }
        [SerializeField, HideInInspector]
        private string _stringValue = string.Empty;
        [NonSerialized]
        private Guid _guidValue;
        public static implicit operator string(UniqueId value) => value._stringValue;
        public static implicit operator UniqueId(string value) => new UniqueId(value);
        public static implicit operator Guid(UniqueId value) => value._guidValue;
        public static implicit operator UniqueId(Guid value) => new UniqueId(value);
        public override bool Equals(object obj)
        {
            return _stringValue.Equals(obj);
        }
        public bool Equals(UniqueId other)
        {
            if (other == null)
                return false;

            return _guidValue == other._guidValue &&_stringValue == other._stringValue;
        }
        public bool Equals(string other)
        {
            if (other == null)
                return false;

            return _stringValue == other;
        }
        public bool Equals(Guid other)
        {
            if (other == null)
                return false;

            return _guidValue == other;
        }
        public override int GetHashCode() => _guidValue.GetHashCode();
        public override string ToString() => Value;
        public Guid ToGuid() => _guidValue;
    }
}