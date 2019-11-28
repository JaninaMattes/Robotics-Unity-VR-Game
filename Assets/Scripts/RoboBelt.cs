using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RoboBelt : MonoBehaviour
{
    // Properties
    public GameObject spawnObject;

    private void OnTriggerStay(Collider col)
    {
        VRTK_InteractGrab grabbable = 
            (col.gameObject.GetComponent<VRTK_InteractGrab>() ? 
            col.gameObject.GetComponent<VRTK_InteractGrab>() : 
            col.gameObject.GetComponentInParent<VRTK_InteractGrab>());

        if (CanGrab(grabbable))
        {
            Debug.Log($"Spwaned the object {grabbable}");
            GameObject spawned = Instantiate(spawnObject);
            grabbable.GetComponent<VRTK_InteractTouch>().ForceTouch(spawnObject);
            grabbable.AttemptGrab();
        }
    }

    private bool CanGrab(VRTK_InteractGrab grab)
    {
        return true; 
            //(grab && grab.GetGrabbedObject() == null && 
            // grabpressed = when grab button is pressed to grab object
            // TODO: Nachlesen in VRTK Dokumentation
            // grab.gameObject.GetComponent<VRTK_ControllerEvents>().grabPressed);
    }
}
