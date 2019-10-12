using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "VariableResetter.asset",
        menuName = SOArchitecture_Utility.SYSTEMS_SUBMENU + "Variable Resetter",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS + 0)]
    public class VariableResetterSystem : ScriptableObjectSystem
    {
        public static VariableResetterSystem CreateAsset() =>
            EditorAssistantUtility.CreateAsset<VariableResetterSystem>();
        public override CallbackType CallbackOn => _CallbackOn;
        [SerializeField]
        private CallbackType _CallbackOn = CallbackType.OnAwake | CallbackType.Start | CallbackType.OnQuit;
        private const string DEFAULT_DEVELOPER_DESCRIPTION = "Variables added to this system will be reset to "
            + "their configured default value when one of the configured callbacks is called.";
        private void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        [Header("Variables to Reset")]
        [Tooltip("Variables added to this list will be reset to their configured default value.")]
        public List<BaseVariable> VariablesToReset = default;
        public override void Start()
        {
            ResetVariables();
        }
        public override void OnAwake()
        {
            ResetVariables();
        }
        public override void OnQuit()
        {
            ResetVariables();
        }
        public void ResetVariables()
        {
            // Reset any variables in the reset list.
            for (int i = 0; i < VariablesToReset.Count; i++)
            {
                VariablesToReset[i].ResetToDefaultValue();
            }
        }
    }
}
