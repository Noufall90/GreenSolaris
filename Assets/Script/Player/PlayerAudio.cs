using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public string walkSoundName = "Walk";
    public float walkStepInterval = 0.4f;
    public float runStepInterval = 0.25f;

    private float stepTimer;
    private bool isMoving = false;
    private bool isRunning = false;

    void Update()
    {
        // Deteksi apakah bergerak
        isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                   Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
                   Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) ||
                   Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow);

        // Deteksi apakah sedang lari (tahan Shift)
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Jika bergerak
        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            float currentInterval = isRunning ? runStepInterval : walkStepInterval;

            if (stepTimer <= 0f)
            {
                SoundManager.Instance.PlaySound2D(walkSoundName);
                stepTimer = currentInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}
