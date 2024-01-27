using Event;
using UnityEngine;

namespace Extension
{
    public class BigScaleAnomaly : AbstractEvent
    {
        [SerializeField] private Transform[] scaleObjects;
        [SerializeField] private float multiplier;
        
        public override void Play()
        {
            foreach (Transform scaleObject in scaleObjects)
            {
                scaleObject.localScale *= multiplier;
            }
        }

        public override void Rewind()
        {
            foreach (Transform scaleObject in scaleObjects)
            {
                scaleObject.localScale /= multiplier;
            }
        }
    }
}