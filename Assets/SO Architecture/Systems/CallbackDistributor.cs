using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "CallbackDistributor.asset",
        menuName = SOArchitecture_Utility.SYSTEMS_SUBMENU + "Callback Distributor",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS + 0)]
    public class CallbackDistributor : SOArchitectureBaseObject
    {
        [Header("Hosted Systems")]
        [Tooltip("Hosted Systems added to this list will receive callbacks.")]
        public List<ScriptableObjectSystem> HostedSystems = default;
        [Header("Reset Variables On Awake")]
        [Tooltip("Variables added to this list will be reset to their configured default value.")]
        public List<BaseVariable> VariablesToReset = default;
        [Header("Back Burner")]
        [Tooltip("ScriptableObjects added to this list will be 'woken up' and 'kept warm' as long as the CallbackDistributor is referenced in the scene.")]
        public List<ScriptableObject> BackBurner = default;
        public CallbackDistributorHost HostMonoBehaviour { get; set; }
        public void Start()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                HostedSystems[i].CallbackDistributor = this;

                if ((HostedSystems[i].RequiresUpdatesOn & UpdateType.Start) == UpdateType.Start)
                    HostedSystems[i].Start();
            }
        }
        public void Update()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].RequiresUpdatesOn & UpdateType.Update) == UpdateType.Update)
                    HostedSystems[i].Update();
            }
        }
        public void FixedUpdate()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].RequiresUpdatesOn & UpdateType.FixedUpdate) == UpdateType.FixedUpdate)
                    HostedSystems[i].FixedUpdate();
            }
        }
        public void LateUpdate()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].RequiresUpdatesOn & UpdateType.LateUpdate) == UpdateType.LateUpdate)
                    HostedSystems[i].LateUpdate();
            }
        }
        public void OnGUI()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].RequiresUpdatesOn & UpdateType.OnGUI) == UpdateType.OnGUI)
                    HostedSystems[i].OnGUI();
            }
        }
        public void OnAwake()
        {
            // Reset any variables in the reset list.
            ResetVariables();
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].RequiresUpdatesOn & UpdateType.OnAwake) == UpdateType.OnAwake)
                    HostedSystems[i].OnAwake();
            }
        }
        public void OnQuit()
        {
            for (int i = 0; i < HostedSystems.Count; i++)
            {
                if ((HostedSystems[i].RequiresUpdatesOn & UpdateType.OnQuit) == UpdateType.OnQuit)
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
        public void ResetVariables()
        {
            for (int i = 0; i < VariablesToReset.Count; i++)
            {
                VariablesToReset[i].ResetToDefaultValue();
            }
        }
    }
    [System.Flags]
    public enum UpdateType { None = 0, Start = 1, Update = 2, FixedUpdate = 4, LateUpdate = 8, OnGUI = 16, OnAwake = 32, OnQuit = 64 }
}