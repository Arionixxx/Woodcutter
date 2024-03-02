using Characters.Woodcutter;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Trees
{
    public class TreesSpawner : MonoBehaviour
    {
        [SerializeField]
        private Tree _treePrefab;

        private List<Tree> _treesPool = new List<Tree>();

        private static List<TreeSpawnPoint> _staticSpawnPoints = new List<TreeSpawnPoint>();

        private int _minSpawnCount = 1;
        private int _maxSpawnCount = 5;//take from script obj

        private static int _spawnPointsCount;

        public static event Action OnTreesSpawn;

        private void Start()
        {
            InstantiateTrees();
            SpawnTreesFromPool();
            SubscribeOnEvents();
        }

        public static void AddSpawnPoint(TreeSpawnPoint treeSpawnPoint)
        {
            if (!_staticSpawnPoints.Contains(treeSpawnPoint))
            {
                _staticSpawnPoints.Add(treeSpawnPoint);
            }
            RecalculateSpawnPointsCount();
        }

        private void SubscribeOnEvents()
        {
            SpawnMoreTreesButton.OnSpawnButtonClick += SpawnTreesFromPool;
        }

        private static void RecalculateSpawnPointsCount()
        {
            _spawnPointsCount = _staticSpawnPoints.Count;
        }

        private void InstantiateTrees()
        {
            int count = _staticSpawnPoints.Count;

            for (int i = 0; i < count; i++)
            {
                Tree tree = Instantiate(_treePrefab, transform);
                tree.gameObject.SetActive(false);
                _treesPool.Add(tree);
            }
        }

        private void SpawnTreesFromPool()
        {
            if (_maxSpawnCount > _spawnPointsCount)
            {
                _maxSpawnCount = _spawnPointsCount;
            }

            if (_minSpawnCount > _spawnPointsCount)
            {
                _minSpawnCount = _spawnPointsCount;
            }

            int randomCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

            List<TreeSpawnPoint> tempDuplicate = new List<TreeSpawnPoint>(_staticSpawnPoints);
            List<TreeSpawnPoint> randomPoints = new List<TreeSpawnPoint>();

            while (randomCount > 0)
            {
                randomCount--;
                int randIndex = Random.Range(0, tempDuplicate.Count);
                randomPoints.Add(tempDuplicate[randIndex]);
                tempDuplicate.RemoveAt(randIndex);
            }

            for (int i = 0; i < randomPoints.Count; i++)
            {
                Tree tree = _treesPool[i];
                tree.gameObject.SetActive(true);
                tree.Spawn(randomPoints[i].transform.position);
            }

            OnTreesSpawn?.Invoke();
        }
    }
}
