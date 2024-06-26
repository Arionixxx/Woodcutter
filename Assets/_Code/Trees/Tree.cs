using Data;
using System;
using UnityEngine;
using UpdateSys;
using Random = UnityEngine.Random;

namespace Trees
{
    public class Tree : MonoBehaviour, IUpdatable
    {
        [SerializeField]
        private CapsuleCollider _collider;

        [SerializeField]
        private TreeVFX _VFX;

        [SerializeField]
        private Rigidbody _rb;

        [SerializeField]
        private Transform _logsSpawnPoint;

        [SerializeField]
        private GameObject[] _visualVariants;

        private GameObject _choosedVisual;

        private readonly float _randomRange = 15f;
        private float _treeFallDelay;
        private float _timer;

        public event Action OnLogsSpawn;

        private void Start()
        {
            _treeFallDelay = DataProvider.TreeSettings.TreeFallDelay;
        }

        public void OnSystemUpdate(float deltaTime)
        {
            TimerUpdate(deltaTime);
        }

        public void Spawn(Vector3 spawnPos)
        {
            TreesCollector.AddTree(this);

            transform.position = spawnPos;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            _collider.enabled = true;
            _rb.isKinematic = true;

            foreach (GameObject variant in _visualVariants)
            {
                variant.SetActive(false);
            }

            _choosedVisual = _visualVariants[Random.Range(0, _visualVariants.Length)];
            _choosedVisual.SetActive(true);
        }

        public void CutDown()
        {
            _rb.isKinematic = false;

            float[] v = new float[3];

            for (int i = 0; i < v.Length; i++)
            {
                v[i] = Random.Range(-_randomRange, _randomRange);
            }

            Vector3 forceVector = new Vector3(v[0], v[1], v[2]);
            _rb.AddForce(forceVector);

            _timer = _treeFallDelay;
            this.StartUpdate();
        }

        private void HideTreeAndSpawnLogs()
        {
            _choosedVisual.SetActive(false);
            _collider.enabled = false;
            _rb.isKinematic = true;

            //convert to 3 parts
            LogsSpawner.SpawnLogs(_logsSpawnPoint.position, _logsSpawnPoint.rotation);
            _VFX.PlayPuffVFX();
            OnLogsSpawn?.Invoke();
        }

        private void TimerUpdate(float deltaTime)
        {
            _timer -= deltaTime;

            if (_timer <= 0)
            {
                HideTreeAndSpawnLogs();
                this.StopUpdate();
            }
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_collider == null) TryGetComponent(out _collider);

            if (_rb == null) TryGetComponent(out _rb);

            if (_VFX == null) TryGetComponent(out _VFX);
        }
#endif
    }
}
