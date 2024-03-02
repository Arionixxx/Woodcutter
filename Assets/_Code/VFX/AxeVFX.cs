using System.Collections;
using System.Collections.Generic;
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

        public void PlayAxeHitTreeVFX()
        {
            _axeHitTree.transform.position = _pointForVFX.position;
            _axeHitTree.Play();
        }
    }
}
