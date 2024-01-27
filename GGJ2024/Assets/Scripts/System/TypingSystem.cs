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
        private const string characters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

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
                    if (!characters.Contains(c))
                    {
                        continue;
                    }

                    if (currentAnswer.Length == normalEvent.Correct.Length)
                    {
                        continue;
                    }

                    currentAnswer += c;

                    UpdateText(normalEvent);

                    if (currentAnswer.Length == normalEvent.Correct.Length)
                    {
                        bool isCorrect = currentAnswer == normalEvent.Correct;
                        await UniTask.Delay(TimeSpan.FromSeconds(checkInterval), cancellationToken: this.GetCancellationTokenOnDestroy());

                        if (isCorrect)
                        {
                            OnTypingCompleted?.Invoke();
                        }

                        currentAnswer = String.Empty;
                        UpdateText(normalEvent);

                        if (isCorrect)
                        {
                            return;
                        }
                    }
                }
            }
        }

        private void UpdateText(NormalEvent normalEvent)
        {
            string space = new string(Enumerable.Repeat('_', normalEvent.Correct.Length - currentAnswer.Length).ToArray());
            space = currentAnswer + space;

            if (currentAnswer.Length == normalEvent.Correct.Length)
            {
                space = currentAnswer == normalEvent.Correct ? $"<color=green>{space}</color>" : $"<color=red>{space}</color>";
            }

            string text = normalEvent.Sentence.Replace("$", space);

            OnSetText?.Invoke(text);
        }
    }
}