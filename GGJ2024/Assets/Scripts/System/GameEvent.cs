using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Utility;

namespace System
{
    public class GameEvent : MonoBehaviour
    {
        [SerializeField] private CanvasGroup gameOverCanvasGroup;
        [SerializeField] private CanvasGroup gameClearCanvasGroup;
        [SerializeField] private GameObject gameOverObj;

        public bool IsCameraLock { get; private set; }

        private void Awake()
        {
            Locator.Register(this);
        }

        public void OnGameOver()
        {
            gameOverObj.SetActive(true);
            Camera.main.transform.localRotation = Quaternion.identity;
            IsCameraLock = true;

            Cursor.lockState = CursorLockMode.None;

            DOTween.To(() => gameOverCanvasGroup.alpha, (a) => gameOverCanvasGroup.alpha = a, 1, 0.3f)
                .SetDelay(5)
                .OnComplete(() =>
                {
                    gameOverCanvasGroup.blocksRaycasts = true;
                    gameOverCanvasGroup.interactable = true;
                });
        }


        public void OnGameClear()
        {
            IsCameraLock = true;

            Cursor.lockState = CursorLockMode.None;

            DOTween.To(() => gameClearCanvasGroup.alpha, (a) => gameClearCanvasGroup.alpha = a, 1, 0.3f)
                .OnComplete(() =>
                {
                    gameClearCanvasGroup.blocksRaycasts = true;
                    gameClearCanvasGroup.interactable = true;
                });
        }

        public void Restart()
        {
            Debug.Log("Restart");
            SceneManager.LoadScene("Player");
        }

        public void ReturnToTitle()
        {
            SceneManager.LoadScene("Title");
        }
    }
}