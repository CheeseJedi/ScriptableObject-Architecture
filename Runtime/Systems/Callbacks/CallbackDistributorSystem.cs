using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "CallbackDistributorSystem.asset",
        menuName = SOArchitecture_Utility.SYSTEMS_SUBMENU + "Callback Distributor",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS + 0)]
    public class CallbackDistributorSystem : ScriptableObjectSystem, ICallbackTransmitter
    {
        [Header("Hosted Systems")]
        [Tooltip("Hosted Systems added to this list will receive callbacks.")]
        //[EditorAssistant(typeof(ScriptableObjectSystem), missingObjectWarning: true, showCreateAssetButton: false, displayInspector: false)]
        public List<ScriptableObjectSystem> HostedSystems = default;
        [Header("Back Burner")]
        [Tooltip("ScriptableObjects added to this list will be 'kept alive' (not unloaded) as long as the CallbackDistributorSystem is referenced in the scene.")]
        public List<ScriptableObject> BackBurner = default;

        public static CallbackDistributorSystem CreateAsset() =>
            EditorAssistantUtility.CreateAsset<CallbackDistributorSystem>();
        public override CallbackType CallbackOn => CallbackType.Everything;
        public CallbackTransmitterComponent HostMonoBehaviour { get; set; }
        private const string DEFAULT_DEVELOPER_DESCRIPTION = "This system re-distributes callbacks received from a "
            + "CallbackTransmitterComponent in a scene to other ScritpableObjectSystems.";
        private void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        public override void Start()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                HostedSystems[i].CallbackDistributor = this;
                if ((HostedSystems[i].CallbackOn & CallbackType.Start) == CallbackType.Start)
                    HostedSystems[i].Start();
            }
        }
        public override void Update()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & CallbackType.Update) == CallbackType.Update)
                    HostedSystems[i].Update();
            }
        }
        public override void FixedUpdate()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & CallbackType.FixedUpdate) == CallbackType.FixedUpdate)
                    HostedSystems[i].FixedUpdate();
            }
        }
        public override void LateUpdate()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & CallbackType.LateUpdate) == CallbackType.LateUpdate)
                    HostedSystems[i].LateUpdate();
            }
        }
        public override void OnGUI()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & CallbackType.OnGUI) == CallbackType.OnGUI)
                    HostedSystems[i].OnGUI();
            }
        }
        public override void OnAwake()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & CallbackType.OnAwake) == CallbackType.OnAwake)
                    HostedSystems[i].OnAwake();
            }
        }
        public override void OnQuit()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].CallbackOn & CallbackType.OnQuit) == CallbackType.OnQuit)
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