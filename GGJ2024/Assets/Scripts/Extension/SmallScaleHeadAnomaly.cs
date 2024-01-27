using System;
using UnityEngine;

namespace Event
{
    public class SmallScaleHeadAnomaly : AbstractEvent
    {
        [SerializeField] private Transform headBone;
        [SerializeField] private float multiplier;

        public override void Play()
        {
            headBone.localScale *= multiplier;
        }

        public override void Rewind()
        {
            headBone.localScale /= multiplier;
        }
    }
}