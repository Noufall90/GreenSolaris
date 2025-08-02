using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField]
    private MusicLibrary musicLibrary;
    [SerializeField]
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        PlayMusic(sceneName); // 🎵 otomatis mainkan lagu sesuai nama scene
    }

    public void PlayMusic(string trackName, float fadeDuration = 0.5f)
    {
        AudioClip clip = musicLibrary.GetClipFromName(trackName);
        if (clip != null)
        {
            StartCoroutine(AnimateMusicCrossfade(clip, fadeDuration));
        }
        else
        {
            Debug.LogWarning($"[MusicManager] Track '{trackName}' tidak ditemukan di MusicLibrary.");
        }
    }

    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime / fadeDuration;
            musicSource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;
        }

        musicSource.clip = nextTrack;
        musicSource.Play();

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;
        }
    }
}
