using Data;
using FixedPoints;
using System;
using Trees;
using UnityEngine;
using Tree = Trees.Tree;

namespace Characters.Woodcutter
{
    public class Woodcutter : CharacterBase
    {
        [SerializeField]
        private WoodcutterCuttingDown _woodcutterCuttingDown;

        [SerializeField]
        private WoodcutterAnimation _characterAnimation;

        [SerializeField]
        private WoodcutterLogsCollecting _woodcutterLogsCollecting;

        [SerializeField]
        private WoodcutterLogStorage _woodcutterLogStorage;

        [SerializeField]
        private CharacterAnimationEvents _characterAnimationEvents;

        [SerializeField]
        private CharacterNavMeshMovement _characterNavMeshMovement;

        private float _stopDistanceForSlashing;

        public static event Action OnTheNearestTreeNotFound;

        private void Start()
        {
            _stopDistanceForSlashing = DataProvider.PlayerSettings.StopDistanceForSlashing;
            SubscribeOnEvents();
            TryToFindTheNearestTree();
        }

        private void SubscribeOnEvents()
        {
            TreesSpawner.OnTreesSpawn += TryToFindTheNearestTree;
            _woodcutterLogStorage.OnLogStored += TryToFindTheNearestTree;
            _characterAnimationEvents.OnAxeSlashEnded += _characterAnimation.BreakAxeSlash;
            _woodcutterLogsCollecting.OnLogsCollected += () =>
            {
                _characterNavMeshMovement.StartMovement(WoodDeliveryPoint.Position, new NavmeshMovementData(WoodDeliveryPoint.Rotation.eulerAngles, () => 
                {
                    _woodcutterLogStorage.Store();
                }));
            };
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
                OnTheNearestTreeNotFound?.Invoke();
            }
        }

        private void StartCutDownTheTree()
        {
            _woodcutterCuttingDown.StartNewCuttingDown();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_characterNavMeshMovement == null) TryGetComponent(out _characterNavMeshMovement);

            if (_characterAnimation == null) TryGetComponent(out _characterAnimation);

            if (_woodcutterCuttingDown == null) TryGetComponent(out _woodcutterCuttingDown);

            if (_characterAnimationEvents == null) _characterAnimationEvents = GetComponentInChildren<CharacterAnimationEvents>();

            if (_woodcutterLogsCollecting == null) TryGetComponent(out _woodcutterLogsCollecting);

            if (_woodcutterLogStorage == null) TryGetComponent(out _woodcutterLogStorage);
        }
#endif
    }
}
