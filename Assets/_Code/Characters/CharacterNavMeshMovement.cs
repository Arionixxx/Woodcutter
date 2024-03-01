using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UpdateSys;

namespace Characters
{
    public class CharacterNavMeshMovement : MonoBehaviour, IUpdatable
    {
        [SerializeField]
        private NavMeshAgent _agent;

        private readonly float _reachPointOffset = 0.1f;

        private Vector3 _targetPosition;

        private Vector3 _targetRotation;

        private Action _onReachPointAction;

        public void OnSystemUpdate(float deltaTime)
        {
            CheckIsPointReached();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartMovement(new Vector3(10, 0, 10), new NavmeshMovementData());
            }
        }

        public void StartMovement(Vector3 position, NavmeshMovementData movementData)
        {
            _targetPosition = position;
            _agent.destination = _targetPosition;
            SetupMovementData(movementData);
            this.StartUpdate();
        }

        private void StopMovement()
        {
            AlignCharacterByRotationAfterStop();
        }

        private void SetupMovementData(NavmeshMovementData movementData)
        {
            _targetRotation = movementData.RotationInReachedPoint;
            _onReachPointAction = movementData.OnReachedPointEvent;
        }

        private void CheckIsPointReached()
        {
            if ((transform.position - _targetPosition).sqrMagnitude < _reachPointOffset)
            {
                StopMovement();
                _onReachPointAction?.Invoke();
                this.StopUpdate();
            }
        }

        private void AlignCharacterByRotationAfterStop()
        {
            if (_targetRotation == Vector3.zero)
            {
                Vector3 target = new Vector3(_targetPosition.x, 0, _targetPosition.z);
                _targetRotation = (target - transform.position).normalized;
            }

            AlignRotation();
        }

        private void AlignRotation()
        {
            transform.forward = _targetRotation;
            Vector3 euler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, euler.y, 0);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_agent == null) TryGetComponent (out _agent);
        }
#endif
    }
}
