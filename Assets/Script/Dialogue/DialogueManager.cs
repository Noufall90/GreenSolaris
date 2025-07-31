using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI fontUtama;
    public TextMeshProUGUI fontEmoji;
    public GameObject dialogBox;

    private Queue<DialogueLine> lines;
    private DialogueLine currentLine;
    private TextMeshProUGUI activeFont;
    private string fullSentence;
    private Coroutine typingCoroutine;

    public bool isDialogueActive = false;
    private bool isTyping = false;
    public float typingSpeed = 0.02f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();

        if (dialogBox != null)
            dialogBox.SetActive(false);
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.R))
        {
            if (isTyping)
            {
                // Langsung tampilkan seluruh kalimat
                ShowFullSentence();
            }
            else
            {
                // Lanjut ke dialog berikutnya
                DisplayNextDialogueLine();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        lines.Clear();
        dialogBox.SetActive(true);

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }


    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue();
        characterName.text = currentLine.character.name;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        fontUtama.gameObject.SetActive(false);
        fontEmoji.gameObject.SetActive(false);

        isTyping = true;

        if (dialogueLine.utama_Dialogue && !dialogueLine.emoji_Dialogue)
        {
            activeFont = fontUtama;
        }
        else if (!dialogueLine.utama_Dialogue && dialogueLine.emoji_Dialogue)
        {
            activeFont = fontEmoji;
        }
        else if (dialogueLine.utama_Dialogue && dialogueLine.emoji_Dialogue)
        {
            Debug.LogWarning("Dua font dipilih, gunakan font utama sebagai default.");
            activeFont = fontUtama;
        }
        else
        {
            Debug.LogWarning("Tidak ada font dipilih, gunakan font utama sebagai default.");
            activeFont = fontUtama;
        }

        activeFont.gameObject.SetActive(true);
        activeFont.text = "";
        fullSentence = dialogueLine.line;

        foreach (char letter in fullSentence)
        {
            activeFont.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void ShowFullSentence()
    {
        if (activeFont != null)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            activeFont.text = fullSentence;
            isTyping = false;
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        fontUtama.gameObject.SetActive(false);
        fontEmoji.gameObject.SetActive(false);
        characterName.text = "";
        dialogBox.SetActive(false);
    }
}
