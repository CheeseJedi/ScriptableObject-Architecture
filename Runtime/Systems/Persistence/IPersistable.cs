using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public interface IPersistable
    {
        string PersistenceGuid { get; }
        System.Type Type { get; }
        bool HasChildObjects { get; }
        bool PersistenceEnabled { get; }
        string Serialise();
        void Deserialise(string input);
    }
}