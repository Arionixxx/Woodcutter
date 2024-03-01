using UnityEngine;
using UpdateSys;

namespace Trees
{
    public class Tree : MonoBehaviour, IUpdatable
    {
        [SerializeField]
        private CapsuleCollider _collider;

        [SerializeField]
        private Rigidbody _rb;

        [SerializeField]
        private GameObject[] _visualVariants;

        private GameObject _choosedVisual;

        private readonly float _randomRange = 5f;
        private readonly float _treeFallDelay = 5f;
        private float _timer;

        private void Start()
        {
            Spawn();
        }

        public void OnSystemUpdate(float deltaTime)
        {
            TimerUpdate(deltaTime);
        }

        public void Spawn()
        {
            _collider.enabled = true;
            _rb.isKinematic = true;

            foreach (GameObject variant in _visualVariants)
            {
                variant.SetActive(false);
            }

            _choosedVisual = _visualVariants[Random.Range(0, _visualVariants.Length)];
            _choosedVisual.SetActive(true);
        }

        public void CutDown()
        {
            _rb.isKinematic = false;

            float[] v = new float[3];

            for (int i = 0; i < v.Length; i++)
            {
                v[i] = Random.Range(-_randomRange, _randomRange);
            }

            _rb.AddForce(v[0], v[1], v[2]);

            _timer = _treeFallDelay;
            this.StartUpdate();
        }

        private void HideTreeAndSpawnLogs()
        {
            _choosedVisual.SetActive(false);
            _collider.enabled = false;

            //convert to 3 parts
        }

        private void TimerUpdate(float deltaTime)
        {
            _timer -= deltaTime;

            if (_timer <= 0)
            {
                HideTreeAndSpawnLogs();
                this.StopUpdate();
            }
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_collider == null) TryGetComponent(out _collider);

            if (_rb == null) TryGetComponent(out _rb);
        }
#endif
    }
}
