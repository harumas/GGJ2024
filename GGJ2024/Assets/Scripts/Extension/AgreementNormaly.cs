using System;
using System.Collections.Generic;
using System.Linq;
using Event;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extension
{
    public class AgreementNormaly : NormalEvent
    {
        [SerializeField] private Animator[] supporters;
        [SerializeField] private ChatView[] chatViews;
        private static readonly int positiveId = Animator.StringToHash("IsPositive");

        private List<int> agreeIndexes;

        public override void Play()
        {
            int takeCount = Random.Range(1, supporters.Length);
            agreeIndexes = Enumerable.Range(0, supporters.Length).OrderBy(_ => Guid.NewGuid()).Take(takeCount).ToList();
            correct = takeCount.ToString();

            foreach (int i in agreeIndexes)
            {
                supporters[i].SetBool(positiveId, true);
                chatViews[i].gameObject.SetActive(true);
            }
        }

        public override void Rewind()
        {
            foreach (int i in agreeIndexes)
            {
                supporters[i].SetBool(positiveId, false);
                chatViews[i].gameObject.SetActive(false);
            }
        }
    }
}