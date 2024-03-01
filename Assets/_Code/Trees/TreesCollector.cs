using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trees
{
    public class TreesCollector : MonoBehaviour
    {
        [SerializeField]
        private List<Tree> _trees = new List<Tree>();

        private static List<Tree> _treesStatic = new List<Tree>();

        private static readonly float _tempMinDistanceForTheNearestTree = 10000f;

        private void Awake()
        {
            _treesStatic = _trees;
        }

        public static Tree FindTheNearestTree(Vector3 position)
        {
            float minDist = _tempMinDistanceForTheNearestTree;
            Tree nearestTree = null;

            foreach (Tree tree in _treesStatic)
            {
                float dist = (tree.transform.position - position).sqrMagnitude;
                if (dist < minDist)
                {
                    minDist = dist;
                    nearestTree = tree;
                }
            }

            return nearestTree;
        }
        private void FillTreesList()
        {
            _trees.Clear();
            Tree[] treesArr = GetComponentsInChildren<Tree>();

            foreach (Tree tree in treesArr)
            {
                _trees.Add(tree);
            }
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            FillTreesList();
        }
#endif
    }
}
