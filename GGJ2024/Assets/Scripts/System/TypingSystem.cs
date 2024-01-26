using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Event;
using UnityEngine;
using Utility;

namespace System
{
    public class TypingSystem : MonoBehaviour
    {
        public event Action<string> OnSetText;
        public event Action OnTypingCompleted;

        [SerializeField] private string currentAnswer;
        [SerializeField] private bool isCameraFocusing;
        [SerializeField] private float checkInterval = 1f;
        private CancellationTokenSource typingCanceller;

        private void Awake()
        {
            Locator.Register(this);
        }

        public void OnCameraFocused()
        {
            isCameraFocusing = true;
        }

        public void OnCameraUnfocused()
        {
            isCameraFocusing = false;
        }

        public async UniTaskVoid StartTyping(NormalEvent normalEvent)
        {
            UpdateText(normalEvent);
            typingCanceller = new CancellationTokenSource();

            while (!this.GetCancellationTokenOnDestroy().IsCancellationRequested && !typingCanceller.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                if (!isCameraFocusing)
                {
                    continue;
                }

                foreach (char c in Input.inputString)
                {
                    if (!currentAnswer.Contains(c))
                    {
                        currentAnswer += c;

                        await UniTask.Delay(TimeSpan.FromSeconds(checkInterval), cancellationToken: this.GetCancellationTokenOnDestroy());
                        //
                        // if ()
                        // {
                        //     OnTypingCompleted?.Invoke();
                        // }
                        // else
                        // {
                        //     
                        // }
                        //

                        currentAnswer = String.Empty;

                        string text = UpdateText(normalEvent);
                    }
                }
            }
        }

        private string UpdateText(NormalEvent normalEvent)
        {
            string space = new string(Enumerable.Repeat('_', normalEvent.Correct.Length - currentAnswer.Length).ToArray());
            space = currentAnswer + space;
            string text = normalEvent.Sentence.Replace("$", space);

            if (currentAnswer.Length == normalEvent.Correct.Length)
            {
                text = currentAnswer == normalEvent.Correct ? $"<color=green>{text}</color>" : $"<color=red>{text}</color>";
            }

            OnSetText?.Invoke(text);

            return space;
        }
    }
}