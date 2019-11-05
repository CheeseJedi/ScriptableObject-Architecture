using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [DefaultExecutionOrder(-80)]
    public class CallbackTransmitterComponent : SOArch_BaseMonoBehaviour, ICallbackTransmitter
    {
        [Header("Callback Receiver")]
        [Tooltip("The system that requires callbacks - typically a CallbackDistributorSystem asset.")]
        [EditorAssistant(typeof(CallbackDistributorSystem), missingObjectWarning: true, showCreateAssetButton: true, displayInspector: false)]
        public ScriptableObjectSystem CallbackReceiver = default;
        [Header("Scene Load Survivability")]
        [Tooltip("Whether this component sets the DontDestroyOnLoad flag on it's parent GameObject.")]
        public bool SurviveSceneLoad = true;

        public CallbackTransmitterComponent HostMonoBehaviour { get => this; set => throw new System.NotImplementedException(); }

        private void Awake()
        {
            if (CallbackReceiver == null)
            {
                Debug.LogError("CallbackTransmitter.Awake: CallbackReceiver was null.");
                return;
            }
            CallbackDistributorSystem receiver = CallbackReceiver as CallbackDistributorSystem;
            if (receiver != null)
            {
                receiver.HostMonoBehaviour = this;
            }
            if (SurviveSceneLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            CallbackReceiver.OnAwake();
        }
        private void Start() => CallbackReceiver?.Start();
        private void Update() => CallbackReceiver?.Update();
        private void FixedUpdate() => CallbackReceiver?.FixedUpdate();
        private void LateUpdate() => CallbackReceiver?.LateUpdate();
        private void OnGUI() => CallbackReceiver?.OnGUI();
        private void OnApplicationQuit() => CallbackReceiver?.OnQuit();
    }
}
