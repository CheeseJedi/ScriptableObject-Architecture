using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "CallbackDistributorSystem.asset",
        menuName = SOArchitecture_Utility.SYSTEMS_SUBMENU + "Callback Distributor",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS + 0)]
    public class CallbackDistributorSystem : Collection<ScriptableObjectSystem>, ICallbackTransmitter
    {
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
        [Header("Back Burner")]
        [Tooltip("ScriptableObjects added to this list will be 'kept alive' (not unloaded) as long as the CallbackDistributorSystem is referenced in the scene.")]
        public List<ScriptableObject> BackBurner = default;
        public override void Start()
        {
            for (int i = 0; i < Count; i++)
            {
                this[i].CallbackDistributor = this;
                if ((this[i].CallbackOn & CallbackType.Start) == CallbackType.Start)
                    this[i].Start();
            }
        }
        public override void Update()
        {
            for (int i = 0; i < Count; i++)
            {
                if ((this[i].CallbackOn & CallbackType.Update) == CallbackType.Update)
                    this[i].Update();
            }
        }
        public override void FixedUpdate()
        {
            for (int i = 0; i < Count; i++)
            {
                if ((this[i].CallbackOn & CallbackType.FixedUpdate) == CallbackType.FixedUpdate)
                    this[i].FixedUpdate();
            }
        }
        public override void LateUpdate()
        {
            for (int i = 0; i < Count; i++)
            {
                if ((this[i].CallbackOn & CallbackType.LateUpdate) == CallbackType.LateUpdate)
                    this[i].LateUpdate();
            }
        }
        public override void OnGUI()
        {
            for (int i = 0; i < Count; i++)
            {
                if ((this[i].CallbackOn & CallbackType.OnGUI) == CallbackType.OnGUI)
                    this[i].OnGUI();
            }
        }
        public override void OnAwake()
        {
            for (int i = 0; i < Count; i++)
            {
                if ((this[i].CallbackOn & CallbackType.OnAwake) == CallbackType.OnAwake)
                    this[i].OnAwake();
            }
        }
        public override void OnQuit()
        {
            for (int i = 0; i < Count; i++)
            {
                if ((this[i].CallbackOn & CallbackType.OnQuit) == CallbackType.OnQuit)
                    this[i].OnQuit();
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
