using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Collection of most important methods regarding the AudioSource component
/// Unity Docs AudioSource -> https://docs.unity3d.com/ScriptReference/AudioSource.html
/// </summary>

public class PlayAudioMethods : MonoBehaviour
{

    [Header("Audio Source")]
    public AudioSource audioSource;
    [Header("Audio Clip")] // Audio clip needs to be played via audioSource. an audiosource contains an audioclip (example mp3)
    public AudioClip audioClip;
    [Header("Delay on Play")]
    public float delayTime = 0;
    [Header("Volume")]
    public float volumeScale = 0.5f;
    public float volume = 1f;
    [Header("Play Audio Clip at Point in Worldspace")]
    public Vector3 audioClipPosition = new Vector3();
    [Header("Example Object to add an AudioSource via Script")]
    public GameObject addAudioSource;

    //Plays the audioclip of the audioSource
    private void PlayAudioSource()
    {
        this.audioSource.Play();
    }

    //Plays the audioclip of the audioSource with a delay in seconds
    private void PlayAudioSourceDelayed()
    {
        this.audioSource.PlayDelayed(this.delayTime);
    }

    //Plays the audioclip of the auioSource on Awake
    private void PlayAudioSourceOnAwake()
    {
        this.audioSource.playOnAwake = true;
    }

    //Plays an AudioClip, and scales the AudioSource volume by volumeScale.
    private void PlayAudioSourceOneShot()
    {
        this.audioSource.PlayOneShot(this.audioClip, this.volumeScale);
    }

    //Pauses the audioclip of the audioSource 
    private void PauseAudioSource()
    {
        this.audioSource.Pause();
    }

    //UnPauses the audioclip of the audioSource
    private void UnPauseAudioSource()
    {
        this.audioSource.UnPause();
    }

    //Stops the audioclip of the audioSource 
    private void StopAudioSource()
    {
        this.audioSource.Stop();
    }

    //Mutes the AudioSource by setting Volume to 0
    private void MuteAudioSource()
    {
        this.audioSource.mute = true;
    }

    //Unmutes the AudioSource by setting Volume back to the set volume.
    private void UnmuteAudioSource()
    {
        this.audioSource.mute = false;
    }

    //Sets the audioSource to play the audioclip in loop
    private void LoopAudioSource()
    {
        this.audioSource.loop = true;
    }

    //Returns true if the AudioSource is playing.
    private bool IsAudioSourcePlaying()
    {
        if (this.audioSource.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Plays an AudioClip at a given position in world space and automatically disposes it once the clip has finished playing (no garbage)
    private void PlayClipAtPosition()
    {
        AudioSource.PlayClipAtPoint(this.audioClip, this.audioClipPosition, this.volume);
    }

    //Add AudioSource to an example GameObject and setup an example audioClip and volume etc.
    private void AddAudioSourceToGameObject()
    {
        this.addAudioSource.AddComponent<AudioSource>();
        SetAudioClip(this.addAudioSource.GetComponent<AudioSource>(), this.audioClip);
        SetVolume(addAudioSource, this.volume);
    }

    private void SetAudioClip(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
    }

    private void SetVolume(GameObject addAudioSource, float volume)
    {
        addAudioSource.GetComponent<AudioSource>().volume = volume;
    }

}
