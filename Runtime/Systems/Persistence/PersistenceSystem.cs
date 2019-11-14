using System;
using System.IO;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [DefaultExecutionOrder(-25)]
    [CreateAssetMenu(fileName = "PersistenceSystem.asset",
        menuName = SOArchitecture_Utility.PERSISTENCE_SUBMENU + "Persistence System",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS + 50)]
    public class PersistenceSystem : Collection<SOArch_BaseScriptableObject>
    {
        #region SO-System Integration
        public static PersistenceSystem CreateAsset() 
            => EditorAssistantUtility.CreateAsset<PersistenceSystem>();
        public override CallbackType CallbackOn => CallbackType.Start | CallbackType.OnQuit;
        private const string DEFAULT_DEVELOPER_DESCRIPTION = "The Persistence System saves " +
            "and loads supported data types to and from storage via a plug-in serializer asset.";
        protected void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        public override bool IsPersistable => true;

        #endregion
        [Tooltip("The version number as written to the data file.")]
        public string versionValue = "0.0.0.1";
        [Tooltip("The Serializer Asset"), EditorAssistant(typeof(AbstractSerializer),
            missingObjectWarning: true, displayInspector: true)]
        public AbstractSerializer SerializerAsset = default;

        [Header("File System Path")]
        [Tooltip("The 'base' of the file name (not including the file extension).")]
        public string fileNameBase = "settings";
        [Tooltip("Advanced File System Settings.")]
        public PersistenceAdvancedFileSystemSettings advancedSettings
            = new PersistenceAdvancedFileSystemSettings();

        public string LastMessage { get; private set; } = string.Empty;
        private string _cachedPath = string.Empty;
        public override void Start() => Load();
        public override void OnQuit() => Save();
        [ContextMenu("Load")]
        public void Load()
        {
            string cachedPath = GetCachedPath(createMissingDirectories: true);
            LastMessage = string.Empty;
            if (!File.Exists(cachedPath))
            {
                Debug.Log($"{DateTime.UtcNow}: Creating new settings file {cachedPath}.{Environment.NewLine}");
                Save();
            }
            SerializerAsset.PersistenceLoad(this);
            LastMessage += $"{DateTime.UtcNow}: Load complete.";
        }
        [ContextMenu("Save")]
        public void Save()
        {
            SerializerAsset.PersistenceSave(this);
            LastMessage = $"{DateTime.UtcNow}: Save complete.";
        }
        public void NotifyOfEditorChange()
        {
            _cachedPath = GeneratePath();
        }
        //private void OnValidate()
        //{
        //    //_cachedPath = GeneratePath();
        //}
        public string GetCachedPath(bool createMissingDirectories)
        {
            if (string.IsNullOrEmpty(_cachedPath))
            {
                _cachedPath = GeneratePath();
            }
            if (createMissingDirectories)
            {
                FileInfo fileInfo = new FileInfo(_cachedPath);
                if (!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }
            }
            return _cachedPath;
        }
        private string GeneratePath()
        {
            string folderPath = Path.Combine(advancedSettings.useApplicationPath ?
                Application.persistentDataPath : advancedSettings.mainDirectory, advancedSettings.subDirectory);
            return Path.Combine(folderPath, fileNameBase + (advancedSettings.useDefaultFileExtension
                ? (SerializerAsset != null ? SerializerAsset.DefaultFileExtension
                : advancedSettings.fileNameExtension) : advancedSettings.fileNameExtension));
        }

        [System.Serializable]
        public class PersistenceAdvancedFileSystemSettings
        {
            [Tooltip("An optional subdirectory of the provided path.")]
            public string subDirectory = "Settings";
            [Tooltip("Whether to use the Application's PersistenData path, or the Main Directory field below.")]
            public bool useApplicationPath = true;
            [Tooltip("Unless using the Application Path, this is the path that will be used to read from and write to.")]
            public string mainDirectory = string.Empty;
            [Tooltip("Whether to use the serializers default file extension, or the File Name Extension field below.")]
            public bool useDefaultFileExtension = true;
            [Tooltip("A user specified file name extension (appended to the end of the file name).")]
            public string fileNameExtension = ".dat";
        }
    }
}
