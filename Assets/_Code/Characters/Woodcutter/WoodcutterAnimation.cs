using UnityEngine;

namespace Characters.Woodcutter {
    public class WoodcutterAnimation : CharacterAnimation
    {
        private int _cuttingHash;
        private int _breakCuttingHash;
        private int _magicCollectingHash;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void AxeSlash()
        {
            _animator.SetTrigger(_cuttingHash);
        }

        public void BreakAxeSlash()
        {
            _animator.SetTrigger(_breakCuttingHash);
        }

        public void SwitchMagicCollecting(bool value)
        {
            _animator.SetBool(_magicCollectingHash, value);
        }

        private void Initialize()
        {
            _cuttingHash = Animator.StringToHash("CuttingTrigger");
            _breakCuttingHash = Animator.StringToHash("CuttingBreak");
            _magicCollectingHash = Animator.StringToHash("MagicCollecting");
        }
    }
}
