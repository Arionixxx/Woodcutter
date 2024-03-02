using UnityEngine;

namespace FixedPoints
{
    public class WoodDeliveryPoint : FixedPointBase
    {
        private static Transform _transform;
        public static Vector3 Position => _transform.position;
        public static Quaternion Rotation => _transform.rotation;

        private void Awake()
        {
            _transform = transform;
        }
    }
}
