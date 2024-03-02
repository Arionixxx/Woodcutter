using UnityEngine;

namespace Trees
{
    public class TreeVFX : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _puffVFX;

        public void PlayPuffVFX()
        {
            _puffVFX.transform.up = Vector3.up;
            _puffVFX.Play();
        }
    }
}
