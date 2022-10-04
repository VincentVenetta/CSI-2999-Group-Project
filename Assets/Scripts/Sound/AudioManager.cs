using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    ///<Summary>Play a sound specified by name.</Summary>
    public void Play(string name)
    {
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == name);

        if (soundToPlay == null)
        {
            Debug.LogWarning("Sound with name: " + name + " not found!");
            return;
        }

        soundToPlay.source.Play();
    }
}
