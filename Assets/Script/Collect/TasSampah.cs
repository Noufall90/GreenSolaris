using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TasSampah : MonoBehaviour
{
    public int kapasitasMaks = 5;

    [Header("UI")]
    public Slider sliderUI;
    public TextMeshProUGUI teksNilaiUI;
    public GameObject notifTasPenuhUI;

    private int totalBeratSekarang = 0;
    private Coroutine notifCoroutine;

    private Dictionary<string, int> beratPerItem = new Dictionary<string, int>()
    {
        { "Plastik", 1 },
        { "Kayu", 3 },
        { "Besi", 5 }
    };

    private void Start()
    {
        if (notifTasPenuhUI != null)
            notifTasPenuhUI.SetActive(false);

        UpdateUI();
    }

    public bool BisaMenambah(string tipe)
    {
        if (!beratPerItem.ContainsKey(tipe)) return false;

        int berat = beratPerItem[tipe];
        return totalBeratSekarang + berat <= kapasitasMaks;
    }

    public void TambahSampah(string tipe)
    {
        if (!beratPerItem.ContainsKey(tipe)) return;

        int berat = beratPerItem[tipe];
        totalBeratSekarang += berat;
        UpdateUI();
    }

    public void KosongkanTas()
    {
        totalBeratSekarang = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (sliderUI != null)
        {
            sliderUI.maxValue = kapasitasMaks;
            sliderUI.value = totalBeratSekarang;
        }

        if (teksNilaiUI != null)
        {
            teksNilaiUI.text = $"{totalBeratSekarang} / {kapasitasMaks}";
        }
    }

    // Notifikasi tas penuh sementara
    public void TampilkanNotifikasiSementara(float durasi)
    {
        if (notifTasPenuhUI == null) return;

        if (notifCoroutine != null)
            StopCoroutine(notifCoroutine);

        notifCoroutine = StartCoroutine(TampilkanNotifikasiCoroutine(durasi));
    }

    private IEnumerator TampilkanNotifikasiCoroutine(float durasi)
    {
        notifTasPenuhUI.SetActive(true);
        yield return new WaitForSeconds(durasi);
        notifTasPenuhUI.SetActive(false);
    }
}
