using Event;
using UnityEngine;

namespace Extension
{
    public class DebugNormaly : NormalEvent
    {
        public override void Play()
        {
            Debug.Log("PlayNormaly");
        }

        public override void Rewind()
        {
            Debug.Log("RewindNormaly");
        }
    }
}