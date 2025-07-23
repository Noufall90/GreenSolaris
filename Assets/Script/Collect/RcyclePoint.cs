using UnityEngine;
using TMPro;

public class RecyclePoint : MonoBehaviour
{
    [Header("Referensi")]
    public TextMeshProUGUI pointText;
    public Collider recycleTrigger;
    public int totalPoin = 0;

    [Header("Poin per Jenis Sampah")]
    public int plastikPoint = 2;
    public int kayuPoint = 4;
    public int besiPoint = 7;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rcycle"))
        {
            CollectItem collectItem = other.GetComponentInParent<CollectItem>();
            TasSampah tasSampah = collectItem != null ? collectItem.tasSampah : null;

            if (collectItem != null)
            {
                TambahkanPoinDariSampah(collectItem);

                if (tasSampah != null)
                    tasSampah.KosongkanTas();
            }
        }
    }

    private void TambahkanPoinDariSampah(CollectItem collectItem)
    {
        int plastik = collectItem.GetSampahJumlah("Plastik");
        int kayu = collectItem.GetSampahJumlah("Kayu");
        int besi = collectItem.GetSampahJumlah("Besi");

        int hasilPoin = plastik * plastikPoint + kayu * kayuPoint + besi * besiPoint;
        totalPoin += hasilPoin;

        if (pointText != null)
            pointText.text = $"Poin: {totalPoin}";

        collectItem.ResetSampah();
    }
}
