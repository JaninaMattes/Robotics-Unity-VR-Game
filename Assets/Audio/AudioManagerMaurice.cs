using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;


//Has to be placed on Player or any child object of the player
//vrtkobjects each (if sound is needed) need an audiosource with an audioclip 
//object with audiosource needs to be tagged 

public class AudioManagerMaurice : MonoBehaviour
{
    private VRTK_InteractableObject[] interactableObjects;

    public void OnEnable()
    {
        GetObjects();
        SetInteractionEventListener();
    }

    public void OnDisable()
    {
        DeactivateInteractionEventListener();
    }

    private void SetInteractionEventListener()
    {
        foreach (VRTK_InteractableObject interactableObject in interactableObjects)
        {
            if (interactableObject != null)
            {
                interactableObject.InteractableObjectTouched += InteractableObjectTouched;
                interactableObject.InteractableObjectUntouched += InteractableObjectUntouched;
                interactableObject.InteractableObjectGrabbed += InteractableObjectGrabbed;
                interactableObject.InteractableObjectUngrabbed += InteractableObjectUnGrabbed;
                interactableObject.InteractableObjectUsed += InteractableObjectUsed;
                interactableObject.InteractableObjectUnused += InteractableObjectUnUsed;
                interactableObject.InteractableObjectEnteredSnapDropZone += InteractableObjectEnteredSnapDropZone;
                interactableObject.InteractableObjectExitedSnapDropZone += InteractableObjectExitedSnapDropZone;
                interactableObject.InteractableObjectSnappedToDropZone += InteractableObjectSnappedToDropZone;
                interactableObject.InteractableObjectUnsnappedFromDropZone += InteractableObjectUnsnappedFromDropZone;
            }
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void DeactivateInteractionEventListener()
    {
        foreach (VRTK_InteractableObject interactableObject in interactableObjects)
        {
            if (interactableObject != null)
            {
                interactableObject.InteractableObjectTouched -= InteractableObjectTouched;
                interactableObject.InteractableObjectUntouched -= InteractableObjectUntouched;
                interactableObject.InteractableObjectGrabbed -= InteractableObjectGrabbed;
                interactableObject.InteractableObjectUngrabbed -= InteractableObjectUnGrabbed;
                interactableObject.InteractableObjectUsed -= InteractableObjectUsed;
                interactableObject.InteractableObjectUnused -= InteractableObjectUnUsed;
                interactableObject.InteractableObjectEnteredSnapDropZone -= InteractableObjectEnteredSnapDropZone;
                interactableObject.InteractableObjectExitedSnapDropZone -= InteractableObjectExitedSnapDropZone;
                interactableObject.InteractableObjectSnappedToDropZone -= InteractableObjectSnappedToDropZone;
                interactableObject.InteractableObjectUnsnappedFromDropZone -= InteractableObjectUnsnappedFromDropZone;
            }
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Awake()
    {

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    protected virtual void InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {

        switch (InteractableObjectTag(sender))
        {
            //Example: VRTK object has audiosource with an audioclip and the gameobject is tagged "fire". the sound of the object will play every time it gets (re) touched.
            case "Fire":
                //Debug.Log("Touched");
                GetAudioSource(sender).Play();
                break;
            case "Tag2":
                GetAudioSource(e).Play();
                break;
            case "Tag3":
                GetAudioSource(e).Play();
                break;
            case "Tag4":
                GetAudioSource(e).Play();
                break;
            case "Tag5":
                GetAudioSource(e).Play();
                break;
            default:

                break;
        }
    }

    protected virtual void InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
    {

    }

    protected virtual void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {

    }

    protected virtual void InteractableObjectUnGrabbed(object sender, InteractableObjectEventArgs e)
    {

    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {

    }

    protected virtual void InteractableObjectUnUsed(object sender, InteractableObjectEventArgs e)
    {

    }

    protected virtual void InteractableObjectEnteredSnapDropZone(object sender, InteractableObjectEventArgs e)
    {

    }

    protected virtual void InteractableObjectExitedSnapDropZone(object sender, InteractableObjectEventArgs e)
    {

    }

    protected virtual void InteractableObjectSnappedToDropZone(object sender, InteractableObjectEventArgs e)
    {

    }

    protected virtual void InteractableObjectUnsnappedFromDropZone(object sender, InteractableObjectEventArgs e)
    {

    }

    private void GetObjects()
    {
        interactableObjects = GameObject.FindObjectsOfType<VRTK_InteractableObject>();
    }

    private AudioSource GetAudioSource(object sender)
    {
        VRTK_InteractableObject interactableObject = sender as VRTK_InteractableObject;
        if (interactableObject.GetComponent<AudioSource>() != null)
        {
            return interactableObject.GetComponent<AudioSource>();
        }
        else return null;
    }

    private string InteractableObjectTag(object sender)
    {
        VRTK_InteractableObject interactableObject = sender as VRTK_InteractableObject;
        return interactableObject.tag;
    }

}