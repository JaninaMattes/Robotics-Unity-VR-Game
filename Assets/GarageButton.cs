using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables;

public class GarageButton : MonoBehaviour
    {

        public GameObject Garagentor;
        VRTK_BaseControllable slider;
        public float speed;
        Vector3 StartpositionTor;
        public Vector3 EndpositionTor;
        public AudioSource voiceOver;
        public AudioClip[] allAudioClips;
        public Dictionary<string, AudioClip> allClips = new Dictionary<string, AudioClip>();
    private AudioClip currentClip;

    public GameObject TorButtonIcon;
    float Audiolength;



    private void Start()
    {
        SetDictionairyAudioClips(this.allAudioClips);
        StartpositionTor = Garagentor.transform.position;
        Invoke("EndlichZuhause", 3f);

        //PlayAudioClipDelayed("endlich_wieder_zuhause", 2);
    }

    private void Update()
    {

    }


    private void EndlichZuhause()
    {
        PlayAudioClipDelayed("endlich_wieder_zuhause");

        Audiolength = currentClip.length;

        Invoke("IconEinblendung", Audiolength);
    }

    private void IconEinblendung()
    {
        TorButtonIcon.SetActive(true);
    }

    private void SetDictionairyAudioClips(AudioClip[] audioClips)
    {
        foreach(AudioClip audioClip in allAudioClips)
        {
            if(audioClip != null)
            {
                allClips.Add(audioClip.name, audioClip);
            }
        }
    }



    public void PlayAudioClipDelayed(string audioClip)
    {
            AudioClip clip = allClips[audioClip] as AudioClip;
            currentClip = clip;
            voiceOver.clip = clip;
            voiceOver.Play();
    }

    private void OnEnable()
    {
        slider = (slider == null ? GetComponent<VRTK_BaseControllable>() : slider);
        slider.MaxLimitReached += Slider_MaxLimitReached;
        slider.MinLimitReached += Slider_MinLimitReached;
    }

    

    private void OnDisable()
    {
        slider = (slider == null ? GetComponent<VRTK_BaseControllable>() : slider);
        slider.MaxLimitReached -= Slider_MaxLimitReached;
        slider.MinLimitReached -= Slider_MinLimitReached;
    }

    private void Slider_MaxLimitReached(object sender, ControllableEventArgs e)
    {
        Debug.Log("Max Limit Reached");

        iTween.MoveTo(Garagentor, iTween.Hash("position", StartpositionTor + EndpositionTor, "speed", speed, "easetype", iTween.EaseType.easeOutBounce));

        TorButtonIcon.SetActive(false);



    }

    private void Slider_MinLimitReached(object sender, ControllableEventArgs e)
    {
        Debug.Log("Min Limit Reached" + "TorPosition"  + Garagentor.transform.position);

    }


}

