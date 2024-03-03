using UnityEngine;

namespace Characters
{
    public class CharacterBase : MonoBehaviour
    {
        [SerializeField]
        protected CharacterAnimationEvents _characterAnimationEvents;

        [SerializeField]
        protected CharacterNavMeshMovement _characterNavMeshMovement;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (_characterAnimationEvents == null) _characterAnimationEvents = GetComponentInChildren<CharacterAnimationEvents>();

            if (_characterNavMeshMovement == null) TryGetComponent(out _characterNavMeshMovement);
        }
#endif
    }
}
