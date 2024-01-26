using Event;
using UnityEngine;

namespace Extension
{
    public class DebugAnomaly : AbstractEvent
    {
        public override void Play()
        {
            Debug.Log("PlayAnomaly");
        }

        public override void Rewind()
        {
            Debug.Log("RewindAnomaly");
        }
    }
}