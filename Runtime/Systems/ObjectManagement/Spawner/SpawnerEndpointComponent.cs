using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class SpawnerEndpointComponent : SOArch_BaseMonoBehaviour
    {
        [Header("Object Spawner System Endpoint")]
        [SerializeField, Tooltip("The ObjectSpawnerSystem this endpoint will register with.")]
        private ObjectSpawnerSystem _spawnerSystem = default;

        [SerializeField, Tooltip("Spawner Endpoint Id - must be unique")]
        private int _endpointId;
        public int EndpointId
        {
            get => _endpointId;
            set => _endpointId = value;
        }

        //private List<GameObject> _spawnedObjects = new List<GameObject>();

        private void OnEnable()
        {
            _spawnerSystem.Add(this);
        }
        private void OnDisable()
        {
            _spawnerSystem.Remove(this);
        }

        public GameObject Spawn(ref SpawnJob job)
        {
            if (job == null)
            {
                Debug.LogError("SpawnerEndpoint.Spawn: job was null.");
                return null;
            }
            if (gameObject == null)
            {
                Debug.LogError("SpawnerEndpoint.Spawn: gameObject was null.");
                return null;
            }

            if (job.position == null) job.position = Vector3.zero;
            if (job.rotation == null) job.rotation = Quaternion.identity;
            if (job.parent == null) job.parent = transform;

            job.spawnedObject = Instantiate(job.prefab, job.position, job.rotation, job.parent);
            //_spawnedObjects.Add(job.spawnedObject);
            job.jobCompleted = true;
            job.callback?.Invoke(job.jobId);
            return job.spawnedObject;
        }
    }
}
