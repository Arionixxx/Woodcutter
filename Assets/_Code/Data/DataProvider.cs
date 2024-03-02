using UnityEngine;

namespace Data
{
    public class DataProvider : MonoBehaviour
    {
        [SerializeField]
        private PlayerSettings _playerSettings;

        [SerializeField]
        private TreeSettings _treeSettings;

        private static PlayerSettings _playerSettingsStatic;
        private static TreeSettings _treeSettingsStatic;

        public static PlayerSettings PlayerSettings => _playerSettingsStatic;
        public static TreeSettings TreeSettings => _treeSettingsStatic;

        private void Awake()
        {
            _playerSettingsStatic = _playerSettings;
            _treeSettingsStatic = _treeSettings;
        }
    }
}
