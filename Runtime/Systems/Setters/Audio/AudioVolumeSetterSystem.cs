using UnityEngine;
using UnityEngine.Audio;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(fileName = "AudioVolumeSetterSystem.asset",
        menuName = SOArchitecture_Utility.SYSTEMS_SUBMENU + "Audio/Volume Setter",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS + 50)]
    public class AudioVolumeSetterSystem : VariableChangedListenerSystem<FloatVariable>
    {
        #region SO-System Integration
        public static AudioVolumeSetterSystem CreateAsset() => EditorAssistantUtility.CreateAsset<AudioVolumeSetterSystem>();
        public override CallbackType CallbackOn => CallbackType.Start;
        private const string DEFAULT_DEVELOPER_DESCRIPTION = "Sets a named volume parameter on an AudioMixer (log. adjustment).";
        private void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        #endregion

        [Header("Audio Volume Setter")]
        [Tooltip("Mixer to set the parameter in")]
        public AudioMixer Mixer = default;
        [Tooltip("Name of the parameter to set in the mixer")]
        public string ParameterName = string.Empty;

        public override void Start()
        {
            OnEventRaised();
        }

        public override void OnEventRaised()
        {
            if (Mixer != null && Variable != null)
            {
                float value = Variable.Value  > 0f ? 20f * Mathf.Log10(Variable.Value) : -80f;
                Mixer.SetFloat(ParameterName, value);
            }
        }
    }
}
