using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;

    [Tooltip("Centang jika ini adalah dialog utama (text biasa)")]
    public bool utama_Dialogue;

    [Tooltip("Centang jika ini adalah dialog emoji")]
    public bool emoji_Dialogue;

    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private bool playerInRange = false;
    private bool dialogueStarted = false;

    private Renderer quadRenderer;
    public Animator npcAnimator; 

    private void Start()
    {
        quadRenderer = GetComponent<Renderer>();
        if (quadRenderer != null)
            quadRenderer.enabled = false;
    }

    private void Update()
    {
        if (playerInRange && !dialogueStarted && Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.Instance.StartDialogue(dialogue);
            dialogueStarted = true;
            SoundManager.Instance.PlaySound2D("Dialogue");
            SoundManager.Instance.PlaySound2D("SuaraNPC");

            if (npcAnimator != null)
                npcAnimator.SetBool("Talking", true);
        }

        if (dialogueStarted && !DialogueManager.Instance.isDialogueActive)
        {
            dialogueStarted = false;

            if (npcAnimator != null)
                npcAnimator.SetBool("Talking", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (quadRenderer != null)
                quadRenderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (quadRenderer != null)
                quadRenderer.enabled = false;
        }
    }
} 