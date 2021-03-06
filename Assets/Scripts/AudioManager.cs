using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if(instance == null) {
            instance = this;
		}else {
            Destroy(gameObject);
            return;
		}

        DontDestroyOnLoad(gameObject);

        // Add an AudioSource component to the gameObject for every sound
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            
            // Set the properties of the AudioSource
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;

        }        
    }

	private void Start() {
        Play("overworld-theme");
	}

	public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Could not find " + name + " to play!");
            return;
        }
        s.source.Play();
	}

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Could not find " + name + " to stop!");
            return;
        }
        s.source.Stop();
    }

    public void StopAll() {
        foreach (Sound s in sounds) {
            s.source.Stop();
        }
    }

    public void Pause(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Could not find " + name + " to pause!");
            return;
        }
        s.source.Pause();
    }

    public void PauseAll() {
        foreach (Sound s in sounds) {
            s.source.Pause();
        }
    }

    public void UnPause(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Could not find " + name + " to unpause!");
            return;
        }
        s.source.UnPause();
    }

    public void UnPauseAll() {
        foreach (Sound s in sounds) {
            s.source.UnPause();
        }
    }

    public void Mute(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Could not find " + name + " to mute!");
            return;
        }
        s.source.mute = true;
    }

    public void MuteAll() {
        foreach(Sound s in sounds) {
            s.source.mute = true;
		}
	}

    public void UnMute(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Could not find " + name + " to unmute!");
            return;
        }
        s.source.mute = false;
    }

    public void UnMuteAll() {
        foreach (Sound s in sounds) {
            s.source.mute = false;
        }
    }
}
