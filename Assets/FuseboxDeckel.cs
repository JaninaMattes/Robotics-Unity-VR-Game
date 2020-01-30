using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Controllables;

public class FuseboxDeckel : MonoBehaviour
{
    //--------Objektaktion---------
    VRTK_BaseControllable rotator;
    public GameObject FuseBoxDeckelIcon;
    public GameObject FuseBoxHebelIcon;
    public GameObject FuseSicherungIcon;
    public GameObject FuseSicherungSnap;
    public GameObject IconRadio;


    //--------Audio---------
    float Audiolength;
    public VoiceOverFolder voiceOverFolder;

    bool wurdeBewegt;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnEnable()
    {
        rotator = (rotator == null ? GetComponent<VRTK_BaseControllable>() : rotator);
        rotator.ValueChanged += Rotator_ValueChanged;
    }

    private void Rotator_ValueChanged(object sender, ControllableEventArgs e)
    {
        FuseBoxDeckelIcon.SetActive(false);
        if (!wurdeBewegt)
        {
            FuseBoxHebelIcon.SetActive(true);
            wurdeBewegt = true;
        }
    }


    private void OnDisable()
    {
        rotator = (rotator == null ? GetComponent<VRTK_BaseControllable>() : rotator);
        rotator.ValueChanged -= Rotator_ValueChanged;
    }

}
