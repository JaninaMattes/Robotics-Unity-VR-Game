using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class IconMovement : VRTK_TransformFollow
{
    float TimeCounter = 0;
    public float waveheight = 0.1f;
    public float speed = 1;
    Vector3 tempPosition;
    Vector3 startPosition;
    Transform cameraRig;

    private void Start()
    {
        startPosition= transform.position;
        Invoke("FindCameraRig", 0.05f);

    }

    void FindCameraRig()
    {
        cameraRig = VRTK_DeviceFinder.HeadsetTransform();
        gameObjectToFollow = cameraRig.gameObject;
    }

    // Update is called once per frame
    new void Update()
    {
        //Debug.Log("Pos: " + cameraRig.transform.position + " Rot: " + cameraRig.transform.rotation);
        TimeCounter += Time.deltaTime * speed;

        tempPosition.y = startPosition.y + Mathf.Sin(TimeCounter)*waveheight;
        tempPosition.z = startPosition.z;
        tempPosition.x = startPosition.x;

        transform.position = tempPosition;
    }

    protected override void SetRotationOnGameObject(Quaternion newRotation)
    {
        Vector3 eulerRotation = newRotation.eulerAngles;
        eulerRotation = new Vector3(0, eulerRotation.y, 0);
        newRotation = Quaternion.Euler(eulerRotation);
         
        transformToChange.rotation = newRotation;
    }
}
