using System;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Event
{
    public class AnomalyEventScheduler : MonoBehaviour
    {
        [SerializeField] private GameObject[] normalEvents;
        [SerializeField] private GameObject[] anomalyEvents;
        [SerializeField] private RandomInt anomalyEventRange;

        private int nextAnomalyEventCount;
        private int currentCount;
        private NormalEvent currentNormalEvent;
        private AbstractEvent currentAnomalyEvent;
        private TypingSystem typingSystem;

        private void Start()
        {
            nextAnomalyEventCount = anomalyEventRange.MakeValue();
            typingSystem = Locator.Resolve<TypingSystem>();
            typingSystem.OnTypingCompleted += OnTypingCompleted;

            PlayNormalEvent();
        }

        private void OnTypingCompleted()
        {
            RewindEvent(currentNormalEvent);

            if (currentCount == 0)
            {
                RewindEvent(currentAnomalyEvent);
            }

            PlayNormalEvent();

            if (currentCount == nextAnomalyEventCount)
            {
                currentAnomalyEvent = ExecuteAbstractEvent(anomalyEvents);
                currentCount = 0;
            }
        }

        private void PlayNormalEvent()
        {
            currentCount++;
            currentNormalEvent = ExecuteAbstractEvent(normalEvents) as NormalEvent;
            typingSystem.StartTyping(currentNormalEvent).Forget();
        }

        private void RewindEvent(AbstractEvent abstractEvent)
        {
            abstractEvent.Rewind();
            Destroy(abstractEvent.gameObject);
        }

        private AbstractEvent ExecuteAbstractEvent(GameObject[] resources)
        {
            GameObject original = resources[Random.Range(0, resources.Length)];
            GameObject executeEvent = Instantiate(original);
            
            if (executeEvent.TryGetComponent(out AbstractEvent abstractEvent))
            {
                abstractEvent.Play();
                return abstractEvent;
            }

            Debug.Log($"{nameof(AbstractEvent)}が実装されていません！");
            return null;
        }
    }
}