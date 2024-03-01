using UnityEngine;

namespace Characters
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        private readonly float _moveSpeedForFullRunAnimation = 8f;

        private int _isMovingHash;
        private int _movingBlendHash;
        private int _moveSpeedMultiplierHash;

        private void Awake()
        {
            Initialize();
        }

        public void ChangeMovingSpeed(float currentMovementSpeed, float maxMovementSpeed)
        {
            float smoothnessMultiplier = currentMovementSpeed / maxMovementSpeed;
            _animator.SetFloat(_moveSpeedMultiplierHash, smoothnessMultiplier);

            float blendingCoeff = currentMovementSpeed / _moveSpeedForFullRunAnimation;
            _animator.SetFloat(_movingBlendHash, blendingCoeff);

            SetMoving(currentMovementSpeed > 0);
        }

        private void SetMoving(bool value)
        {
            _animator.SetBool(_isMovingHash, value);
        }

        private void Initialize()
        {
            _isMovingHash = Animator.StringToHash("IsMoving");
            _movingBlendHash = Animator.StringToHash("MovingBlend");
            _moveSpeedMultiplierHash = Animator.StringToHash("MoveSpeedMultiplier");
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_animator == null) _animator = GetComponentInChildren<Animator>();
        }
#endif
    }
}
