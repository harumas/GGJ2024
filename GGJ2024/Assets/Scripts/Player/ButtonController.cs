using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Cysharp.Threading.Tasks;
public class ButtonController : MonoBehaviour
{
    [SerializeField] private CanvasGroup SettingsMenu;
    [SerializeField] private CanvasGroup program;

    void Start()
    {
        SettingsMenu.interactable = false;
        SettingsMenu.blocksRaycasts = false;
        SettingsMenu.alpha = 0;

        program.interactable = false;
        program.blocksRaycasts = false;
        program.alpha = 0;
    }

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
