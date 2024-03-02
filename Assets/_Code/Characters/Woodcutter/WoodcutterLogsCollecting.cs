using System;
using System.Collections.Generic;
using Trees;
using UnityEngine;
using UpdateSys;

namespace Characters.Woodcutter
{
    public class WoodcutterLogsCollecting : MonoBehaviour, IUpdatable
    {
        [SerializeField]
        private WoodcutterAnimation _woodcutterAnimation;

        [SerializeField]
        private WoodcutterVFX _woodcutterVFX;

        [SerializeField]
        private Transform _finalPoint;

        private List<SingleLog> _flyingLogs;

        private readonly float _logSpeed = 2.5f;
        private readonly float _offset = 0.01f;

        public event Action OnLogsCollected;

        public void OnSystemUpdate(float deltaTime)
        {
            CollectingUpdate();
        }

        public void StartLogsCollecting()
        {
            _woodcutterAnimation.SwitchMagicCollecting(true);
            _woodcutterVFX.TurnOnHandsVFX();

            _flyingLogs = new List<SingleLog>(LogsSpawner.Logs);

            foreach (SingleLog log in _flyingLogs)
            {
                log.SwitchKinematic(true);
            }

            this.StartUpdate();
        }

        private void CollectingUpdate()
        {

            for (int i = 0; i < _flyingLogs.Count; i++)
            {
                Transform logTran = _flyingLogs[i].transform;

                if ((logTran.position - _finalPoint.position).sqrMagnitude > _offset)
                {
                    Vector3 finalPos = new Vector3(_finalPoint.position.x, _finalPoint.position.y + i, _finalPoint.position.z);

                    logTran.position = Vector3.MoveTowards(logTran.position,
                    finalPos, Time.deltaTime * _logSpeed);

                    logTran.rotation = Quaternion.RotateTowards(logTran.rotation,
                        _finalPoint.rotation, Time.deltaTime * _logSpeed);
                }
                else
                {
                    logTran.rotation = _finalPoint.rotation;
                    logTran.SetParent(_finalPoint);
                    _flyingLogs.RemoveAt(i);
                }
            }

            if (_flyingLogs.Count == 0)
            {
                this.StopUpdate();
                _woodcutterAnimation.SwitchMagicCollecting(false);
                _woodcutterVFX.TurnOffHandsVFX();
                OnLogsCollected?.Invoke();
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
