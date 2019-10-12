using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class ScriptableObjectSystem : SOArchitectureBaseObject
    {
        [Header("Callback Configuration")]
        [SerializeField, Tooltip("When this ScriptableObjectSystem requires updates - multi-select possible.")]
        public UpdateType RequiresUpdatesOn = UpdateType.None;
        [System.NonSerialized]
        [EditorAssistant(typeof(CallbackDistributor), missingObjectWarning: true, showCreateAssetButton: true, displayInspector: true)]
        public CallbackDistributor CallbackDistributor;
        private const string k_NoOverrideMessage = " called, but not overridden! Check the RequriesUpdateOn setting for Hosted SO Systems.";
        public virtual void Start() => Debug.LogWarning($"{name}(ScriptableObjectSystem).Start:" + k_NoOverrideMessage);
        public virtual void Update() => Debug.LogWarning($"{name}(ScriptableObjectSystem).Update:" + k_NoOverrideMessage);
        public virtual void FixedUpdate() => Debug.LogWarning($"{name}(ScriptableObjectSystem).FixedUpdate:" + k_NoOverrideMessage);
        public virtual void LateUpdate() => Debug.LogWarning($"{name}(ScriptableObjectSystem).LateUpdate:" + k_NoOverrideMessage);
        public virtual void OnGUI() => Debug.LogWarning($"{name}(ScriptableObjectSystem).OnGUI:" + k_NoOverrideMessage);
        public virtual void OnAwake() => Debug.LogWarning($"{name}(ScriptableObjectSystem).OnAwake:" + k_NoOverrideMessage);
        public virtual void OnQuit() => Debug.LogWarning($"{name}(ScriptableObjectSystem).OnQuit:" + k_NoOverrideMessage);
    }
}