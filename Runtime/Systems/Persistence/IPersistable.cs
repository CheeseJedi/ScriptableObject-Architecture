using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public interface IPersistable
    {
        string Name { get; }
        bool PersistenceEnabled { get; }
        string PersistenceGuid { get; }
        System.Type Type { get; }
        bool HasChildObjects { get; }
        object Serialise();
        void Deserialise(object input);
    }
}