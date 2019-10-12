using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class ScriptableObjectSystem : SOArchitectureBaseObject, ICallbackReceiver
    {
        public CallbackDistributorSystem CallbackDistributor { get; set; }
        public virtual UpdateType CallbackOn => UpdateType.None;

        private const string k_NoOverrideMessage = " called, but not overridden! Check the RequriesUpdateOn setting for receiving system.";
        public virtual void Start() => Debug.LogWarning($"{name}(ScriptableObjectSystem).Start:" + k_NoOverrideMessage);
        public virtual void Update() => Debug.LogWarning($"{name}(ScriptableObjectSystem).Update:" + k_NoOverrideMessage);
        public virtual void FixedUpdate() => Debug.LogWarning($"{name}(ScriptableObjectSystem).FixedUpdate:" + k_NoOverrideMessage);
        public virtual void LateUpdate() => Debug.LogWarning($"{name}(ScriptableObjectSystem).LateUpdate:" + k_NoOverrideMessage);
        public virtual void OnGUI() => Debug.LogWarning($"{name}(ScriptableObjectSystem).OnGUI:" + k_NoOverrideMessage);
        public virtual void OnAwake() => Debug.LogWarning($"{name}(ScriptableObjectSystem).OnAwake:" + k_NoOverrideMessage);
        public virtual void OnQuit() => Debug.LogWarning($"{name}(ScriptableObjectSystem).OnQuit:" + k_NoOverrideMessage);
    }
}