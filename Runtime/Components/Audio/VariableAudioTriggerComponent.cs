using ScriptableObjectArchitecture;
using UnityEngine;

namespace Core.Audio
{
    public class VariableAudioTriggerComponent : VariableChangedListenerComponent<FloatVariable>
    {
        [Header("Variable Audio Trigger")]
        [Tooltip("The AudioSource to control playback on")]
        public AudioSource AudioSource;

        [Tooltip("The low threshold - when crossed the audio source stops")]
        public FloatReference LowThreshold;

        public override void OnEventRaised()
        {
            if (AudioSource != null && Variable != null && LowThreshold != null)
            {
                if (Variable.Value < LowThreshold.Value)
                {
                    if (!AudioSource.isPlaying)
                    {
                        AudioSource.Play();
                    }
                }
                else
                {
                    if (AudioSource.isPlaying)
                    {
                        AudioSource.Stop();
                    }
                }
            }
        }
    }
}