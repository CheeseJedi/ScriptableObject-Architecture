using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class CollectionChangedListenerSystem<TColl> //, TEvent>
        : ScriptableObjectSystem, IGameEventListener
        where TColl : BaseCollection
        //where TEvent : GameEventBase
    {
        //protected ScriptableObject GameEvent => Variable as TEvent;
        [Header("Monitored Collection")]
        [Tooltip("The collection to monitor for changes.")]
        [SerializeField]
        [EditorAssistant(typeof(BaseCollection), missingObjectWarning: true, showCreateAssetButton: false, displayInspector: false)]
        protected TColl Collection = default;
        [SerializeField, HideInInspector]
        private TColl _previouslyRegisteredEvent = default;
        /// <summary>
        /// Notification of modification elsewhere via event.
        /// </summary>
        public abstract void OnEventRaised();
        protected virtual void OnEnable()
        {
            if (Collection != null)
                Register();
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