using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject kotrolPanel;
    [SerializeField] private GameObject aboutUsPanel;
    [SerializeField] private GameObject keluarPanel;

    [Header("Save System")]

    private bool isMainMenuPanel = false;
    private bool isSettingsOpen = false;
    private bool isKontrolOpen = false;
    private bool isAboutOpen = false;
    private bool isKeluarConfirmOpen = false;

    public bool IsMenuOpen => isMainMenuPanel || isSettingsOpen || isKontrolOpen || isAboutOpen || isKeluarConfirmOpen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscape();
        }
    }

    private void HandleEscape()
    {
        if (isKeluarConfirmOpen)
        {
            CancelKeluar();
        }
        else if (isSettingsOpen)
        {
            CloseSettings();
        }
        else if (isKontrolOpen)
        {
            CloseKontrol();
        }
        else if (isAboutOpen)
        {
            CloseAbout();
        }
    }

    public void OpenMainMenuPanel()
    {
        isMainMenuPanel = true;
        mainMenuPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        isSettingsOpen = true;
        settingsPanel.SetActive(true);

        isMainMenuPanel = false;
        mainMenuPanel.SetActive(false);
    }

    public void OpenKontrol()
    {
        isKontrolOpen = true;
        kotrolPanel.SetActive(true);

        isMainMenuPanel = false;
        mainMenuPanel.SetActive(false);
    }

    public void OpenAbout()
    {
        isAboutOpen = true;
        aboutUsPanel.SetActive(true);

        isMainMenuPanel = false;
        mainMenuPanel.SetActive(false);
    }

    public void OpenKeluarPanel()
    {
        keluarPanel.SetActive(true);
        isKeluarConfirmOpen = true;

        isMainMenuPanel = false;
        mainMenuPanel.SetActive(false);
    }

    public void CancelKeluar()
    {
        keluarPanel.SetActive(false);
        isKeluarConfirmOpen = false;

        isMainMenuPanel = true;
        mainMenuPanel.SetActive(true);
    }

    public void ConfirmKeluar()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#endif
    }

    public void CloseSettings()
    {
        isSettingsOpen = false;
        settingsPanel.SetActive(false);

        isMainMenuPanel = true;
        mainMenuPanel.SetActive(true);
    }

    public void CloseKontrol()
    {
        isKontrolOpen = false;
        kotrolPanel.SetActive(false);

        isMainMenuPanel = true;
        mainMenuPanel.SetActive(true);
    }

    public void CloseAbout()
    {
        isAboutOpen = false;
        aboutUsPanel.SetActive(false);

        isMainMenuPanel = true;
        mainMenuPanel.SetActive(true);
    }
}
