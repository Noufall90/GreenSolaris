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

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;
    public float typingSpeed = 0.02f;

    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.R))
        {
            DisplayNextDialogueLine();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        animator.Play("show");

        lines.Clear();

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

        DialogueLine currentLine = lines.Dequeue();
        characterName.text = currentLine.character.name;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        fontUtama.gameObject.SetActive(false);
        fontEmoji.gameObject.SetActive(false);

        TextMeshProUGUI activeFont = null;

        if (dialogueLine.utama_Dialogue)
            activeFont = fontUtama;
        else if (dialogueLine.emoji_Dialogue)
            activeFont = fontEmoji;
        else
        {
            Debug.LogWarning("Tidak ada font yang dipilih di DialogueLine.");
            yield break;
        }

        activeFont.gameObject.SetActive(true);
        activeFont.text = "";

        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            activeFont.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("hide");
        fontUtama.text = "";
        fontEmoji.text = "";
    }
}
