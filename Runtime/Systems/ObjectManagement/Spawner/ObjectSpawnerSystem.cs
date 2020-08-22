using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(fileName = "ObjectSpawnerSystem.asset",
        menuName = SOArchitecture_Utility.SYSTEMS_SUBMENU + "Object Spawner",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_SYSTEMS + 50)]
    public class ObjectSpawnerSystem : Collection<SpawnerEndpointComponent>
    {
        #region SOA Integration
        public static ObjectSpawnerSystem CreateAsset() => EditorAssistantUtility.CreateAsset<ObjectSpawnerSystem>();
        public override CallbackType CallbackOn => CallbackType.OnAwake | CallbackType.Start | CallbackType.Update | CallbackType.OnGUI;
        private const string DEFAULT_DEVELOPER_DESCRIPTION = "The ObjectSpawnerSystem spawns an object specified in "
            + "a SpawnJob via a SpawnerEndpointComponent attached to a GameObject in a scene.";
        private void Awake()
        {
            if (DeveloperDescription == BASE_DEFAULT_DEVELOPER_DESCRIPTION)
                DeveloperDescription = new DeveloperDescription(DEFAULT_DEVELOPER_DESCRIPTION);
        }
        #endregion
        [Header("Debug")]
        public bool showDebugUI = false;
        public float borderSize = 40;

        private Queue<SpawnJob> _pendingSpawnJobs = new Queue<SpawnJob>();
        private List<SpawnJob> _allSpawnJobs = new List<SpawnJob>();
        private bool _isCoroutineRunning = false;
        private int _nextJobId = 0;

        public bool HasPendingJobs => _pendingSpawnJobs.Count > 0;
        public int NumPendingJobs => _pendingSpawnJobs.Count;

        public override void OnAwake()
        {
            debugStyle = new GUIStyle();
            debugStyle.normal.textColor = Color.magenta;
        }

        public override void Start()
        {
            _isCoroutineRunning = false;
            _pendingSpawnJobs.Clear();
            _allSpawnJobs.Clear();
            _nextJobId = 0;
        }

        public override void Update()
        {
            if (_pendingSpawnJobs.Count > 0 && !_isCoroutineRunning)
            {
                CallbackDistributor.HostMonoBehaviour.StartCoroutine(ProcessQueue());
            }
        }

        public SpawnerEndpointComponent FindSpawnerEndpointById(int id) => Find(s => s.EndpointId == id);

        public void EnqueueSpawnJob(SpawnJob job)
        {
            if (job == null)
            {
                Debug.LogError("SpawnerEndpoint.EnqueueSpawnJob: job was null.");
                return;
            }
            if (job.targetEndpointId == -1)
            {
                Debug.LogError("SpawnerEndpoint.EnqueueSpawnJob: targetEndpointId was invalid (-1).");
                return;
            }

            job.jobId = _nextJobId++;
            _allSpawnJobs.Add(job);
            _pendingSpawnJobs.Enqueue(job);
        }

        public void EnqueueSpawnJob(List<SpawnJob> jobs)
        {
            foreach (SpawnJob job in jobs)
            {
                EnqueueSpawnJob(job);
            }
        }

        private IEnumerator ProcessQueue()
        {
            _isCoroutineRunning = true;
            yield return null;
            Debug.Log($"{name}.ProcessQueue: Coroutine started.");

            while (HasPendingJobs)
            {
                Debug.Log($"{name}.ProcessQueue: has pending jobs.");

                SpawnJob currentJob = _pendingSpawnJobs.Dequeue();
                if (currentJob == null)
                {
                    Debug.LogError("ObjectSpawnerSystem.ProcessQueue: currentJob was null.");
                    _isCoroutineRunning = false;
                    yield break;
                }
                SpawnerEndpointComponent targetEndpoint = FindSpawnerEndpointById((int)currentJob.targetEndpointId);
                if (targetEndpoint == null)
                {
                    Debug.LogError($"ObjectSpawnerSystem.ProcessQueue: targetEndpoint with id {currentJob.targetEndpointId} for jobId {currentJob.jobId} was null.");
                    _isCoroutineRunning = false;
                    yield break;
                }
                targetEndpoint.Spawn(ref currentJob);
                yield return null;
            }
            _isCoroutineRunning = false;
        }

        public override void OnGUI()
        {
            if (showDebugUI)
            {
                if (debugStyle == null)
                {
                    debugStyle = new GUIStyle();
                    debugStyle.normal.textColor = Color.magenta;
                }
                DrawUI();
            }
        }

        const string k_debugUITitle = "ObjectSpawner Debug";
        const string k_isCoroutineRunningTitle = "IsCoroutineRunning: ";
        const string k_nextJobIdTitle = "NextJobId: ";
        GUIStyle debugStyle;

        private void DrawUI()
        {
            GUILayout.BeginArea(new Rect(borderSize, borderSize,
                Screen.width - (borderSize * 2), Screen.height - (borderSize * 2)));

            GUILayout.FlexibleSpace();

            GUILayout.Label($"{k_debugUITitle} ({name})", debugStyle);

            GUILayout.Label(k_isCoroutineRunningTitle + _isCoroutineRunning, debugStyle);
            GUILayout.Label(k_nextJobIdTitle + _nextJobId, debugStyle);

            GUILayout.Label($"Pending Job Queue ({_pendingSpawnJobs.Count})", debugStyle);
            foreach (SpawnJob job in _pendingSpawnJobs)
            {
                if (job == null)
                {
                    GUILayout.Label($"null spawn job!", debugStyle);
                    return;
                }
                GUILayout.Label($"JobId: {job.jobId} EndpointId: {job.targetEndpointId} JobCompleted: {job.jobCompleted}", debugStyle);
            }
            GUILayout.Label($"All Spawn Jobs ({_allSpawnJobs.Count})", debugStyle);
            GUILayout.EndArea();
        }
    }
}
