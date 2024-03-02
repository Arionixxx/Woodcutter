using UnityEngine;

namespace Trees
{
    public class SingleLog : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rb;

        private readonly float _yOffset = 1f;

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            gameObject.SetActive(true);
            transform.position = new Vector3(position.x, position.y + _yOffset, position.z);
            transform.rotation = rotation;
            _rb.isKinematic = false;
        }

        public void MakeKinematic()
        {
            _rb.isKinematic = true;
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_rb == null) TryGetComponent(out _rb);
        }
#endif
    }
}
