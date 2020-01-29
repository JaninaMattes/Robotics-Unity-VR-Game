using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class IconControllerHighlighter : VRTK_ControllerHighlighter
{
    GameObject controllerRight;
    GameObject controllerLeft;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("FindControllerRight", 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindControllerRight()
    {
        controllerRight = VRTK_DeviceFinder.GetControllerRightHand();
        controllerLeft = VRTK_DeviceFinder.GetControllerLeftHand();
        controllerAlias = controllerRight;
    }
}
