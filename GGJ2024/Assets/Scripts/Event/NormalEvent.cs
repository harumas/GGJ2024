using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public abstract class NormalEvent : AbstractEvent
    {
        [SerializeField] protected string sentence;
        [SerializeField] protected string correct;

        public string Sentence => sentence;
        public string Correct => correct;
    }
}