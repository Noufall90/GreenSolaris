using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    [Header("Panel Quest")]
    public GameObject panelQuest;

    private bool isPanelOpen = false;

    void Start()
    {
        // Pastikan panel quest tertutup saat mulai
        if (panelQuest != null)
            panelQuest.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isPanelOpen = !isPanelOpen;

            if (panelQuest != null)
                panelQuest.SetActive(isPanelOpen);
        }
    }
}
