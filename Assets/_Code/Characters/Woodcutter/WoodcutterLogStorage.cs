using FixedPoints;
using System;
using System.Collections;
using System.Collections.Generic;
using Trees;
using UnityEngine;
using UpdateSys;

namespace Characters.Woodcutter
{
    public class WoodcutterLogStorage : MonoBehaviour, IUpdatable
    {
        [SerializeField]
        private WoodcutterAnimation _woodcutterAnimation;

        [SerializeField]
        private WoodcutterVFX _woodcutterVFX;

        private readonly float _offset = 0.01f;
        private readonly float _logSpeed = 2.5f;

        private List<SingleLog> _logs;
        private Vector3 _storagePoint;

        public event Action OnLogStored;

        private void Start()
        {
            _storagePoint = StoragePoint.Position;
        }

        public void OnSystemUpdate(float deltaTime)
        {
            MoveLogsToTheStorage();
        }

        public void Store()
        {
            _logs = new List<SingleLog>(LogsSpawner.Logs);

            foreach (SingleLog log in _logs)
            {
                log.transform.SetParent(null);
            }

            _woodcutterVFX.TurnOnHandsVFX();
            _woodcutterAnimation.SwitchMagicCollecting(true);
            this.StartUpdate();
        }

        private void MoveLogsToTheStorage()
        {
            for (int i = 0; i < _logs.Count; i++)
            {
                Transform logTran = _logs[i].transform;

                if ((logTran.position - _storagePoint).sqrMagnitude > _offset)
                {
                    logTran.position = Vector3.MoveTowards(logTran.position, _storagePoint, Time.deltaTime * _logSpeed / (i+1));
                }
                else
                {
                    _logs[i].SwitchKinematic(false);
                    _logs.RemoveAt(i);
                }
            }
            if (_logs.Count == 0)
            {
                _woodcutterVFX.TurnOffHandsVFX();
                _woodcutterAnimation.SwitchMagicCollecting(false);
                OnLogStored?.Invoke();
                this.StopUpdate();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_woodcutterAnimation == null) TryGetComponent(out _woodcutterAnimation);

            if (_woodcutterVFX == null) TryGetComponent(out _woodcutterVFX);
        }
#endif
    }
}
