using System.Collections;
using System.Collections.Generic;
using Trees;
using UnityEngine;

namespace Characters.Woodcutter
{
    public class WoodcutterLogsCollecting : MonoBehaviour
    {
        private List<SingleLog> _logs = new List<SingleLog>();

        public void TakeLogs(List<SingleLog> singleLogs)
        {
            _logs = singleLogs;
        }
    }
}
