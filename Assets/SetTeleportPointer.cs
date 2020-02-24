using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SetTeleportPointer : MonoBehaviour
{
    public Transform pointerOrigin;
    public VRTK_InteractGrab rightController;
    public VRTK_InteractGrab leftController;

    public void SetPointerOrigin()
    {
        if(this.rightController.GetGrabbedObject() == this.gameObject)
        {
            this.rightController.GetComponent<VRTK_Pointer>().customOrigin = this.pointerOrigin;
            UpdatePointer(this.rightController.GetComponent<VRTK_Pointer>(), this.rightController.GetComponent<VRTK_StraightPointerRenderer>());
        }
        else if (this.leftController.GetGrabbedObject() == this.gameObject)
        {
            this.leftController.GetComponent<VRTK_Pointer>().customOrigin = this.pointerOrigin;
            UpdatePointer(this.leftController.GetComponent<VRTK_Pointer>(), this.leftController.GetComponent<VRTK_StraightPointerRenderer>());
        }
    }

    public void SetBackPointerOrigin()
    {
        this.rightController.GetComponent<VRTK_Pointer>().customOrigin = this.rightController.transform;
        UpdatePointer(this.rightController.GetComponent<VRTK_Pointer>(), this.rightController.GetComponent<VRTK_StraightPointerRenderer>());
        this.leftController.GetComponent<VRTK_Pointer>().customOrigin = this.leftController.transform;
        UpdatePointer(this.leftController.GetComponent<VRTK_Pointer>(), this.leftController.GetComponent<VRTK_StraightPointerRenderer>());
    }

    public void UpdatePointer(VRTK_Pointer pointer, VRTK_StraightPointerRenderer pointerRenderer)
    {
        pointerRenderer.enabled = false;
        pointerRenderer.enabled = true;
        pointer.enabled = false;
        pointer.enabled = true;
    }

}
