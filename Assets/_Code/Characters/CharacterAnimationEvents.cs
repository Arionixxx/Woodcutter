using System;
using UnityEngine;

namespace Characters
{
    public class CharacterAnimationEvents : MonoBehaviour
    {
        public event Action OnAxeSlashEnded;
        public event Action OnAxeSlashMiddle;

        private void AxeSlashEnd_AnimEvent()
        {
            OnAxeSlashEnded?.Invoke();
        }

        private void AxeSlashMiddle_AnimEvent()
        {
            OnAxeSlashMiddle?.Invoke();
        }

    }
}
