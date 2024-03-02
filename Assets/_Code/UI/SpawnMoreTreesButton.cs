using Characters.Woodcutter;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpawnMoreTreesButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        public static event Action OnSpawnButtonClick;

        private void Start()
        {
            _button.gameObject.SetActive(false);

            Woodcutter.OnTheNearestTreeNotFound += () => gameObject.SetActive(true);

            _button.onClick.AddListener(() =>
            {
                OnSpawnButtonClick?.Invoke();
                gameObject.SetActive(false);
            });
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_button == null) TryGetComponent(out _button);
        }
#endif
    }
}
