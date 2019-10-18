using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class VariableChangedListenerSystem<TVar>
        : ScriptableObjectSystem, IGameEventListener
        where TVar : BaseVariable
    {
        [Header("Monitored Variable")]
        [Tooltip("The variable to monitor for changes.")]
        [SerializeField]
        [EditorAssistant(typeof(BaseVariable), missingObjectWarning: true, 
            showCreateAssetButton: false, displayInspector: true)]
        protected TVar Variable = default;
        [SerializeField, HideInInspector]
        private TVar _previouslyRegisteredEvent = default;
        /// <summary>
        /// Called by the variable when it changes.
        /// </summary>
        public abstract void OnEventRaised();
        protected virtual void OnEnable()
        {
            if (Variable != null)
                Register();
        }
        private void OnDisable()
        {
            if (Variable != null)
                Variable.RemoveListener(this);
        }
        private void Register()
        {
            if (_previouslyRegisteredEvent != null)
            {
                _previouslyRegisteredEvent.RemoveListener(this);
            }
            Variable.AddListener(this);
            _previouslyRegisteredEvent = Variable;
        }
    }
}