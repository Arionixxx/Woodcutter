using System;
using UnityEngine;

namespace Data
{
    public struct NavmeshMovementData
    {
        public readonly Vector3 RotationInReachedPoint;
        public readonly Action OnReachedPointEvent;

        public NavmeshMovementData(Action onReachedPointEvent) : this(Vector3.zero, onReachedPointEvent) { }

        public NavmeshMovementData(Vector3 rotationInReachedPoint) : this(rotationInReachedPoint, null) { }

        public NavmeshMovementData(Vector3 rotationInReachedPoint, Action onReachedPointEvent)
        {
            RotationInReachedPoint = rotationInReachedPoint;
            OnReachedPointEvent = onReachedPointEvent;
        }
    }
}
