using Event;
using UI;
using UnityEngine;

namespace Extension
{
    public class ChatNormaly : NormalEvent
    {
        [SerializeField] private ChatView chatView;

        public override void Play()
        {
            chatView.SetText(Sentence.Replace("$", Correct));
            chatView.gameObject.SetActive(true);
        }

        public override void Rewind()
        {
            chatView.gameObject.SetActive(false);
        }
    }
}