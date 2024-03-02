using UnityEngine;

namespace FixedPoints
{
    public class WoodDeliveryPoint : FixedPointBase
    {
        private static Transform _transform;
        public static Vector3 Position => _transform.position;

        private void Awake()
        {
            _transform = transform;
        }
    }
}
