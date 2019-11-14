using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public interface IDisplayable
    {
        string Name { get; }
        Sprite Icon { get; }
    }
}
