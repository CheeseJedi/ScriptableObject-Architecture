using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjectArchitecture
{
    public class BoolVariableFieldComponent : VariableChangedListenerComponent<BoolVariable>
    {
        [Header("UI Controls")]
        public Toggle Toggle; // Set via editor.
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
            Variable.Value = Toggle.isOn;
        }
        public void UpdateUI()
        {
            Toggle.isOn = Variable.Value;
        }
    }
}
