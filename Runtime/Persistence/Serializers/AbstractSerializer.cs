using System;
using System.IO;
using System.Text;

namespace ScriptableObjectArchitecture
{
    public abstract class AbstractSerializer : SOArch_BaseScriptableObject
    {
        #region SOA Integration
        private const string DEFAULT_DEVELOPER_DESCRIPTION = 
            "Default description for a class derived from AbstractSerializer. Click to edit.";
        protected virtual void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        #endregion
        public abstract SerializerFeatures Features { get; }
        public abstract string DefaultFileExtension { get; }
        public virtual T Load<T>(string filename) where T : class
        {
            if (File.Exists(filename))
            {
                //try
                //{
                    return LoadStream<T>(filename);
                //}
                //catch (Exception e)
                //{
                //    Debug.LogException(e);
                //}
            }
            return default;
        }
        public virtual void Save<T>(string filename, T data) where T : class
        {
            //try
            //{
                SaveStream(filename, data);
            //}
            //catch (Exception e)
            //{
            //    Debug.LogException(e);
            //}
        }
        protected virtual T LoadStream<T>(string filename) where T : class
        {
            using(Stream stream = File.OpenRead(filename))
            {
                return ReadStream<T>(stream);
            }
        }
        protected virtual void SaveStream<T>(string filename, T data) where T : class
        {
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                WriteStream<T>(stream, data);
            }
        }
        protected abstract T ReadStream<T>(Stream stream) where T : class;
        protected abstract void WriteStream<T>(Stream stream, T data) where T : class;
        public string GetFeatureListText()
        {
            if (string.IsNullOrEmpty(_featureList))
            {
                _featureList = BuildFeatureList();
            }
            return _featureList;
        }
        protected string BuildFeatureList()
        {
            StringBuilder sb = new StringBuilder();
            const string FEATURE_LINE = "    {0}    {1}";
            const string CHECK_MARK = "✓";
            sb.Append("Supported Features:" + Environment.NewLine);
            foreach(SerializerFeatures value in Enum.GetValues(typeof(SerializerFeatures)))
            {
                if (value == SerializerFeatures.None) continue;
                string indicatorCharacter = ((Features & value) == value) ? CHECK_MARK : string.Empty;
                sb.Append(string.Format(FEATURE_LINE, value, indicatorCharacter) + Environment.NewLine);
            }
            sb.Append(Environment.NewLine + "Default File Extension:  " + DefaultFileExtension);
            return sb.ToString();
        }
        private string _featureList = string.Empty;
        [Flags]
        public enum SerializerFeatures
        {
            None = 0,
            Text_Format = 1,
            Binary_Format = 2,
            Supports_System_Object = 4,
            Supports_Unity_Object = 8,
            Supports_Polymophism = 16
        }
    }
}
