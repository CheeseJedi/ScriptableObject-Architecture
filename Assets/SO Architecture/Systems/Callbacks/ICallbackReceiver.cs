using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public interface ICallbackReceiver
    {
        CallbackType CallbackOn { get; }
        CallbackDistributorSystem CallbackDistributor { get; }
        void Start();
        void Update();
        void FixedUpdate();
        void LateUpdate();
        void OnGUI();
        void OnAwake();
        void OnQuit();
    }
}