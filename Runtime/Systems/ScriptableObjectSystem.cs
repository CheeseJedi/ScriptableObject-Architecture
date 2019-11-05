using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class ScriptableObjectSystem : SOArch_BaseScriptableObject, ICallbackReceiver
    {
        public CallbackDistributorSystem CallbackDistributor { get; set; }
        public virtual CallbackType CallbackOn => CallbackType.None;

        private const string NO_OVERRIDE_MESSAGE = " called, but not overridden! Check the CallbackOn setting for receiving system.";
        public virtual void Start() => Debug.LogWarning($"{name}(ScriptableObjectSystem).Start:" + NO_OVERRIDE_MESSAGE);
        public virtual void Update() => Debug.LogWarning($"{name}(ScriptableObjectSystem).Update:" + NO_OVERRIDE_MESSAGE);
        public virtual void FixedUpdate() => Debug.LogWarning($"{name}(ScriptableObjectSystem).FixedUpdate:" + NO_OVERRIDE_MESSAGE);
        public virtual void LateUpdate() => Debug.LogWarning($"{name}(ScriptableObjectSystem).LateUpdate:" + NO_OVERRIDE_MESSAGE);
        public virtual void OnGUI() => Debug.LogWarning($"{name}(ScriptableObjectSystem).OnGUI:" + NO_OVERRIDE_MESSAGE);
        public virtual void OnAwake() => Debug.LogWarning($"{name}(ScriptableObjectSystem).OnAwake:" + NO_OVERRIDE_MESSAGE);
        public virtual void OnQuit() => Debug.LogWarning($"{name}(ScriptableObjectSystem).OnQuit:" + NO_OVERRIDE_MESSAGE);
    }
}