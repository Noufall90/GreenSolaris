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
        SoundManager.Instance.PlaySound2D("Button");
        if (!isPaused)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {   
        SoundManager.Instance.PlaySound2D("Button");
        isPaused = true;
        Time.timeScale = 0f;

        pausePanel.SetActive(true);
        resumeButton.SetActive(true);

        settingsPanel.SetActive(false);
        bantuanPanel.SetActive(false);
        controlPanel.SetActive(false);

    }

    public void ResumeGame()
    {   
        SoundManager.Instance.PlaySound2D("Button");        
        isPaused = false;
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        bantuanPanel.SetActive(false);
        controlPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        SoundManager.Instance.PlaySound2D("Button");
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OpenBantuan()
    {
        SoundManager.Instance.PlaySound2D("Button");
        settingsPanel.SetActive(false);
        controlPanel.SetActive(false);
        pausePanel.SetActive(false);
        bantuanPanel.SetActive(true);
    }

    public void OpenControl()
    {   
        SoundManager.Instance.PlaySound2D("Button");
        settingsPanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void BackToPause()
    {   
        SoundManager.Instance.PlaySound2D("Button");
        settingsPanel.SetActive(false);
        bantuanPanel.SetActive(false);
        controlPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
