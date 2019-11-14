using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjectArchitecture
{
    public class IntVariableSliderComponent : VariableChangedListenerComponent<IntVariable>
    {
        [Header("UI Controls")]
        [Tooltip("The slider to set when the value changes")]
        public Slider Slider = default; // Set via editor.
        [Tooltip("The TextMeshPro Text to update when the value changes")]
        public Text Text = default; // Set via editor.
        private void Start()
        {
            OnEventRaised();
        }
        /// <summary>
        /// Notification of modification elsewhere via event.
        /// </summary>
        public override void OnEventRaised()
        {
            UpdateUI();
        }
        /// <summary>
        /// Notification of modification via UI.
        /// </summary>
        public void NotifyOfControlChange()
        {
            // Adjust the variable's value.
            Variable.Value = (int)Slider.value;
        }
        private void UpdateUI()
        {
            Slider.value = Variable;
            if (Text != null)
            {
                Text.text = Variable.ToString();
            }
        }
    }
}
