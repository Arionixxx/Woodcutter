using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "TreeSettings", menuName = "Tree/Create Tree Settings", order = 2)]
    public class TreeSettings : ScriptableObject
    {
        [SerializeField]
        private int _logsCount;

        [SerializeField]
        private float _treeFallDelay;

        [SerializeField]
        private int _minSpawnCount;

        [SerializeField]
        private int _maxSpawnCount;

        public int LogsCount => _logsCount;

        public float TreeFallDelay => _treeFallDelay;

        public int MinSpawnCount => _minSpawnCount;

        public int MaxSpawnCount => _maxSpawnCount;
    }
}
