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
        private WoodcutterCuttingDown _woodcutterCuttingDown;

        [SerializeField]
        private WoodcutterAnimation _characterAnimation;

        [SerializeField]
        private CharacterAnimationEvents _characterAnimationEvents;

        [SerializeField]
        private CharacterNavMeshMovement _characterNavMeshMovement;

        private readonly float _stopDistanceForSlashing = 1.5f;

        private void Start()
        {
            SubscribeOnEvents();
            TryToFindTheNearestTree();
        }

        private void SubscribeOnEvents()
        {
            TreesSpawner.OnTreesSpawn += TryToFindTheNearestTree;
            _woodcutterTimer.OnTheTimerExpires += TryToFindTheNearestTree;
            _characterAnimationEvents.OnAxeSlashEnded += _characterAnimation.BreakAxeSlash;
        }

        private void TryToFindTheNearestTree()
        {
            Tree tree = TreesCollector.FindTheNearestTree(transform.position);

            if (tree != null)
            {
                _characterNavMeshMovement.StartMovement(tree.transform.position,
                    new NavmeshMovementData(_stopDistanceForSlashing, StartCutDownTheTree));

                _woodcutterCuttingDown.SetupCurrentTree(tree);
            }
            else
            {
                //go to the house and wait for new trees
            }
        }

        private void StartCutDownTheTree()
        {
            _woodcutterCuttingDown.StartNewCuttingDown();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_woodcutterTimer == null) TryGetComponent(out _woodcutterTimer);

            if (_characterNavMeshMovement == null) TryGetComponent(out _characterNavMeshMovement);

            if (_characterAnimation == null) TryGetComponent(out _characterAnimation);

            if (_woodcutterCuttingDown == null) TryGetComponent(out _woodcutterCuttingDown);

            if (_characterAnimationEvents == null) _characterAnimationEvents = GetComponentInChildren<CharacterAnimationEvents>();
        }
#endif
    }
}
