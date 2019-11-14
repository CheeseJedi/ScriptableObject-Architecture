using System;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [Serializable]
    public class PersistenceId : IEquatable<PersistenceId>, IEquatable<Guid>, IEquatable<string>
    {
        public const string EMPTY_GUID = "00000000-0000-0000-0000-000000000000";
        public PersistenceId() => GenerateNewId();
        public PersistenceId(string value) => SetValue(value);
        public PersistenceId(Guid value) => SetValue(value);
        public string Value
        {
            get => _stringValue;
            set
            {
                _stringValue = value;
                _guidValue = new Guid(_stringValue);
            }
        }
        public Guid GuidValue
        {
            get => _guidValue;
            set
            {
                _guidValue = value;
                _stringValue = _guidValue.ToString();
            }
        }
        protected void SetValue(Guid value) => GuidValue = value;
        protected void SetValue(string value) => Value = value;
        [SerializeField, Tooltip("Whether this object takes part in persistence.")]
        protected bool _persistenceEnabled = false;
        [SerializeField]
        private string _stringValue = string.Empty;
        [NonSerialized]
        private Guid _guidValue;
        public static implicit operator string(PersistenceId value) => value._stringValue;
        public static implicit operator PersistenceId(string value) => new PersistenceId(value);
        public static implicit operator Guid(PersistenceId value) => value._guidValue;
        public static implicit operator PersistenceId(Guid value) => new PersistenceId(value);
        public override bool Equals(object obj)
        {
            return _stringValue.Equals(obj);
        }
        public bool Equals(PersistenceId other)
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
        public override string ToString() => _stringValue;
        public Guid ToGuid() => _guidValue;
        public static readonly PersistenceId Default = new PersistenceId(EMPTY_GUID)
        {
            _persistenceEnabled = false
        };
        public void EnablePersistence() => _persistenceEnabled = true;
        public void DisablePersistence() => _persistenceEnabled = false;
        public void GenerateNewId() => SetValue(Guid.NewGuid());
    }
}
