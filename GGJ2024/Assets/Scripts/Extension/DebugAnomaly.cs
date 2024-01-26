using Event;
using UnityEngine;

namespace Extension
{
    public class DebugAnomaly : NormalEvent
    {
        public override void Play()
        {
            Debug.Log("Play");
        }

        public override void Rewind()
        {
            Debug.Log("Rewind");
        }
    }
}