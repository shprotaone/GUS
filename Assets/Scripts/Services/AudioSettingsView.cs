using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsView : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Button _SFXButton;
    [SerializeField] private Button _musicButton;

    [SerializeField] private Sprite[] _musicSprite;
    [SerializeField] private Sprite[] _soundSprite;

    private AudioSettings _audioService;

    private void Start()
    {
        _audioService = new AudioSettings(_audioMixer);       
        _SFXButton.onClick.AddListener(SwitchSFX);
        _musicButton.onClick.AddListener(SwitchMusic);

        _audioService.LoadSettings();
        ChangeButton(_audioService.SFXVal, false);
        ChangeButton(_audioService.MusicVal, true);
    }

    private void SwitchSFX()
    {
        _audioService.SwitchSFX();
        ChangeButton(_audioService.SFXVal, false);
    }

    private void SwitchMusic()
    {
        _audioService.SwitchMusic();
        ChangeButton(_audioService.MusicVal, true);
    }

    private void ChangeButton(float value, bool isMusic)
    {
        if (value == _audioService.lowerBound && isMusic)
        {
            //_musicButton.image.sprite = _musicSprite[0];
            _musicButton.image.color = Color.red;
        }
        else if (value == _audioService.upperBound && isMusic)
        {
            //_musicButton.image.sprite = _musicSprite[1];
            _musicButton.image.color = Color.green;
        }
        else if (value == _audioService.lowerBound && !isMusic)
        {
            //_SFXButton.image.sprite = _soundSprite[0];
            _SFXButton.image.color = Color.red;
        }
        else if (value == _audioService.upperBound && !isMusic)
        {
            //_SFXButton.image.sprite = _soundSprite[1];
            _SFXButton.image.color = Color.green;
        }

        PlayerPrefs.Save();
    }
}
