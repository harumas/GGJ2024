using System.Collections.Generic;
using UnityEngine;
using Event;

namespace Extension
{
    public class ChangeBornPositionAnomaly : AbstractEvent
    {
        [SerializeField] private List<GameObject> targetCharacters = new List<GameObject>();
        [SerializeField] private List<GameObject> changedCharacters = new List<GameObject>();

        public override void Play()
        {
            for (int i = 0; i < targetCharacters.Count; i++)
            {
                targetCharacters[i].SetActive(false);
                // changedCharacters[i].transform.position = targetCharacters[i].transform.position;
                // changedCharacters[i].transform.rotation = targetCharacters[i].transform.rotation;
                changedCharacters[i].SetActive(true);
            }
        }

        public override void Rewind()
        {
            for (int i = 0; i < targetCharacters.Count; i++)
            {
                targetCharacters[i].SetActive(true);
                changedCharacters[i].SetActive(false);
            }
        }
    }

}