using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjectArchitecture
{
    public class StringVariableFieldComponent : VariableChangedListenerComponent<StringVariable>
    {
        [Header("UI Controls")]
        [Tooltip("The Unity UI Text Input Field to update when the value changes")]
        public Text InputField = default; // Set via editor.
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
            Variable.Value = InputField.text;
        }
        private void UpdateUI()
        {
            if (InputField.text != Variable.Value)
            {
                InputField.text = Variable.Value;
            }
        }
    }
}
