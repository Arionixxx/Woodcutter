using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trees
{
    public class LogsSpawner : MonoBehaviour
    {
        [SerializeField]
        private SingleLog _logPrefab;

        private static List<SingleLog> _logs = new List<SingleLog>();

        public static List<SingleLog> Logs => _logs;

        private int _logsCount;

        private void Start()
        {
            _logsCount = DataProvider.TreeSettings.LogsCount;
            InstantiateLogs();
        }

        public static void SpawnLogs(Vector3 position, Quaternion rotation)
        {
            for (int i = 0; i < _logs.Count; i++)
            {
                position.y *= i;
                _logs[i].Spawn(position, rotation);
            }
        }

        private void InstantiateLogs()
        {
            for (int i = 0; i < _logsCount; i++)
            {
                SingleLog log = Instantiate(_logPrefab, transform);
                log.gameObject.SetActive(false);
                _logs.Add(log);
            }
        }
    }
}
