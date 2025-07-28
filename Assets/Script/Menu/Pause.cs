using UnityEngine;

public class Pause : MonoBehaviour
{
    [Header("Main Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject resumeButton;

    [Header("Sub Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject bantuanPanel;
    [SerializeField] private GameObject controlPanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeSelf || bantuanPanel.activeSelf)
            {
                BackToPause();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void OnPauseButtonPressed()
    {
        if (!isPaused)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        pausePanel.SetActive(true);
        resumeButton.SetActive(true);

        settingsPanel.SetActive(false);
        bantuanPanel.SetActive(false);
        controlPanel.SetActive(false);

        SetCursorState(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        bantuanPanel.SetActive(false);
        controlPanel.SetActive(false);

        SetCursorState(false);
    }

    public void OpenSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OpenBantuan()
    {
        settingsPanel.SetActive(false);
        controlPanel.SetActive(false);
        pausePanel.SetActive(false);
        bantuanPanel.SetActive(true);
    }

    public void OpenControl()
    {
        settingsPanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void BackToPause()
    {
        settingsPanel.SetActive(false);
        bantuanPanel.SetActive(false);
        controlPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    private void SetCursorState(bool showCursor)
    {
#if UNITY_EDITOR
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.None;
#else
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
#endif
        Cursor.visible = showCursor;
    }
}
