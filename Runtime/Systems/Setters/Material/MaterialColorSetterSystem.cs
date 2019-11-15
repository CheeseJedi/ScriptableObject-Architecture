using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(fileName = "MaterialColorSetterSystem.asset",
        menuName = SOArchitecture_Utility.SYSTEMS_SUBMENU + "Material Color Setter",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS + 50)]
    public class MaterialColorSetterSystem : VariableChangedListenerSystem<ColorVariable>
    {
        #region SO-System Integration
        public static MaterialColorSetterSystem CreateAsset() => EditorAssistantUtility.CreateAsset<MaterialColorSetterSystem>();
        public override CallbackType CallbackOn => CallbackType.Start;
        private const string DEFAULT_DEVELOPER_DESCRIPTION = "Sets the albedo color on the specified material.";
        private void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        #endregion

        [Header("Material Color Setter")]
        [Tooltip("Material to set the named color parameter in.")]
        public Material Material = default;
        [Tooltip("Name of the parameter to set in the material.")]
        public string ParameterName = string.Empty;
        public override void Start()
        {
            OnEventRaised();
        }

        public override void OnEventRaised()
        {
            if (Material != null && Variable != null)
            {
                //Material.color = Variable.Value;
                Material.SetColor(ParameterName, Variable.Value);
            }
        }
    }
}
