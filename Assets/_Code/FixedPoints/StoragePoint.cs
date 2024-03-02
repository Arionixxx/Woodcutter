using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FixedPoints
{
    public class StoragePoint : FixedPointBase
    {
        private static Transform _transform;
        public static Vector3 Position => _transform.position;

        private void Awake()
        {
            _transform = transform;
        }
    }
}
