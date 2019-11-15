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
                MethodInfo ToJTokenMethod = typeof(PersistenceExtensions).GetMethod
                    ("ToPersistenceTemplate", BindingFlags.Public | BindingFlags.Static, null, new System.Type[] { objType }, null);
                if (ToJTokenMethod != null)
                {
                    Persistable template = (Persistable)ToJTokenMethod.Invoke(null, new object[] { _typedObject[i] });
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
            // Sanity checks.
            if (_typedObject == null)
            {
                Debug.LogError($"{(nameof(PersistableCollection<T>))}.PopulateObjectInternal: _typedObject is null.");
                return;
            }
            if (Name != _typedObject.name)
            {
                Debug.LogError($"{(nameof(PersistableCollection<T>))}.PopulateObjectInternal: Error - Name mismatch.");
                return;
            }
            if (UniqueId != _typedObject.PersistenceId)
            {
                Debug.LogError($"{(nameof(PersistableCollection<T>))}.PopulateObjectInternal: Error - UniqueId mismatch.");
                return;
            }
            if (!PerformTypeCheck()) return;
            _typedObject.SelectedItemIndex = _typedObject.IsSelectedItemTracked ? SelectedItemIndex : -1;
            // Process sub items.
            for (int i = 0; i < CollectionItems.Count; i++)
            {
                // Attempt to find a matching child of the target object.
                SOArch_BaseScriptableObject result = _typedObject.Find(c => c.name == CollectionItems[i].Name);
                if (result != null)
                {
                    System.Type objType = result.GetType();
                    Debug.Log($"CollectionItems[{i}].Name = {CollectionItems[i].Name} DetectedType = {objType}");

                    System.Type templateType = System.Type.GetType(CollectionItems[i].TemplateType);

                    MethodInfo FromPersistenceTemplateMethod = typeof(PersistenceExtensions).GetMethod
                        ("FromPersistenceTemplate", BindingFlags.Public | BindingFlags.Static, null, new System.Type[] { objType, templateType }, null);

                    // TODO: Add check for method directly on the class?

                    if (FromPersistenceTemplateMethod != null)
                    {

                        FromPersistenceTemplateMethod.Invoke(null, new object[] { result, CollectionItems[i] }); // working for types that don't need instantiating

                    }
                    else Debug.LogWarning($"{_typedObject.name}(PersistableCollection).PopulateTemplate(Ext): " +
                        $"FromPersistenceTemplate info was null. {_typedObject.Type}");
                }
                else
                {
                    // No match - if the type can have an instance created, create one.
                    // This is primarily for characters and other transitive classes derived from ScriptableObject
                    // as the persistence templates for variables currently only restore the primary value.

                    // TODO!
                }
            }
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
