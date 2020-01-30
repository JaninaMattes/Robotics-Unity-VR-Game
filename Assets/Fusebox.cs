using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusebox : MonoBehaviour
{

    bool fuseStatus;
    bool circuitClosed;

    public Transform funkenLocation;

    public GameObject Funken;

    public GameObject Lever;

    public AudioSource SoundEffekt;
    public FuseboxDeckel fuseboxdeckel;
    public VoiceOverFolder voiceOverFolder;
    private bool audiospielt=false;
    private bool sicherungKaputt = false;
    private bool huch = false;
    private bool eineImRegal = false;
    private bool playHuch = false;
    private bool mistDieSicherung = false;
    private bool invoke = true;
    private bool invoke2 = true;
    private bool invoke3 = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        /*if (voiceOverFolder.voiceOver.isPlaying)
        {
            audiospielt = true;
        }
        else
        {
            audiospielt = false;
        }

        if (playHuch && audiospielt == false)
        {
            voiceOverFolder.PlayAudioClipDelayed("da_ist_der_radar_sensor", 0.2f);
            if (invoke)
            {
                Invoke("SetBool", 1.5f);
                invoke = false;
            }

        }

       else  if (playHuch == false)
        {
            voiceOverFolder.PlayAudioClipDelayed("test1", 1f);
            if (invoke2)
            {
                Invoke("SetBool1", 4f);
                invoke2 = false;
            }

        }

        if (mistDieSicherung == false)
        {
            voiceOverFolder.PlayAudioClipDelayed("da_war_doch_noch_eine_im_regal", 2f);
            fuseboxdeckel.FuseSicherungIcon.SetActive(true);

        }
        */
    }

    /*private void SetBool()
    {
        playHuch = false;
    }

    private void SetBool1()
    {
        mistDieSicherung = false;
    }
    private void SetBool2()
    {
        playHuch = false;
    }*/

    public void SetSicherungTrue()
    {
        fuseStatus = true;
    }

    public void SetSicherungFalse()
    {
        fuseStatus = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Is Triggered");
        if (other.gameObject== Lever)
        {
            Debug.Log("Is Lever");
            if (fuseStatus)
            {
                circuitClosed = true;
            }
            else
            {
                SoundEffekt.Play();
                Instantiate(Funken, funkenLocation.transform.position, funkenLocation.transform.rotation);
                fuseboxdeckel.FuseBoxHebelIcon.SetActive(false);
                //playHuch = true;

               
            }
        }
    }


}
