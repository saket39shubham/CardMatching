using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource source;

    [Header("Clips")]
    [SerializeField] private AudioClip flip;
    [SerializeField] private AudioClip match;
    [SerializeField] private AudioClip mismatch;
    [SerializeField] private AudioClip gameover;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (source == null)
            source = gameObject.AddComponent<AudioSource>();

        source.playOnAwake = false;
    }

    public void PlayFlip()
    {
        if (flip) source.PlayOneShot(flip);
    }

    public void PlayMatch()
    {
        if (match) source.PlayOneShot(match);
    }

    public void PlayMismatch()
    {
        if (mismatch) source.PlayOneShot(mismatch);
    }

    public void PlayGameOver()
    {
        if (gameover) source.PlayOneShot(gameover);
    }
}