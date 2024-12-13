using System;
using System.Collections;
using System.Collections.Generic;
using Project.Interfaces.Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VolumeController : MonoBehaviour
{
    private const string MusicKey = nameof(MusicKey);
    private const string EffectsKey = nameof(EffectsKey);
    
    [SerializeField] private Scrollbar _volumeScrollbar;
    
    private IAudioService _audioService;

    [Inject]
    public void Construct(IAudioService audioService)
    {
        _audioService = audioService;
    }

    public void SetMusicVolume()
    {
        _audioService.SetMusicLevel(_volumeScrollbar.value);
        Save(MusicKey);
    }

    public void SetEffectsVolume()
    {
        _audioService.SetEffectsVolume(_volumeScrollbar.value);
        Save(EffectsKey);
    }

    private void Save(string key)
    {
        PlayerPrefs.SetFloat(key, _volumeScrollbar.value);
        PlayerPrefs.Save();
    }
}