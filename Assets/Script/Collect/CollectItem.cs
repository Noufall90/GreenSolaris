using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CollectItem : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI plastikText;
    public TextMeshProUGUI kayuText;
    public TextMeshProUGUI besiText;

    [Header("Tas")]
    public TasSampah tasSampah;

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

            // Cek apakah bisa ditambah
            if (tasSampah != null)
            {
                if (tasSampah.BisaMenambah(tipe))
                {
                    tasSampah.TambahSampah(tipe);

                    if (sampahCounter.ContainsKey(tipe))
                    {
                        sampahCounter[tipe]++;
                        UpdateUI();
                    }

                    Destroy(currentSampah.gameObject);
                    currentSampah = null;
                }
                else
                {
                    // Penuh → Tampilkan notifikasi selama 2 detik
                    tasSampah.TampilkanNotifikasiSementara(2f);
                    Debug.Log("Tas penuh! Tidak bisa ambil.");
                }
            }
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

    public int GetSampahJumlah(string tipe)
    {
        return sampahCounter.ContainsKey(tipe) ? sampahCounter[tipe] : 0;
    }

    public void ResetSampah()
    {
        sampahCounter["Plastik"] = 0;
        sampahCounter["Kayu"] = 0;
        sampahCounter["Besi"] = 0;
        UpdateUI();
    }
    public void KurangiSampah(string tipe, int jumlah)
    {
        if (sampahCounter.ContainsKey(tipe))
        {
            sampahCounter[tipe] = Mathf.Max(0, sampahCounter[tipe] - jumlah);
            UpdateUI();
        }
    }

}
