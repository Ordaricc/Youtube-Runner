using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Image muteButtonImage;

    [SerializeField] private Sprite audioOnSprite;
    [SerializeField] private Sprite audioOffSprite;

    public const string prefAudioMute = "prefAudioMute";

    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(prefAudioMute))
            AudioListener.volume = PlayerPrefs.GetFloat(prefAudioMute);

        UpdateMuteButtonImageSprite();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.isLoop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.volume = s.volume;

            if (s.playOnAwake)
                s.source.Play();
        }
    }

    private void UpdateMuteButtonImageSprite()
    {
        if (AudioListener.volume == 0)
            muteButtonImage.sprite = audioOffSprite;
        else
            muteButtonImage.sprite = audioOnSprite;

    }

    public void PlayClipByName(string _clipName)
    {
        Sound soundToPlay = Array.Find(sounds, dummySound => dummySound.clipName == _clipName);

        if (soundToPlay != null)
            soundToPlay.source.Play();
    }

    public void StopClipByName(string _clipName)
    {
        Sound soundToStop = Array.Find(sounds, dummySound => dummySound.clipName == _clipName);

        if (soundToStop != null)
            soundToStop.source.Stop();
    }

    //public void PlayClipByNameWithForeach(string _clipName)
    //{
    //    Sound soundToPlay = null;

    //    foreach (Sound s in sounds)
    //    {
    //        if (s.clipName == _clipName)
    //            soundToPlay = s;
    //    }
        
    //    if (soundToPlay != null)
    //        soundToPlay.source.Play();
    //}

    //public void StopClipByNameWithForeach(string _clipName)
    //{
    //    Sound soundToStop = null;

    //    foreach (Sound s in sounds)
    //    {
    //        if (s.clipName == _clipName)
    //            soundToStop = s;
    //    }

    //    if (soundToStop != null)
    //        soundToStop.source.Play();
    //}

    public void ToggleMute()
    {
        if (AudioListener.volume == 1)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;

        PlayerPrefs.SetFloat(prefAudioMute, AudioListener.volume);

        UpdateMuteButtonImageSprite();
    }
}