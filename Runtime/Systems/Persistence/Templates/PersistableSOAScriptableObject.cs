using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    public class PersistableSOAScriptableObject<T> : Persistable
        where T : SOArch_BaseScriptableObject
    {
        [System.NonSerialized]
        protected T _typedObject;
        public void PopulateTemplate(T source)
        {
            if (source == null)
            {
                Debug.LogError(nameof(PersistableSOAScriptableObject<T>) + ".PopulateTemplate(object): source object is null.");
                return;
            }
            _typedObject = source;
            _untypedObject = source;
            PopulateTemplateInternal();
        }
        //public override void PopulateObject(object target)
        //{
        //    if (target == null)
        //    {
        //        Debug.LogError(nameof(PersistableSOAScriptableObject<T>) + ".PopulateObject(object): target object is null.");
        //        return;
        //    }
        //    _typedObject = (T)target;
        //    PopulateObjectInternal();
        //}
        public void PopulateObject(T target)
        {
            if (target == null)
            {
                Debug.LogError(nameof(PersistableSOAScriptableObject<T>) + ".PopulateObject(object): target object is null.");
                return;
            }
            _typedObject = target;
            _untypedObject = target;
            PopulateObjectInternal();
        }
    }
}
