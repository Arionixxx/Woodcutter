using UnityEngine;

namespace Characters.Woodcutter
{
    public class WoodcutterVFX : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem[] _handsVFX;

        public void TurnOnHandsVFX()
        {
            foreach(ParticleSystem particle in _handsVFX)
            {
                particle.Play();
            }
        }

        public void TurnOffHandsVFX()
        {
            foreach (ParticleSystem particle in _handsVFX)
            {
                particle.Stop();
            }
        }
    }
}
