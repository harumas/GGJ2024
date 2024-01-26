using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public abstract class NormalEvent : AbstractEvent
    {
        [SerializeField] private string sentence;
        [SerializeField] private string correct;

        public string Sentence => sentence;
        public string Correct => correct;
    }
}