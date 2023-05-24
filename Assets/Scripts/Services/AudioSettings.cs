using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings
{
    public readonly string musicName = "Music";
    public readonly string SFXName = "SFX";

    public readonly float lowerBound = -80;
    public readonly float upperBound = 0;

    public float MusicVal { get; private set; }
    public float SFXVal {get; private set; }

    private AudioMixer _audioMixer;
    public AudioSettings(AudioMixer mixer)
    {
        _audioMixer = mixer;
    }

    public void LoadSettings()
    {
        MusicVal = PlayerPrefs.GetFloat(musicName,upperBound);
        SFXVal = PlayerPrefs.GetFloat(SFXName,upperBound);

        _audioMixer.SetFloat(musicName, MusicVal);
        _audioMixer.SetFloat(SFXName, SFXVal);
    }

    public void SwitchMusic()
    {
        if (MusicVal == lowerBound) MusicVal = upperBound;
        else MusicVal = lowerBound;

        _audioMixer.SetFloat(musicName, MusicVal);
        PlayerPrefs.SetFloat(musicName, MusicVal);
    }

    public void SwitchSFX()
    {
        if (SFXVal == lowerBound) SFXVal = upperBound;
        else SFXVal = lowerBound;

        _audioMixer.SetFloat(SFXName, lowerBound);
        PlayerPrefs.SetFloat(SFXName, SFXVal);
    }

    
}
