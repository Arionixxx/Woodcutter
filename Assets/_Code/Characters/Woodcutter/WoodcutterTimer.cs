using System;
using UnityEngine;
using UpdateSys;

namespace Characters.Woodcutter
{
    public class WoodcutterTimer : MonoBehaviour, IUpdatable
    {
        //this class calculate delay between trees cutting down
        private readonly float _delay = 5f;
        private float _timer;

        public event Action OnTheTimerExpires;

        public void OnSystemUpdate(float deltaTime)
        {
            TimerUpdate(deltaTime);
        }

        public void StartNewTimer()
        {
            _timer = _delay;
            this.StartUpdate();
        }

        private void TimerUpdate(float deltaTime)
        {
            _timer -= deltaTime;

            if (_timer <= 0)
            {
                OnTheTimerExpires?.Invoke();
                this.StopUpdate();
            }
        }
    }
}
