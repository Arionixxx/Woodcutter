using Collision;
using Data;
using Trees;
using UnityEngine;
using UpdateSys;
using Tree = Trees.Tree;

namespace Characters.Woodcutter
{
    public class WoodcutterCuttingDown : MonoBehaviour, IUpdatable
    {
        [SerializeField]
        private CharacterAnimationEvents _characterAnimationEvents;

        [SerializeField]
        private WoodcutterAnimation _woodcutterAnimation;

        [SerializeField]
        private WoodcutterLogsCollecting _woodcutterLogsCollecting;

        [SerializeField]
        private AxeCollision _axe;

        private Tree _currentTree;

        private int _slashesBeforeTreeWillBeCuttedDown;
        private int _currentSlashCount = 0;
        private float _delayBetweenSlashes;
        private float _timer;
        private bool _isCuttingDown;

        private void Start()
        {
            _delayBetweenSlashes = DataProvider.PlayerSettings.DelayBetweenSlashes;
            _slashesBeforeTreeWillBeCuttedDown = DataProvider.PlayerSettings.SlashesBeforeTreeWillBeCutterDown;
            SubscribeOnEvents();
            HideAxe();
        }

        public void OnSystemUpdate(float deltaTime)
        {
            TimerUpdate(deltaTime);
        }

        public void SetupCurrentTree(Tree tree)
        {
            if (_currentTree != null)
            {
                _currentTree.OnLogsSpawn -= _woodcutterLogsCollecting.StartLogsCollecting;
            }

            _currentTree = tree;
        }

        public void StartNewCuttingDown()
        {
            _isCuttingDown = true;
            _characterAnimationEvents.OnAxeSlashEnded -= HideAxe;
            ShowAxe();
            _woodcutterAnimation.AxeChop();
        }

        private void StopCuttingDown()
        {
            _isCuttingDown = false;
            TreesCollector.RemoveTree(_currentTree);
        }

        private void SubscribeOnEvents()
        {
            _characterAnimationEvents.OnAxeColliderTurningOn += _axe.TurnOnCollision;
            _characterAnimationEvents.OnAxeColliderTurningOff += _axe.TurnOffCollision;
            _characterAnimationEvents.OnAxeSlashMiddle += IncreaseSlashCount;
            _characterAnimationEvents.OnAxeSlashEnded += () =>
            {
                if (_isCuttingDown)
                {
                    _timer = _delayBetweenSlashes;
                    this.StartUpdate();
                }
            };
        }

        private void IncreaseSlashCount()
        {
            _currentSlashCount++;
            if (_currentSlashCount >= _slashesBeforeTreeWillBeCuttedDown)
            {
                StopCuttingDown();
                _currentSlashCount = 0;
                _currentTree.CutDown();
                _currentTree.OnLogsSpawn += _woodcutterLogsCollecting.StartLogsCollecting;
                _characterAnimationEvents.OnAxeSlashEnded += HideAxe;
            }
        }

        private void TimerUpdate(float deltaTime)
        {
            _timer -= deltaTime;

            if (_timer <= 0)
            {
                _woodcutterAnimation.AxeChop();
                this.StopUpdate();
            }
        }

        private void HideAxe()
        {
            _axe.gameObject.SetActive(false);
        }

        private void ShowAxe()
        {
            _axe.gameObject.SetActive(true);
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_characterAnimationEvents == null) _characterAnimationEvents = GetComponentInChildren<CharacterAnimationEvents>();

            if (_woodcutterAnimation == null) TryGetComponent(out _woodcutterAnimation);

            if (_woodcutterLogsCollecting == null) TryGetComponent(out _woodcutterLogsCollecting);
        }
#endif
    }
}
