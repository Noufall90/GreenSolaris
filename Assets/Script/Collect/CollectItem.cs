using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CollectItem : MonoBehaviour
{
    [Header("Animasi")]
    public Animator playerAnimator; // Drag animator pemain ke sini

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
            SoundManager.Instance.PlaySound2D("Pickup");
            string tipe = currentSampah.tipeSampah;

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

                    if (playerAnimator != null)
                        playerAnimator.SetTrigger("TakingItem");

                    Destroy(currentSampah.gameObject);
                    currentSampah = null;
                }
                else
                {
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
