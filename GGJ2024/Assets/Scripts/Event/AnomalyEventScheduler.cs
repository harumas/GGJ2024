using UnityEngine;
using Utility;

namespace Event
{
    public class AnomalyEventScheduler : MonoBehaviour
    {
        [SerializeField] private GameObject[] normalEvents;
        [SerializeField] private GameObject[] anomalyEvents;
        [SerializeField] private RandomInt anomalyEventRange;

        private int nextAnomalyEventCount;
        private int currentCount;
        private AbstractEvent currentNormalEvent;
        private AbstractEvent currentAnomalyEvent;

        private void Start()
        {
            nextAnomalyEventCount = anomalyEventRange.MakeValue();
            
            currentCount++;
            ExecuteAbstractEvent(normalEvents);
        }

        public void OnTypingCompleted()
        {
            RewindEvent(currentNormalEvent);

            if (currentCount == 0)
            {
                RewindEvent(currentAnomalyEvent);
            }

            currentCount++;
            ExecuteAbstractEvent(normalEvents);

            if (currentCount == nextAnomalyEventCount)
            {
                ExecuteAbstractEvent(anomalyEvents);
                currentCount = 0;
            }
        }

        private void RewindEvent(AbstractEvent abstractEvent)
        {
            abstractEvent.Rewind();
            Destroy(abstractEvent.gameObject);
        }

        private void ExecuteAbstractEvent(GameObject[] resources)
        {
            GameObject original = resources[Random.Range(0, resources.Length)];
            GameObject executeEvent = Instantiate(original);

            if (executeEvent.TryGetComponent(out AbstractEvent abstractEvent))
            {
                abstractEvent.Play();
            }
            else
            {
                Debug.Log($"{nameof(AbstractEvent)}が実装されていません！");
            }
        }
    }
}