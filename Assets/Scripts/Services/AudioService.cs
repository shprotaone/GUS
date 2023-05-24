using UnityEngine;

public class AudioService : MonoBehaviour
{
    [SerializeField] private AudioData _audioData;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;

    public AudioData Data => _audioData;

    public void PlayMusic(AudioClip clip) => _musicSource.PlayOneShot(clip);
    public void PlaySFX(AudioClip clip) => _sfxSource.PlayOneShot(clip);
    public void StopMusic() => _musicSource.Stop();
    public void Pause() => _musicSource.Pause();
    public void Resume() => _musicSource.UnPause();
}
