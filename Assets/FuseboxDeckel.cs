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
    public GameObject IconRadio;


    //--------Audio---------
    float Audiolength;
    public VoiceOverFolder voiceOverFolder;
    //public VRTK_BaseControllable ; 
    //public VRTK_BaseControllable;



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
        //throw new System.NotImplementedException();

        FuseBoxDeckelIcon.SetActive(false);
        FuseBoxHebelIcon.SetActive(true);


        Debug.Log("ValueChanged");
    }


    private void OnDisable()
    {
        rotator = (rotator == null ? GetComponent<VRTK_BaseControllable>() : rotator);
        rotator.ValueChanged -= Rotator_ValueChanged;
    }

}
