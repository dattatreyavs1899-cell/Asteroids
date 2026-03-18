using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip shootSound;
    public AudioClip hitSound;
    public AudioClip breakSound;

    private AudioSource audioSource;

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f); 
        audioSource.PlayOneShot(clip, volume);
    }
}