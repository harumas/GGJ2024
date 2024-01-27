using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TypingView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI sentenceText;
        [SerializeField] private TypingSystem typingSystem;

        private void Awake()
        {
            typingSystem.OnSetText += SetText;
        }

        private void SetText(string text)
        {
            sentenceText.text = text;
        }
    }
}