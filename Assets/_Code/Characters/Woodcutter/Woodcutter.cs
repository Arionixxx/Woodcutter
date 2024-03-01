using Data;
using Trees;
using UnityEngine;
using Tree = Trees.Tree;

namespace Characters.Woodcutter
{
    public class Woodcutter : CharacterBase
    {
        [SerializeField]
        private WoodcutterTimer _woodcutterTimer;

        [SerializeField]
        private CharacterNavMeshMovement _characterNavMeshMovement;

        private readonly float _stopDistanceForSlashing = 2f;

        private Tree _currentTree;

        private bool _isCuttingTreeNow;

        private void Start()
        {
            SubscribeOnEvents();
            TryToFindTheNearestTree();
        }

        private void SubscribeOnEvents()
        {
            _woodcutterTimer.OnTheTimerExpires += TryToFindTheNearestTree;
        }

        private void TryToFindTheNearestTree()
        {
            _currentTree = TreesCollector.FindTheNearestTree(transform.position);

            if (_currentTree != null)
            {
                _characterNavMeshMovement.StartMovement(_currentTree.transform.position,
                    new NavmeshMovementData(_stopDistanceForSlashing, StartCutDownTheTree));
            }
        }

        private void StartCutDownTheTree()
        {
            Debug.Log("Start the cutting down");
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_woodcutterTimer == null) TryGetComponent(out _woodcutterTimer);

            if (_characterNavMeshMovement == null) TryGetComponent(out _characterNavMeshMovement);
        }
#endif
    }
}
