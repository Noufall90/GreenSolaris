using UnityEngine;

public class Rcycle_UI : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject rcyclePanel;     // Panel UI yang akan dibuka saat tekan F

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

        if (rcyclePanel != null)
            rcyclePanel.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.Instance.PlaySound2D("Rcycle");
            OpenPanel();
        }

        if (isPanelOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.Instance.PlaySound2D("Button");
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
        if (rcyclePanel == null) return;

        rcyclePanel.SetActive(true);
        isPanelOpen = true;
        Time.timeScale = 0f;

        if (quadRenderer != null)
            quadRenderer.enabled = false;
    }

    public void ClosePanel()
    {
        SoundManager.Instance.PlaySound2D("Button");
        if (rcyclePanel == null) return;

        rcyclePanel.SetActive(false);
        isPanelOpen = false;
        Time.timeScale = 1f;

        if (isPlayerInside && quadRenderer != null)
            quadRenderer.enabled = true;
    }
}
