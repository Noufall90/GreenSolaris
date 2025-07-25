using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class RcycleChoosePoin : MonoBehaviour
{
    [Header("Point per item")]
    public int plastikValue = 2;
    public int kayuValue = 4;
    public int besiValue = 7;

    [Header("UI")]
    public TextMeshProUGUI plastikText, kayuText, besiText;
    public Button tambahPlastikBtn, kurangPlastikBtn, tukarPlastikBtn;
    public Button tambahKayuBtn, kurangKayuBtn, tukarKayuBtn;
    public Button tambahBesiBtn, kurangBesiBtn, tukarBesiBtn;

    public GameObject notifGagalUI;
    private Coroutine notifCoroutine;

    [Header("Referensi")]
    public RecyclePoint recyclePoint;
    public CollectItem collectItem;
    public TasSampah tasSampah;

    private int plastikPoin, kayuPoin, besiPoin;

    private void Start()
    {
        // Plastik
        tambahPlastikBtn.onClick.AddListener(() => Tambah(ref plastikPoin, plastikText));
        kurangPlastikBtn.onClick.AddListener(() => Kurang(ref plastikPoin, plastikText));
        tukarPlastikBtn.onClick.AddListener(() => Tukar("Plastik", plastikPoin, plastikValue, ref plastikPoin, plastikText));

        // Kayu
        tambahKayuBtn.onClick.AddListener(() => Tambah(ref kayuPoin, kayuText));
        kurangKayuBtn.onClick.AddListener(() => Kurang(ref kayuPoin, kayuText));
        tukarKayuBtn.onClick.AddListener(() => Tukar("Kayu", kayuPoin, kayuValue, ref kayuPoin, kayuText));

        // Besi
        tambahBesiBtn.onClick.AddListener(() => Tambah(ref besiPoin, besiText));
        kurangBesiBtn.onClick.AddListener(() => Kurang(ref besiPoin, besiText));
        tukarBesiBtn.onClick.AddListener(() => Tukar("Besi", besiPoin, besiValue, ref besiPoin, besiText));

        UpdateAllText();

        if (notifGagalUI != null)
            notifGagalUI.SetActive(false);
    }

    private void Tambah(ref int poin, TextMeshProUGUI text)
    {
        poin++;
        text.text = poin.ToString();
    }

    private void Kurang(ref int poin, TextMeshProUGUI text)
    {
        poin = Mathf.Max(0, poin - 1);
        text.text = poin.ToString();
    }

    private void Tukar(string jenis, int jumlahPoin, int nilaiPerItem, ref int counter, TextMeshProUGUI text)
    {
        if (jumlahPoin <= 0) return;

        int jumlahTersedia = collectItem.GetSampahJumlah(jenis);
        if (jumlahTersedia < jumlahPoin)
        {
            TampilkanNotif();
            return;
        }

        // Hitung total poin
        int total = jumlahPoin * nilaiPerItem;

        // Kurangi dari CollectItem
        collectItem.KurangiSampah(jenis, jumlahPoin);

        // Kurangi dari TasSampah
        tasSampah.KurangiBerat(jenis, jumlahPoin);

        // Tambah ke RecyclePoint
        recyclePoint.TambahPoin(total);

        // Reset UI poin lokal
        counter = 0;
        text.text = "0";
    }

    private void UpdateAllText()
    {
        plastikText.text = plastikPoin.ToString();
        kayuText.text = kayuPoin.ToString();
        besiText.text = besiPoin.ToString();
    }

    private void TampilkanNotif()
    {
        if (notifGagalUI == null) return;

        if (notifCoroutine != null)
            StopCoroutine(notifCoroutine);

        notifCoroutine = StartCoroutine(NotifikasiSebentar(1f));
    }
    
    private IEnumerator NotifikasiSebentar(float durasi)
    {
        notifGagalUI.SetActive(true);
        yield return new WaitForSecondsRealtime(durasi);
        notifGagalUI.SetActive(false);
    }

    private void SembunyikanNotif()
    {
        notifGagalUI.SetActive(false);
    }
}
