﻿using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseVariable : GameEventBase, IPersistable
    {
        [SerializeField]
        protected bool _readOnly = false;
        [SerializeField]
        protected bool _raiseWarning = true;
        [SerializeField]
        protected bool _isClamped = false;
        public virtual bool Clampable => false;
        public virtual bool IsClamped => _isClamped;
        public virtual bool ReadOnly => _readOnly;
        public abstract System.Type Type { get; }
        public abstract object BaseValue { get; set; }
        private const string DEFAULT_DEVELOPER_DESCRIPTION = "Default description for a Variable. Click to edit.";
#pragma warning disable IDE0051 // Remove unused private members
        private void Awake()
#pragma warning restore IDE0051 // Remove unused private members
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        public abstract void ResetToDefaultValue();
        protected void RaiseReadonlyWarning()
        {
            if (!_readOnly || !_raiseWarning)
                return;

            Debug.LogWarning("Tried to set value on " + name + ", but value is readonly!", this);
        }

        #region IPersistable implementation
        [Header("Persistence")]
        [SerializeField, Tooltip("Persistence Guid - must be unique.")]
        private string _persistenceGuid;
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
        public virtual bool PersistenceEnabled => false;
        public virtual bool HasChildObjects => false;
        /// <summary>
        /// An over-ridable method to populate the object with the passed input data.
        /// </summary>
        /// <param name="input"></param>
        public virtual void Deserialise(object input) => throw new System.NotImplementedException
            ($"{name}(BaseVariable).Deserialise: Called, but not over-ridden.");
        /// <summary>
        /// An over-ridable method to create serialisable data from the object's state.
        /// </summary>
        /// <returns></returns>
        public virtual object Serialise() => throw new System.NotImplementedException
            ($"{name}(BaseVariable).Serialise: Called, but not over-ridden.");
        private void AssignGuid()
        {
            _persistenceGuid = System.Guid.NewGuid().ToString();
        }
        #endregion

    }
    public abstract class BaseVariable<T> : BaseVariable
    {
        public virtual T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = SetValue(value);
                Raise();
            }
        }
        public virtual T MinClampValue
        {
            get
            {
                if(Clampable)
                {
                    return _minClampedValue;
                }
                else
                {
                    return default;
                }
            }
        }
        public virtual T MaxClampValue
        {
            get
            {
                if(Clampable)
                {
                    return _maxClampedValue;
                }
                else
                {
                    return default;
                }
            }
        }

        public override System.Type Type { get { return typeof(T); } }
        public override object BaseValue
        {
            get
            {
                return _value;
            }
            set
            {
                _value = SetValue((T)value);
                Raise();
            }
        }

        [SerializeField]
        protected T _value = default;
        [SerializeField]
        protected T _defaultValue = default;
        [SerializeField]
        protected T _minClampedValue = default;
        [SerializeField]
        protected T _maxClampedValue = default;

        public virtual T SetValue(T value)
        {
            if (_readOnly)
            {
                RaiseReadonlyWarning();
                return _value;
            }
            else if(Clampable && IsClamped)
            {
                return ClampValue(value);
            }

            return value;
        }
        public virtual T SetValue(BaseVariable<T> value)
        {
            if (_readOnly)
            {
                RaiseReadonlyWarning();
                return _value;
            }
            else if (Clampable && IsClamped)
            {
                return ClampValue(value.Value);
            }

            return value.Value;
        }
        /// <summary>
        /// Resets the variable's value to its configured default value.
        /// </summary>
        public override void ResetToDefaultValue()
        {
            Value = _defaultValue;
        }
        protected virtual T ClampValue(T value)
        {
            return value;
        }
        public override string ToString()
        {
            return _value == null ? "null" : _value.ToString();
        }
        public static implicit operator T(BaseVariable<T> variable)
        {
            return variable.Value;
        }
    } 
}
