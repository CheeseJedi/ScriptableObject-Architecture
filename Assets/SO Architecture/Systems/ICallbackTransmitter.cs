using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public interface ICallbackTransmitter
    {
        CallbackTransmitterComponent HostMonoBehaviour { get; set; }
    }
}
