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
        private WoodcutterTimer _woodcutterTimer;

        [SerializeField]
        private GameObject _axe;

        private Tree _currentTree;

        private readonly float _delayBetweenSlashes = 0.5f;
        private readonly int _slashesBeforeTreWillBeCuttedDown = 3;

        private int _currentSlashCount = 0;
        private float _timer;
        private bool _isCuttingDown;

        private void Start()
        {
            SubscribeOnEvents();
            _axe.SetActive(false);
        }

        public void OnSystemUpdate(float deltaTime)
        {
            TimerUpdate(deltaTime);
        }


        public void SetupCurrentTree(Tree tree)
        {
            _currentTree = tree;
        }

        public void StartNewCuttingDown()
        {
            _axe.SetActive(true);
            _woodcutterAnimation.AxeSlash();
            _isCuttingDown = true;
        }

        private void StopCuttingDown()
        {
            _isCuttingDown = false;
            TreesCollector.RemoveTree(_currentTree);
        }

        private void SubscribeOnEvents()
        {
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
            if (_currentSlashCount >= _slashesBeforeTreWillBeCuttedDown)
            {
                StopCuttingDown();
                _currentSlashCount = 0;
                _currentTree.CutDown();
                _woodcutterTimer.StartNewTimer();
            }
        }

        private void TimerUpdate(float deltaTime)
        {
            _timer -= deltaTime;

            if (_timer <= 0)
            {
                _woodcutterAnimation.AxeSlash();
                this.StopUpdate();
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_characterAnimationEvents == null) _characterAnimationEvents = GetComponentInChildren<CharacterAnimationEvents>();

            if (_woodcutterAnimation == null) TryGetComponent(out _woodcutterAnimation);

            if (_woodcutterTimer == null) TryGetComponent(out _woodcutterTimer);
        }
#endif
    }
}
