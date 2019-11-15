using UnityEngine;
using UnityEngine.Audio;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(fileName = "AudioParameterSetterSystem.asset",
        menuName = SOArchitecture_Utility.SYSTEMS_SUBMENU + "Audio/Parameter Setter",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS + 50)]
    public class AudioParameterSetter : VariableChangedListenerSystem<FloatVariable>
    {
        #region SO-System Integration
        public static AudioParameterSetter CreateAsset() => EditorAssistantUtility.CreateAsset<AudioParameterSetter>();
        public override CallbackType CallbackOn => CallbackType.Start;
        private const string DEFAULT_DEVELOPER_DESCRIPTION = "Sets a named parameter on an AudioMixer.";
        private void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        #endregion

        [Header("Audio Parameter Setter")]
        [Tooltip("Mixer to set the parameter in.")]
        public AudioMixer Mixer = default;
        [Tooltip("Name of the parameter to set in the mixer.")]
        public string ParameterName = string.Empty;
        [Tooltip("Minimum value of the Variable that is mapped to the curve.")]
        public FloatReference Min = default;
        [Tooltip("Maximum value of the Variable that is mapped to the curve.")]
        public FloatReference Max = default;
        [Tooltip("Curve to evaluate in order to look up a final value to send as the parameter.\n" +
         "T=0 is when Variable == Min\n" +
         "T=1 is when Variable == Max")]
        public AnimationCurve Curve = default;

        public override void Start()
        {
            OnEventRaised();
        }

        public override void OnEventRaised()
        {
            if (Mixer != null && Variable != null && Min != null && Max != null)
            {
                float value = Mathf.Clamp01(Mathf.InverseLerp(Min.Value, Max.Value, Variable.Value));
                Mixer.SetFloat(ParameterName, value);
            }
        }
    }
}
