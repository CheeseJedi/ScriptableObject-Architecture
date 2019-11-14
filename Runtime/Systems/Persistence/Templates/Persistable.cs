using System.Xml.Serialization;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    public class Persistable
    {
        #region Template Fields
        public string Name;
        public string UniqueId;
        public string TemplateType;
        #endregion
        [System.NonSerialized]
        protected object _untypedObject; // The object to be persisted.
        public virtual void PopulateTemplate(object source)
        {
            Debug.LogWarning(nameof(Persistable) + ".PopulateTemplate(object): Probably shouldn't be here!");
            if (source == null)
            {
                Debug.LogError(nameof(Persistable) + ".PopulateTemplate(object): source object is null.");
                return;
            }
            _untypedObject = source;
            //PopulateTemplateInternal();
        }
        public virtual void PopulateObject(object target)
        {
            Debug.LogWarning(nameof(Persistable) + ".PopulateObject(object): Probably shouldn't be here!");
            if (target == null)
            {
                Debug.LogError(nameof(Persistable) + ".PopulateObject(object): target object is null.");
                return;
            }
            _untypedObject = target;
            //PopulateObjectInternal();
        }
        protected virtual void PopulateTemplateInternal() => throw new System.NotImplementedException();
        protected virtual void PopulateObjectInternal() => throw new System.NotImplementedException();
    }
}
