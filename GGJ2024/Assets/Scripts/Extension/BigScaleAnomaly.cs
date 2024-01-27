using DG.Tweening;
using Event;
using UnityEngine;

namespace Extension
{
    public class BigScaleAnomaly : AbstractEvent
    {
        [SerializeField] private Transform[] scaleObjects;
        [SerializeField] private float multiplier;
        [SerializeField] private float duration;

        public override void Play()
        {
            foreach (Transform scaleObject in scaleObjects)
            {
                scaleObject.DOScale(multiplier, duration).SetEase(Ease.InExpo);
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