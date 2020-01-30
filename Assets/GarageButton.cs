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
    AudioSource ButtonSound;



    public GameObject TorButtonIcon;
    public GameObject FuseBoxDeckelIcon;
    float Audiolength;
    public VoiceOverFolder voiceOverFolder;



    private void Start()
    {
        StartpositionTor = Garagentor.transform.position;
        Invoke("EndlichZuhause", 3f);
        ButtonSound= GetComponent<AudioSource>();
    }

    private void EndlichZuhause()
    {
        voiceOverFolder.PlayAudioClipDelayed("endlich_wieder_zuhause",0);

        

        Audiolength = voiceOverFolder.currentClip.length;

        Invoke("IconEinblendung", Audiolength);
    }

    private void IconEinblendung()
    {
        TorButtonIcon.SetActive(true);
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
        ButtonSound.Play();


        voiceOverFolder.PlayAudioClipDelayed("ich_mach_erstmal_das_licht_wieder_an", 2f);
        FuseBoxDeckelIcon.SetActive(true);
    }

    private void Slider_MinLimitReached(object sender, ControllableEventArgs e)
    {
        Debug.Log("Min Limit Reached" + "TorPosition"  + Garagentor.transform.position);

    }


}

