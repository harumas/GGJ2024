using UnityEngine;

namespace Event
{
    public abstract class AbstractEvent : MonoBehaviour
    {
        public abstract void Play();
        public abstract void Rewind();
    }
}