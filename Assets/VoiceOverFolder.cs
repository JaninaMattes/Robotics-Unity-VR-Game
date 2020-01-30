using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables;

public class VoiceOverFolder : MonoBehaviour
{

    public AudioSource voiceOver;
    public AudioClip[] allAudioClips;
    public Dictionary<string, AudioClip> allClips = new Dictionary<string, AudioClip>();
    public AudioClip currentClip;

    private void Start()
    {
        SetDictionairyAudioClips(this.allAudioClips);

    }


    private void SetDictionairyAudioClips(AudioClip[] audioClips)
    {
        foreach (AudioClip audioClip in allAudioClips)
        {
            if (audioClip != null)
            {
                allClips.Add(audioClip.name, audioClip);
            }
        }
    }



    public void PlayAudioClipDelayed(string audioClip, float delay)
    {
        AudioClip clip = allClips[audioClip] as AudioClip;
        currentClip = clip;
        voiceOver.clip = clip;
        voiceOver.PlayDelayed(delay);


    }
}
