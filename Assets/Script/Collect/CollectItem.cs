using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CollectItem : MonoBehaviour
{
    public TextMeshProUGUI plastikText;
    public TextMeshProUGUI kayuText;
    public TextMeshProUGUI besiText;

    private Dictionary<string, int> sampahCounter = new Dictionary<string, int>()
    {
        { "Plastik", 0 },
        { "Kayu", 0 },
        { "Besi", 0 }
    };

    private SampahItem currentSampah;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (currentSampah != null && Input.GetKeyDown(KeyCode.F))
        {
            string tipe = currentSampah.tipeSampah;
            if (sampahCounter.ContainsKey(tipe))
            {
                sampahCounter[tipe]++;
                UpdateUI();
            }
            Destroy(currentSampah.gameObject);
            currentSampah = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sampah"))
        {
            currentSampah = other.GetComponent<SampahItem>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sampah") && other.GetComponent<SampahItem>() == currentSampah)
        {
            currentSampah = null;
        }
    }

    private void UpdateUI()
    {
        plastikText.text = $"{sampahCounter["Plastik"]}";
        kayuText.text = $"{sampahCounter["Kayu"]}";
        besiText.text = $"{sampahCounter["Besi"]}";
    }
}
