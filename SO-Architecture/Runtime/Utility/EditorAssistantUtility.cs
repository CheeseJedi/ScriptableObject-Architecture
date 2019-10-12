using UnityEngine;
using UnityEditor;

namespace ScriptableObjectArchitecture
{
    public static class EditorAssistantUtility
    {
        /// <summary>
        /// Create new asset from <see cref="ScriptableObject"/> type with unique name at
        /// selected folder in project window. Asset creation can be cancelled by pressing
        /// escape key when asset is initially being named.
        /// </summary>
        /// <typeparam name="T">Type of scriptable object.</typeparam>
        /// <returns>Returns a reference to the newly created asset.</returns>
        public static T CreateAsset<T>() where T : ScriptableObject
        {
            /// Based on the solution at http://wiki.unity3d.com/index.php/CreateScriptableObjectAsset2
            /// Modified to return a reference to the new asset.
            T asset = ScriptableObject.CreateInstance<T>();
            ProjectWindowUtil.CreateAsset(asset, "New " + typeof(T).Name + ".asset");
            return asset;
        }
    }
}
