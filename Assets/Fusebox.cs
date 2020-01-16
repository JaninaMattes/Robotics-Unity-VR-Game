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


    // Start is called before the first frame update
    void Start()
    {

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
            }
            else
            {
                SoundEffekt.Play();
                Instantiate(Funken, funkenLocation.transform.position, funkenLocation.transform.rotation);
            }
        }
    }


}
