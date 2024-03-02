using UnityEngine;
using VFX;
using Tree = Trees.Tree;

namespace Collision
{
    public class AxeCollision : MonoBehaviour
    {
        [SerializeField]
        private AxeVFX _axeVFX;

        [SerializeField]
        private BoxCollider _boxCollider;

        public void TurnOffCollision()
        {
            _boxCollider.enabled = false;
        }

        public void TurnOnCollision()
        {
            _boxCollider.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Tree>(out Tree tree))
            {
                _axeVFX.PlayAxeHitTreeVFX();
                TurnOffCollision();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_axeVFX == null) TryGetComponent(out _axeVFX);

            if (_boxCollider == null) TryGetComponent(out _boxCollider);
        }
#endif
    }
}
