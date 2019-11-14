using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    public class SpawnJob
    {
        public int jobId = -1;
        public int targetEndpointId = -1;
        public System.Action<int> callback = null;
        public bool jobCompleted = false;
        public GameObject prefab = default;
        public Vector3 position = Vector3.zero;
        public Quaternion rotation = Quaternion.identity;
        public Transform parent = null;
        public GameObject spawnedObject = default;

        public SpawnJob()
        {

        }
    }
}
