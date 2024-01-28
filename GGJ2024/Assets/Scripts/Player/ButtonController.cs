using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Cysharp.Threading.Tasks;
public class ButtonController : MonoBehaviour
{
    [SerializeField] private CanvasGroup SettingsMenu;
    [SerializeField] private CanvasGroup program;

    public void ViewSetting()
    {
        SettingsMenu.interactable = true;
        SettingsMenu.blocksRaycasts = true;
        DOTween.To(() => SettingsMenu.alpha, (a) => SettingsMenu.alpha = a, 1, 0.3f);
    }

    public void GameStart()
    {
        SceneChange().Forget();
    }

    private async UniTaskVoid SceneChange()
    {
        program.interactable = true;
        program.blocksRaycasts = true;
        await DOTween.To(() => program.alpha, (a) => program.alpha = a, 1, 0.3f).AsyncWaitForCompletion();
        await UniTask.Delay(1000);

        SceneManager.LoadScene("Player");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
}
