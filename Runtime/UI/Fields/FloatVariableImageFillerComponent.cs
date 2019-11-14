using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjectArchitecture
{
    /// <summary>
    /// Sets an Image component's fill amount to represent how far Variable is between Min and Max.
    /// </summary>
    public class FloatVariableImageFillerComponent : VariableChangedListenerComponent<FloatVariable>
    {
        [Header("UI Controls")]
        public Image Image = default;
        public FloatReference Min = default;
        public FloatReference Max = default;
        private void Start()
        {
            OnEventRaised();
        }
        override public void OnEventRaised()
        {
            if (Image != null && Variable != null && Min != null && Max != null)
            {
                Image.fillAmount = Mathf.Clamp01(
                    Mathf.InverseLerp(Min.Value, Max.Value, Variable.Value));
            }
        }
    }
}
