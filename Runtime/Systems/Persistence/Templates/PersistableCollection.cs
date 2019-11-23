using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    public class PersistableCollection<T> : PersistableSOAScriptableObject<Collection<T>>
        where T : SOArch_BaseScriptableObject
    {
        public int SelectedItemIndex = -1;
        // TODO: A better way than the following needs to be found - it may be possible to put 
        // something similar on the templates themselves. Investigation required.
        [XmlArray,
            XmlArrayItem(Type = typeof(PersistableFloatVariableCollection)),
            XmlArrayItem(Type = typeof(PersistableIntVariableCollection)),
            XmlArrayItem(Type = typeof(PersistableStringVariableCollection)),
            XmlArrayItem(Type = typeof(PersistableBoolVariableCollection)),
            XmlArrayItem(Type = typeof(PersistableCharacterCollection)),
            XmlArrayItem(Type = typeof(PersistableVariable<float>)),
            XmlArrayItem(Type = typeof(PersistableVariable<int>)),
            XmlArrayItem(Type = typeof(PersistableVariable<string>)),
            XmlArrayItem(Type = typeof(PersistableVariable<bool>)),
            XmlArrayItem(Type = typeof(PersistableCharacter))]
        public List<Persistable> CollectionItems;
        protected override void PopulateTemplateInternal()
        {
            if (_typedObject == null)
            {
                Debug.LogError($"{(nameof(PersistableCollection<T>))}.PopulateTemplateInternal: _typedObject is null.");
                return;
            }

            Name = _typedObject.name;
            UniqueId = _typedObject.PersistenceId.Value;
            TemplateType = typeof(PersistableCollection<T>).AssemblyQualifiedName;

            SelectedItemIndex = _typedObject.IsSelectedItemTracked ? _typedObject.SelectedItemIndex : -1;
            CollectionItems = new List<Persistable>();
            //Debug.Log($"{ConverterType}.PopulateTemplate: collection Items Count {_typedObject.Count}");
            for (int i = 0; i < _typedObject.Count; i++)
            {
                //CollectionItems.Add(_typedObject[i].ToPersistenceTemplate());
                if (_typedObject[i] == null)
                {
                    Debug.Log($"{TemplateType}.PopulateTemplate: Skipping null typed object.");
                    continue;
                }
                System.Type objType = _typedObject[i].GetType();
                //Debug.Log(objType);
                MethodInfo ToPersistenceTemplateMethod = typeof(PersistenceExtensions).GetMethod
                    ("ToPersistenceTemplate", BindingFlags.Public | BindingFlags.Static, null, new System.Type[] { objType }, null);
                if (ToPersistenceTemplateMethod != null)
                {
                    Persistable template = (Persistable)ToPersistenceTemplateMethod.Invoke(null, new object[] { _typedObject[i] });
                    if (template == null)
                    {
                        Debug.LogError("Template was null");
                        continue;
                    }
                    CollectionItems.Add(template);
                }
                else Debug.Log($"PersistableCollection.PopulateTemplate(Ext): " +
                    $"ToPersistenceTemplate info was null. {_typedObject.Type}");
            }
        }
        protected override void PopulateObjectInternal()
        {
            if (_typedObject == null)
            {
                Debug.LogError($"{(nameof(PersistableCollection<T>))}.PopulateObjectInternal: _typedObject is null.");
                return;
            }
            if (!PerformIdentityCheck()) return;
            if (!PerformTypeCheck()) return;
            //if (RestoreMissingChildObjects) Debug.Log($"{_typedObject.name}(PersistableCollection): RestoreMissingChildObjects=true");
            // Process sub items.
            for (int i = 0; i < CollectionItems.Count; i++)
            {
                // Attempt to find a matching child of the target object.
                SOArch_BaseScriptableObject result = _typedObject.Find(c => c.name == CollectionItems[i].Name);
                System.Type templateType = System.Type.GetType(CollectionItems[i].TemplateType);
                
                if (result == null && RestoreMissingChildObjects)
                {
                    result = ScriptableObject.CreateInstance<T>();
                    result.name = CollectionItems[i].Name;
                    result.PersistenceId.EnablePersistence();
                    _typedObject.Add(result);
                    //Debug.Log($"{_typedObject.name}(PersistableCollection): Restored missing object '{result.name}'");
                }

                if (result != null)
                {
                    System.Type objType = result.GetType();
                    //Debug.Log($"CollectionItems[{i}].Name = {CollectionItems[i].Name} DetectedType = {objType}");

                    MethodInfo FromPersistenceTemplateMethod = typeof(PersistenceExtensions).GetMethod
                        ("FromPersistenceTemplate", BindingFlags.Public | BindingFlags.Static, null, new System.Type[] { objType, templateType }, null);

                    // TODO: Add check for method directly on the class?
                    if (FromPersistenceTemplateMethod != null)
                    {
                        FromPersistenceTemplateMethod.Invoke(null, new object[] { result, CollectionItems[i] }); // working for types that don't need instantiating
                    }
                    else Debug.LogWarning($"{_typedObject.name}(PersistableCollection).PopulateObjectInternal: " +
                        $"FromPersistenceTemplate info was null. {_typedObject.Type}");
                }
                else
                {
                    Debug.LogWarning($"{_typedObject.name}(PersistableCollection).PopulateObjectInternal: " +
                        $"Unable to find matching child object and unable to create missing item.");
                }
            }
            _typedObject.SelectedItemIndex = _typedObject.IsSelectedItemTracked ? SelectedItemIndex : -1;
        }

        protected virtual bool PerformIdentityCheck()
        {
            bool matchedId = UniqueId == _typedObject.PersistenceId;
            bool matchedName = Name == _typedObject.name;
            // A match on UniqueId is preferred
            if (!matchedId)
            {
                Debug.LogWarning($"{(nameof(PersistableCollection<T>))}.PopulateObjectInternal: UniqueId mismatch, attempting match using Name.");
                if (!matchedName)
                {
                    Debug.LogError($"{(nameof(PersistableCollection<T>))}.PopulateObjectInternal: Error - Name mismatch.");
                    return false;
                }
            }
            return true;
        }
        protected virtual bool PerformTypeCheck()
        {
            if (TemplateType != typeof(PersistableCollection<T>).AssemblyQualifiedName)
            {
                Debug.LogError($"{nameof(PersistableCollection<T>)}.PerformTypeCheck: FAILED! - Template type mismatch.\n" +
                    $"Expected: {typeof(PersistableCollection<T>).AssemblyQualifiedName}\nGot        : {TemplateType}");
                return false;
            }
            return true;
        }
    }
}
