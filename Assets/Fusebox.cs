using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using VRTK;

public class Fusebox : MonoBehaviour
{

    bool fuseStatus;
    bool circuitClosed;

    public Transform funkenLocation;

    public GameObject Funken;

    public GameObject Lever;

    public AudioSource SoundEffekt;
    public AudioSource SoundEffektHebel;
    //AudioSource Hebelsound;
    public FuseboxDeckel fuseboxdeckel;
    public VoiceOverFolder voiceOverFolder;
    float Audiolength;

    public VRTK_InteractableObject Sicherung;
    public Renderer[] lightRenderers;
    Color32[] emissionCol;
    public GameObject[] lights;

    public PostProcessVolume ppVolumeDark;

    bool wurdeGespielt = false;


    // Start is called before the first frame update
    void Start()
    {
        DisableEmission();  
        //Hebelsound = GetComponent<AudioSource>();
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
                SoundEffektHebel.Play();
                fuseboxdeckel.FuseSicherungSnap.SetActive(false);
                EnableEmission();
                Hebelicon();
                voiceOverFolder.PlayAudioClipDelayed("es_ist_so_ruhig_hier", 6f);
                Radioicon();

            }
            else
            {
                SoundEffektHebel.Play();
                DisableEmission();
                SoundEffekt.Play();
                Instantiate(Funken, funkenLocation.transform.position, funkenLocation.transform.rotation);
                Hebelicon();

                if (!wurdeGespielt)
                {
                    voiceOverFolder.PlayAudioClipDelayed("huch_sicherungkaputt_regal", 0.1f);
                    wurdeGespielt = true;
                }

                Audiolength = voiceOverFolder.currentClip.length;
                Invoke("Sicherungsicon",Audiolength);

            }
        }
    }

    private void Sicherungsicon()
    {
        fuseboxdeckel.FuseSicherungIcon.SetActive(true);
    }

    private void Hebelicon()
    {
        fuseboxdeckel.FuseBoxHebelIcon.SetActive(false);
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
        fuseboxdeckel.FuseSicherungSnap.SetActive(true);
    }

    void EnableEmission()
    {
        
        ppVolumeDark.weight = 0;
        for (int i = 0; i < lightRenderers.Length; i++)
        {
            //for (int n = 0; n < lightRenderers[i].materials.Length; n++)
            //{
            //    lightRenderers[i].materials[n].EnableKeyword("_EMISSION");
            //}
            Material[] mats = lightRenderers[i].materials;
            foreach (Material mat in mats)
            {
                Debug.Log("MatName: " + mat.name);
                mat.EnableKeyword("_EMISSION");
            }
        }
        for (int l = 0; l < lights.Length; l++)
        {
            lights[l].SetActive(true);

        }
    }

    void DisableEmission()
    {
        for (int i = 0; i < lightRenderers.Length; i++)
        {
            Debug.Log(lightRenderers[i].name);
            //for (int n = 0; n < lightRenderers[i].materials.Length; n++)
            //{
            //    lightRenderers[i].materials[n].DisableKeyword("_EMISSION");
            //}
            Material[] mats = lightRenderers[i].materials;
            foreach (Material mat in mats)
            {
                Debug.Log("MatName: " + mat.name);
                mat.DisableKeyword("_EMISSION");
            }
        }

        for (int l = 0; l < lights.Length; l++)
        {
            lights[l].SetActive(false);

        }

    }

}
