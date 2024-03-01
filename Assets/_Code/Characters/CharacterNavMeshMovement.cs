using Data;
using System;
using UnityEngine;
using UnityEngine.AI;
using UpdateSys;

namespace Characters
{
    public class CharacterNavMeshMovement : MonoBehaviour, IUpdatable
    {
        [SerializeField]
        private NavMeshAgent _agent;

        [SerializeField]
        private CharacterAnimation _characterAnimation;

        private readonly float _reachPointOffset = 0.01f;

        private float _maxMoveSpeed = 4f;
        private float _rotationSpeed = 10f;

        private Vector3 _targetPosition;

        private Vector3 _targetRotation;

        private Action _onReachPointAction;

        private void Start()
        {
            InitializeSpeedAndRotation();
        }


        public void OnSystemUpdate(float deltaTime)
        {
            NavmeshMovementUpdate();
            CheckIsPointReached();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartMovement(new Vector3(10, 0, 10), new NavmeshMovementData(() =>
                { Debug.Log("o!");}
                ));
            }
        }

        public void StartMovement(Vector3 position, NavmeshMovementData movementData)
        {
            _targetPosition = position;
            _agent.enabled = true;
            _agent.destination = _targetPosition;
            SetupMovementData(movementData);
            this.StartUpdate();
        }

        private void StopMovement()
        {
            _agent.enabled = false;
            _characterAnimation.ChangeMovingSpeed(0, _maxMoveSpeed);
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

        private void NavmeshMovementUpdate()
        {
            _characterAnimation.ChangeMovingSpeed(_agent.velocity.sqrMagnitude / _maxMoveSpeed, _maxMoveSpeed);
        }

        private void InitializeSpeedAndRotation()
        {
            _agent.speed = _maxMoveSpeed;//_moveSpeed = _settings.NavmeshMoveSpeed;
            _agent.angularSpeed = _rotationSpeed;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_agent == null) TryGetComponent (out _agent);

            if (_characterAnimation == null) TryGetComponent(out _characterAnimation);
        }
#endif
    }
}
