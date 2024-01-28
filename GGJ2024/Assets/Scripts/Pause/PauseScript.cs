using UnityEngine;
using Utility;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    public bool isPause { get; private set; }

    void Start()
    {
        Locator.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("Title");
    }

    public void UnPause()
    {
        OnPause();
    }

    void OnPause()
    {
        isPause = !isPause;
        pauseCanvas.SetActive(isPause);
        Time.timeScale = isPause ? 0 : 1;
    }
}
