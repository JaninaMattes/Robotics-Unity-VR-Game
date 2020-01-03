using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class BumperSpawner : MonoBehaviour
{
    // All gameobjects that can be used
    public GameObject circularBumper;
    // All settings for Bumper
    public float distance;
    public int sceneIndex;
    
    // store new GameObject instance
    private GameObject activeBumper;
    private Scene activeScene;
    private Vector3 playerLocation;
    private GameObject cameraRigTransform;

    private void Awake()
    {
        // Retrieve player headset 
        cameraRigTransform = GameObject.FindGameObjectWithTag("MainCamera");
    }
    
    // Called once per frame
    void Update()
    {
        activeScene = SceneManager.GetActiveScene();
        sceneIndex = activeScene.buildIndex;
        // Retrieve Player Headset position
        // playerLocation = OVRManager.tracker.GetPose().position;        
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
        Debug.Log("Collission detected");
        Vector3 contact = col.contacts[0].point;
        // Check the scene index
        if (activeScene.buildIndex != 0)
        {
            if (activeBumper == null)
            {
                activeBumper = Instantiate(circularBumper, new Vector3(contact.x, cameraRigTransform.transform.position.y - 0.7f, contact.z) +
                               cameraRigTransform.transform.forward * distance, Quaternion.identity);
                activeBumper.transform.LookAt(activeBumper.transform.position + cameraRigTransform.transform.rotation * Vector3.left);
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