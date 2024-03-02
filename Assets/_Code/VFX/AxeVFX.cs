using UnityEngine;

namespace VFX
{
    public class AxeVFX : MonoBehaviour
    {
        [SerializeField]
        private Transform _pointForVFX;

        [SerializeField]
        private ParticleSystem _axeHitTreePrefab;

        private ParticleSystem _axeHitTree;

        private void Awake()
        {
            _axeHitTree = Instantiate(_axeHitTreePrefab);
        }

        public void PlayAxeHitTreeVFX(Vector3 hitObjectPos)
        {
            _axeHitTree.transform.position = new Vector3(hitObjectPos.x,
                _pointForVFX.position.y, hitObjectPos.z);
            _axeHitTree.Play();
        }
    }
}
