using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Player/Create Player Settings", order = 2)]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField]
        private float _moveSpeedForFullRunAnimation;

        [SerializeField]
        private float _maxMoveSpeed;

        [SerializeField]
        private float _rotationSpeed;

        [SerializeField]
        private float _stopDistanceForSlashing;

        [SerializeField]
        private float _delayBetweenSlashes;

        [SerializeField]
        private int _slashesBeforeTreeWillBeCuttedDown;

        public float MoveSpeedForFullRunAnimation => _moveSpeedForFullRunAnimation;

        public float MaxMoveSpeed => _maxMoveSpeed;

        public float RotationSpeed => _rotationSpeed;

        public float StopDistanceForSlashing => _stopDistanceForSlashing;

        public float DelayBetweenSlashes => _delayBetweenSlashes;

        public int SlashesBeforeTreeWillBeCutterDown => _slashesBeforeTreeWillBeCuttedDown;
    }
}
