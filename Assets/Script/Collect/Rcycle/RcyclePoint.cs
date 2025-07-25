using UnityEngine;
using TMPro;

public class RecyclePoint : MonoBehaviour
{
    [Header("Referensi")]
    public TextMeshProUGUI pointText;
    public int totalPoin = 0;

    public void TambahPoin(int jumlah)
    {
        totalPoin += jumlah;
        if (pointText != null)
            pointText.text = $"{totalPoin}";
    }

    public int GetTotalPoin() => totalPoin;
}
