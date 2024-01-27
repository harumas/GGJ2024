using System;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Event
{
    [Serializable]
    public struct AnomalyEventStamp
    {
        [SerializeField] private int normalCount;
        [SerializeField] private GameObject eventObject;

        public int NormalCount => normalCount;
        public GameObject EventObject => eventObject;
    }

    public class AnomalyEventSequencer : MonoBehaviour
    {
        [SerializeField] private AnomalyEventStamp[] schedule;
        [SerializeField] private GameObject[] normalEvents;

        private int nextAnomalyEventCount;
        private int currentCount;
        private int currentIndex;
        private NormalEvent currentNormalEvent;
        private AbstractEvent currentAnomalyEvent;
        private TypingSystem typingSystem;

        private void Start()
        {
            nextAnomalyEventCount = schedule[0].NormalCount;
            typingSystem = Locator.Resolve<TypingSystem>();
            typingSystem.OnTypingCompleted += OnTypingCompleted;

            PlayNormalEvent();
            PlayAnomalyEvent();
        }

        private void OnTypingCompleted()
        {
            RewindEvent(currentNormalEvent);

            if (currentCount == 0)
            {
                RewindEvent(currentAnomalyEvent);
            }

            PlayNormalEvent();
            PlayAnomalyEvent();
        }

        private void PlayNormalEvent()
        {
            currentCount++;
            currentNormalEvent = ExecuteAbstractEvent(normalEvents[Random.Range(0, normalEvents.Length)]) as NormalEvent;
            typingSystem.StartTyping(currentNormalEvent).Forget();
        }

        private void PlayAnomalyEvent()
        {
            if (currentCount > nextAnomalyEventCount)
            {
                currentAnomalyEvent = ExecuteAbstractEvent(schedule[currentIndex].EventObject);
                currentCount = 0;
                currentIndex++;

                if (currentIndex < schedule.Length)
                {
                    nextAnomalyEventCount = schedule[currentIndex].NormalCount;
                }
                else
                {
                    Debug.LogError("シーケンスが終了しました。");
                }
            }
        }

        private void RewindEvent(AbstractEvent abstractEvent)
        {
            abstractEvent.Rewind();
        }

        private AbstractEvent ExecuteAbstractEvent(GameObject original)
        {
            if (original.TryGetComponent(out AbstractEvent abstractEvent))
            {
                abstractEvent.Play();
                return abstractEvent;
            }

            Debug.Log($"{nameof(AbstractEvent)}が実装されていません！");
            return null;
        }
    }
}