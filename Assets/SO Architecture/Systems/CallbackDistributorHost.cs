using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [DefaultExecutionOrder(-80)]
    public class CallbackDistributorHost : SOArchitectureBaseMonobehaviour
    {
        [Header("Callback Distributor")]
        [Tooltip("The Callback Distributor asset - use the create menu to create one if one doesn't already exist.")]
        public CallbackDistributor CallbackDistributorAsset = default;
        [Header("Scene Load Survivability")]
        [Tooltip("Whether the SOSystemHost sets the DontDestroyOnLoad flag on it's parent GameObject.")]
        public bool SurviveSceneLoad = true;
        private void Awake()
        {
            if (CallbackDistributorAsset == null)
            {
                Debug.LogError("SOSystemHost.Awake: CallbackDistributorAsset was null.");
                return;
            }
            CallbackDistributorAsset.HostMonoBehaviour = this;

            if (SurviveSceneLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            CallbackDistributorAsset.OnAwake();
        }
        private void Start() => CallbackDistributorAsset?.Start();
        private void Update() => CallbackDistributorAsset?.Update();
        private void FixedUpdate() => CallbackDistributorAsset?.FixedUpdate();
        private void LateUpdate() => CallbackDistributorAsset?.LateUpdate();
        private void OnGUI() => CallbackDistributorAsset?.OnGUI();
        private void OnApplicationQuit() => CallbackDistributorAsset?.OnQuit();
    }
}
