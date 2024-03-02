using UnityEngine;

namespace Services
{
    public class SleepDisabler : MonoBehaviour
    {
        private void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}
