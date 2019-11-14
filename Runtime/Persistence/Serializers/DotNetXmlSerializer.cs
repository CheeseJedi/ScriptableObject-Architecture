using ScriptableObjectArchitecture;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Core.Serialization
{
    [CreateAssetMenu(
        fileName = "DotNetXmlSerializer.asset",
        menuName = SOArchitecture_Utility.SERIALIZER_SUBMENU + "DotNet Xml",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS)]
    public class DotNetXmlSerializer : AbstractSerializer
    {
        #region SOA Integration
        private const string DEFAULT_DEVELOPER_DESCRIPTION =
            "A serializer asset that implements persistence through .NET XML serialization.";
        protected override void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        #endregion
        public override SerializerFeatures Features => SerializerFeatures.Text_Format
            | SerializerFeatures.Supports_System_Object | SerializerFeatures.Supports_Polymophism;
        private const string DEFAULT_FILE_EXTENSION = ".xml";
        public override string DefaultFileExtension => DEFAULT_FILE_EXTENSION;
        [Header("Serializer Settings")]
        [Tooltip("Whether the formatter applies indenting.")]
        public Formatting formatting = Formatting.Indented;
        protected override T ReadStream<T>(Stream stream)
        {
            using (XmlTextReader xtr = new XmlTextReader(stream))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                return xs.Deserialize(xtr) as T;
            }
        }
        protected override void WriteStream<T>(Stream stream, T data)
        {
            using (XmlTextWriter xtw = new XmlTextWriter(stream, System.Text.Encoding.Default))
            {
                xtw.Formatting = formatting;
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(xtw, data);
            }
        }
    }
}
