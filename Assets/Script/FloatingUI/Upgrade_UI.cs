using UnityEngine;

public class Upgrade_UI : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject upgradePanel;     // Panel UI yang akan dibuka saat tekan F

    private Collider colUI;
    private Renderer quadRenderer;

    private bool isPlayerInside = false;
    private bool isPanelOpen = false;
    public GameObject player; // drag player ke sini di inspector


    void Start()
    {
        colUI = GetComponent<Collider>();
        if (colUI != null) colUI.isTrigger = true;

        quadRenderer = GetComponent<Renderer>();
        if (quadRenderer != null)
            quadRenderer.enabled = false;

        if (upgradePanel != null)
            upgradePanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.Instance.PlaySound2D("Upgrade");
            OpenPanel();
        }

        if (isPanelOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rcycle"))
        {
            isPlayerInside = true;

            if (quadRenderer != null)
                quadRenderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rcycle"))
        {
            isPlayerInside = false;

            if (quadRenderer != null)
                quadRenderer.enabled = false;
        }
    }

    public void OpenPanel()
    {
        if (upgradePanel == null) return;

        upgradePanel.SetActive(true);
        isPanelOpen = true;
        Time.timeScale = 0f;

        if (quadRenderer != null)
            quadRenderer.enabled = false;
    }

    public void ClosePanel()
    {
        SoundManager.Instance.PlaySound2D("Button");
        if (upgradePanel == null) return;

        upgradePanel.SetActive(false);
        isPanelOpen = false;
        Time.timeScale = 1f;

        if (isPlayerInside && quadRenderer != null)
            quadRenderer.enabled = true;
    }
}
