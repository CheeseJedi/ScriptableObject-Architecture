using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "CallbackDistributorSystem.asset",
        menuName = SOArchitecture_Utility.SYSTEMS_SUBMENU + "Callback Distributor",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS + 0)]
    public class CallbackDistributorSystem : ScriptableObjectSystem, ICallbackReceiver, ICallbackTransmitter
    {
        [Header("Hosted Systems")]
        [Tooltip("Hosted Systems added to this list will receive callbacks.")]
        [EditorAssistant(typeof(ScriptableObjectSystem), missingObjectWarning: true, showCreateAssetButton: false, displayInspector: false)]
        public List<ScriptableObjectSystem> HostedSystems = default;
        [Header("Back Burner")]
        [Tooltip("ScriptableObjects added to this list will be 'woken up' and 'kept warm' as long as the CallbackDistributorSystem is referenced in the scene.")]
        public List<ScriptableObject> BackBurner = default;

        public static CallbackDistributorSystem CreateAsset() =>
            EditorAssistantUtility.CreateAsset<CallbackDistributorSystem>();
        public override UpdateType CallbackOn => UpdateType.OnAwake | UpdateType.Start |
            UpdateType.FixedUpdate | UpdateType.Update | UpdateType.LateUpdate |
            UpdateType.OnGUI | UpdateType.OnQuit;
        public CallbackTransmitterComponent HostMonoBehaviour { get; set; }
        public override void Start()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                HostedSystems[i].CallbackDistributor = this;
                if ((HostedSystems[i].CallbackOn & UpdateType.Start) == UpdateType.Start)
                    HostedSystems[i].Start();
            }
        }
        public override void Update()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & UpdateType.Update) == UpdateType.Update)
                    HostedSystems[i].Update();
            }
        }
        public override void FixedUpdate()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & UpdateType.FixedUpdate) == UpdateType.FixedUpdate)
                    HostedSystems[i].FixedUpdate();
            }
        }
        public override void LateUpdate()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & UpdateType.LateUpdate) == UpdateType.LateUpdate)
                    HostedSystems[i].LateUpdate();
            }
        }
        public override void OnGUI()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & UpdateType.OnGUI) == UpdateType.OnGUI)
                    HostedSystems[i].OnGUI();
            }
        }
        public override void OnAwake()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & UpdateType.OnAwake) == UpdateType.OnAwake)
                    HostedSystems[i].OnAwake();
            }
        }
        public override void OnQuit()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & UpdateType.OnQuit) == UpdateType.OnQuit)
                    HostedSystems[i].OnQuit();
            }
        }
        public void OnQuitRequestReceived()
        {
            OnQuit();
            // Handle the actual quit operation here.
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}