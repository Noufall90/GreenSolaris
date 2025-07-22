using UnityEngine;

public class Sampah_UI : MonoBehaviour
{
    private Collider colUI;
    private Renderer quadRenderer;

    void Start()
    {
        colUI = GetComponent<Collider>();
        if (colUI != null) colUI.isTrigger = true;

        quadRenderer = GetComponent<Renderer>();
        if (quadRenderer != null) quadRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (quadRenderer != null) quadRenderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (quadRenderer != null) quadRenderer.enabled = false;
        }
    }
}
