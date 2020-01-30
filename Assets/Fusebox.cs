using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

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
    float Audiolength;

    public VRTK_InteractableObject Sicherung;
    public Renderer[] lightMaterials;
    Color32[] emissionCol;


    // Start is called before the first frame update
    void Start()
    {
        DisableEmission();       
    }



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
                EnableEmission();
                fuseboxdeckel.FuseBoxHebelIcon.SetActive(false);
                voiceOverFolder.PlayAudioClipDelayed("es_ist_so_ruhig_hier", 6f);
                Radioicon();

            }
            else
            {
                DisableEmission();
                SoundEffekt.Play();
                Instantiate(Funken, funkenLocation.transform.position, funkenLocation.transform.rotation);
                fuseboxdeckel.FuseBoxHebelIcon.SetActive(false);
                //playHuch = true;
                voiceOverFolder.PlayAudioClipDelayed("huch_sicherungkaputt_regal", 0.1f);
                Audiolength = voiceOverFolder.currentClip.length;
                Invoke("Sicherungsicon",Audiolength);

            }
        }
    }

    private void Sicherungsicon()
    {
        fuseboxdeckel.FuseSicherungIcon.SetActive(true);
    }


    private void Radioicon()
    {
        fuseboxdeckel.IconRadio.SetActive(true);
    }


    private void OnEnable()
    {
        Sicherung = (Sicherung == null ? GetComponent<VRTK_InteractableObject>() : Sicherung);

        if (Sicherung != null)
        {
            Sicherung.InteractableObjectGrabbed += Sicherung_InteractableObjectGrabbed; ;
            Sicherung.InteractableObjectUngrabbed += Sicherung_InteractableObjectUngrabbed; ;
        }
    }


    private void OnDisable()
    {
        if (Sicherung != null)
        {
            Sicherung.InteractableObjectGrabbed -= Sicherung_InteractableObjectGrabbed; ;
            Sicherung.InteractableObjectUngrabbed -= Sicherung_InteractableObjectUngrabbed; ;
        }
    }

    private void Sicherung_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
    }

    private void Sicherung_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        fuseboxdeckel.FuseSicherungIcon.SetActive(false);
    }

    void EnableEmission()
    {
        for (int i = 0; i < lightMaterials.Length; i++)
        {
            lightMaterials[i].material.EnableKeyword("_EMISSION");
        }
    }

    void DisableEmission()
    {
        for (int i = 0; i < lightMaterials.Length; i++)
        {
            lightMaterials[i].material.DisableKeyword("_EMISSION");
        }
    }

}
