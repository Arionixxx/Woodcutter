using System;
using UnityEngine;

namespace Characters
{
    public class CharacterAnimationEvents : MonoBehaviour
    {
        public event Action OnAxeSlashEnded;
        public event Action OnAxeSlashMiddle;
        public event Action OnAxeColliderTurningOn;
        public event Action OnAxeColliderTurningOff;

        private void AxeSlashEnd_AnimEvent()
        {
            OnAxeSlashEnded?.Invoke();
        }

        private void AxeSlashMiddle_AnimEvent()
        {
            OnAxeSlashMiddle?.Invoke();
        }

        private void TurnOnAxeCollider_AnimEvent()
        {
            OnAxeColliderTurningOn?.Invoke();
        }

        private void TurnOffAxeCollider_AnimEvent()
        {
            OnAxeColliderTurningOff?.Invoke();
        }

    }
}
