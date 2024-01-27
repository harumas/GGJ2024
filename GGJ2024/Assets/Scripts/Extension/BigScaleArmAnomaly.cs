using System;
using UnityEngine;

namespace Event
{
    public class BigScaleArmAnomaly : AbstractEvent
    {
        [SerializeField] private Transform armBoneR;
        [SerializeField] private Transform armBoneL;
        [SerializeField] private float multiplier;
        private bool isEnable;

        public override void Play()
        {
            armBoneR.localScale *= multiplier;
            armBoneL.localScale *= multiplier;
        }

        public override void Rewind()
        {
            armBoneR.localScale /= multiplier;
            armBoneL.localScale /= multiplier;
        }
    }
}