using UnityEngine;

namespace Trees
{
    public class TreeSpawnPoint : MonoBehaviour
    {
        private void Awake()
        {
            TreesSpawner.AddSpawnPoint(this);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 pos = new Vector3(transform.position.x,
                transform.position.y + 0.5f, transform.position.z);
            Gizmos.DrawCube(pos, Vector3.one);
        }
#endif
    }
}
