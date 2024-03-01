using System;
using UnityEngine;

namespace Data
{
    public struct NavmeshMovementData
    {
        public readonly float StopDistance;
        public readonly Vector3 RotationInReachedPoint;
        public readonly Action OnReachedPointEvent;

        public NavmeshMovementData(float stopDistance) : this(stopDistance, Vector3.zero, null) { }

        public NavmeshMovementData(Action onReachedPointEvent) : this(0, Vector3.zero, onReachedPointEvent) { }

        public NavmeshMovementData(Vector3 rotationInReachedPoint) : this(0, rotationInReachedPoint, null) { }

        public NavmeshMovementData(float stopDistance, Action onReachedPointEvent) : this(stopDistance, Vector3.zero, onReachedPointEvent) { }

        public NavmeshMovementData(float stopDistance, Vector3 rotationInReachedPoint, Action onReachedPointEvent)
        {
            StopDistance = stopDistance;
            RotationInReachedPoint = rotationInReachedPoint;
            OnReachedPointEvent = onReachedPointEvent;
        }
    }
}
