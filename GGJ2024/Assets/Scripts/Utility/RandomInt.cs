using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utility
{
    [Serializable]
    public class RandomInt
    {
        [SerializeField] private int min;
        [SerializeField] private int max;

        public int MakeValue()
        {
            return Random.Range(min, max + 1);
        }
    }
}