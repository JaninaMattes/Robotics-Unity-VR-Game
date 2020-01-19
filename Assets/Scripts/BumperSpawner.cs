using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class BumperSpawner : MonoBehaviour
{
    [Header("Circular Bumper Settings")]
    // All gameobjects that can be used
    public GameObject circularBumper;
    // All settings for Bumper
    public int sceneIndex;
    public GameObject cameraRigTransform;
    public float distance = 0.2f;
    public float xRotation;
    public float yRotation;
    public float xRotationT;
    public float yRotationT;
    // store new GameObject instance
    private GameObject activeBumper;
    private Scene activeScene;
    private Vector3 playerLocation;
    private Transform targetPosition;

    // Called once per frame
    void Update()
    {
        activeScene = SceneManager.GetActiveScene();
        sceneIndex = activeScene.buildIndex;
        // Retrieve Player Headset position
        // playerLocation = OVRManager.tracker.GetPose().position;
        //cameraRigTransform = GameObject.FindGameObjectWithTag("MainCamera");
        playerLocation = cameraRigTransform.transform.position; //returns found VRSimulatorCameraRig if it is found
        // Check Scene index returns an integer value
        if (activeScene.buildIndex != 0)
        {
            // Activate Controller in Minigames
            ActivateCollider(true);
        }
        else
        {
            // Deactivate Controller in Workshop
            ActivateCollider(false);
        }
    }

    /// <summary>
    /// On Collision Enter a new bumper object will be 
    /// instantiated and attached to the player.
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("# Collission detected");
        Vector3 contact = col.contacts[0].point;
        // Check the scene index
        if (activeScene.buildIndex > 0 && activeScene.buildIndex < 3)
        {
            if (activeBumper == null)
            {
                targetPosition = cameraRigTransform.transform;
                activeBumper = Instantiate(circularBumper, new Vector3(contact.x, contact.y, contact.z), Quaternion.identity);
                activeBumper.transform.Rotate(xRotation, yRotation, 0);
                Vector3 lookPos = new Vector3(activeBumper.transform.position.x,
                                        targetPosition.position.y,
                                        activeBumper.transform.position.z);
                activeBumper.transform.LookAt(lookPos);
                // Check if the position of the player and bumper are approximately equal.
                if (Vector3.Distance(activeBumper.transform.position, targetPosition.position) < 0.001f)
                {
                    // Swap the position
                    targetPosition.position *= -1.0f;
                }
                activeBumper.transform.position = Vector3.MoveTowards(activeBumper.transform.position, targetPosition.position, distance);
                xRotationT = activeBumper.transform.rotation.x;
                yRotationT = activeBumper.transform.rotation.y;
            }
        }
    }

    /// <summary>
    /// OnCollisionExit is calledf when collision is ended
    /// </summary>
    /// <param name="collisionInfo"></param>
    void OnCollisionExit(Collision collisionInfo)
    {
        if (activeBumper != null)
        {
            Destroy(activeBumper);
        }
    }

    /// <summary>
    /// Activate the Collider of the associated Controller.
    /// </summary>
    /// <param name="enable"></param>
    void ActivateCollider(bool enable)
    {
        // Set all Colliders inactive on the gameobjects
        GetComponent<BoxCollider>().enabled = enable;
    }

}