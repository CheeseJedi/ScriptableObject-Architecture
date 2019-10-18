using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class CollectionChangedListenerComponent
        : SOArchitectureBaseMonobehaviour, IGameEventListener
    {
        [Header("Monitored Collection")]
        [SerializeField, Tooltip("The collection to monitor for changes.")]
        [EditorAssistant(typeof(BaseCollection), missingObjectWarning: true, 
            showCreateAssetButton: false, displayInspector: false)]
        protected BaseCollection Collection = default;
        [SerializeField, HideInInspector]
        private BaseCollection _previouslyRegisteredEvent = default;
        /// <summary>
        /// Notification of modification elsewhere via event.
        /// </summary>
        public abstract void OnEventRaised();
        protected virtual void OnEnable()
        {
            if (Collection != null)
                Register();
            OnEventRaised();
        }
        private void OnDisable()
        {
            if (Collection != null)
                Collection.RemoveListener(this);
        }
        private void Register()
        {
            if (_previouslyRegisteredEvent != null)
            {
                _previouslyRegisteredEvent.RemoveListener(this);
            }
            Collection.AddListener(this);
            _previouslyRegisteredEvent = Collection;
        }
    }
}