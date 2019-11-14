using ScriptableObjectArchitecture;
using System.IO;
using UnityEngine;

namespace Core.Serialization
{
    [CreateAssetMenu(
        fileName = "UnityJsonSerializer.asset",
        menuName = SOArchitecture_Utility.SERIALIZER_SUBMENU + "Unity Json",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS)]
    public class UnityJsonSerializer : AbstractSerializer
    {
        #region SOA Integration
        private const string DEFAULT_DEVELOPER_DESCRIPTION =
            "A serializer asset that implements persistence through Unity's Json serialization.";
        protected override void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        #endregion
        public override SerializerFeatures Features => SerializerFeatures.Text_Format
            | SerializerFeatures.Supports_System_Object | SerializerFeatures.Supports_Unity_Object;
        private const string DEFAULT_FILE_EXTENSION = ".json";
        public override string DefaultFileExtension => DEFAULT_FILE_EXTENSION;
        [Header("Serializer Settings")]
        [Tooltip("Whether the formatter applies indenting.")]
        public bool prettyPrint = true;
        protected override T ReadStream<T>(Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                return JsonUtility.FromJson<T>(sr.ReadToEnd()) as T;
            }
        }
        protected override void WriteStream<T>(Stream stream, T data)
        {
            using (StreamWriter sw = new StreamWriter(stream))
            {
                sw.Write(JsonUtility.ToJson(data, prettyPrint));
            }
        }
    }
}
