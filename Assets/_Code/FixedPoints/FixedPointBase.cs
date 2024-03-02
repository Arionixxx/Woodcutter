using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FixedPoints {
    public class FixedPointBase : MonoBehaviour
    {

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            Gizmos.DrawCube(pos, Vector3.one);
        }
#endif
    }
}
