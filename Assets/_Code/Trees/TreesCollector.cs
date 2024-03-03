using System.Collections.Generic;
using UnityEngine;

namespace Trees
{
    public class TreesCollector : MonoBehaviour
    {
        private static HashSet<Tree> _treesStatic = new HashSet<Tree>();

        public static Tree FindTheNearestTree(Vector3 position)
        {
            float minDist = float.MaxValue;
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

        public static void AddTree(Tree tree)
        {
            if (!_treesStatic.Contains(tree))
            {
                _treesStatic.Add(tree);
            }
        }

        public static void RemoveTree(Tree tree)
        {
            if (_treesStatic.Contains(tree))
            {
                _treesStatic.Remove(tree);
            }
        }
    }
}
