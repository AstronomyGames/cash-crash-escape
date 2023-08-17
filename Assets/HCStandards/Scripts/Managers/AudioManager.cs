using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip spreading;

    protected override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip)
    {
        if (!HCStandards.Audio.isAudioEnabled)
            return;

        audioSource.PlayOneShot(clip);
    }
}
