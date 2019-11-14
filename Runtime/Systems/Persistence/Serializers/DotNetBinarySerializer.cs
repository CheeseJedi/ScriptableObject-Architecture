using ScriptableObjectArchitecture;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Core.Serialization
{
    [CreateAssetMenu(
        fileName = "DotNetBinarySerializer.asset",
        menuName = SOArchitecture_Utility.SERIALIZER_SUBMENU + "DotNet Binary",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS)]
    public class DotNetBinarySerializer : AbstractSerializer
    {
        #region SOA Integration
        private const string DEFAULT_DEVELOPER_DESCRIPTION =
            "A serializer asset that implements persistence through .NET Binary serialization.";
        protected override void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        #endregion
        public override SerializerFeatures Features => SerializerFeatures.Binary_Format |
            SerializerFeatures.Supports_System_Object | SerializerFeatures.Supports_Polymophism;
        private const string DEFAULT_FILE_EXTENSION = ".bin";
        public override string DefaultFileExtension => DEFAULT_FILE_EXTENSION;
        protected override T ReadStream<T>(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream) as T;
        }
        protected override void WriteStream<T>(Stream stream, T data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }
}
