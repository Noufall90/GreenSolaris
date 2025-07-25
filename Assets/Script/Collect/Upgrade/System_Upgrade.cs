using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class System_Upgrade : MonoBehaviour
{
    [Header("Referensi")]
    public TasSampah tasSampah;
    public CollectItem collectItem;
    public RecyclePoint recyclePoint;

    [Header("Upgrade Buttons")]
    public Button level1Btn;
    public Button level2Btn;
    public Button level3Btn;

    [Header("UI")]
    public GameObject infoText;
    [Header("Debug")]
    public Button resetButton;


    private int currentLevel = 0;

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("TasLevel", 0);
        SetKapasitas(currentLevel);
        UpdateUpgradeButtons();

        level1Btn.onClick.AddListener(() => TryUpgrade(1));
        level2Btn.onClick.AddListener(() => TryUpgrade(2));
        level3Btn.onClick.AddListener(() => TryUpgrade(3));

        resetButton.onClick.AddListener(ResetUpgradeDebug);

    }

    private void Update()
    {
        UpdateUpgradeButtons();
    }

    private void TryUpgrade(int targetLevel)
    {
        if (currentLevel + 1 != targetLevel) return; // hanya bisa upgrade ke level berikutnya

        var req = GetRequirement(currentLevel);
        if (BisaUpgrade(req))
        {
            // Kurangi resource
            collectItem.KurangiSampah("Plastik", req.plastik);
            collectItem.KurangiSampah("Kayu", req.kayu);
            collectItem.KurangiSampah("Besi", req.besi);
            recyclePoint.TambahPoin(-req.poin);

            // Naikkan level
            currentLevel++;
            PlayerPrefs.SetInt("TasLevel", currentLevel);
            SetKapasitas(currentLevel);
            UpdateUpgradeButtons();
        }
    }

    void SetKapasitas(int level)
    {
        int[] kapasitas = { 5, 10, 20, 35 };
        tasSampah.kapasitasMaks = kapasitas[Mathf.Clamp(level, 0, kapasitas.Length - 1)];
        tasSampah.KosongkanTas(); // reset isi agar tidak over
    }

    void UpdateUpgradeButtons()
    {
        // Get all requirements
        var req1 = GetRequirement(0);
        var req2 = GetRequirement(1);
        var req3 = GetRequirement(2);

        // Set interactable based on current level
        level1Btn.interactable = currentLevel == 0 && BisaUpgrade(req1);
        level2Btn.interactable = currentLevel == 1 && BisaUpgrade(req2);
        level3Btn.interactable = currentLevel == 2 && BisaUpgrade(req3);

        // Optional: Update label teks pada tombol atau ganti warna background (misal hijau/abu)
        UpdateButtonVisual(level1Btn, 1);
        UpdateButtonVisual(level2Btn, 2);
        UpdateButtonVisual(level3Btn, 3);
    }

    void UpdateButtonVisual(Button btn, int level)
    {
        var txt = btn.GetComponentInChildren<TextMeshProUGUI>();
        if (currentLevel >= level)
            txt.text = $"LEVEL {level}";
        else if (currentLevel + 1 == level)
            txt.text = $"TINGKATKAN{level}";
        else
            txt.text = $"TERKUNCI";
    }

    bool BisaUpgrade((int poin, int plastik, int kayu, int besi) req)
    {
        return recyclePoint.GetTotalPoin() >= req.poin &&
               collectItem.GetSampahJumlah("Plastik") >= req.plastik &&
               collectItem.GetSampahJumlah("Kayu") >= req.kayu &&
               collectItem.GetSampahJumlah("Besi") >= req.besi;
    }

    (int poin, int plastik, int kayu, int besi) GetRequirement(int level)
    {
        switch (level)
        {
            case 0: return (35, 5, 0, 0);
            case 1: return (70, 7, 3, 1);
            case 2: return (120, 10, 5, 3);
            default: return (0, 0, 0, 0);
        }
    }
    private void ResetUpgradeDebug()
    {
        PlayerPrefs.DeleteKey("TasLevel");
        currentLevel = 0;
        SetKapasitas(currentLevel);
        UpdateUpgradeButtons();

        Debug.Log("<color=yellow>Upgrade telah direset ke level 0.</color>");
    }
}
