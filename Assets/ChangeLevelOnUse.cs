using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;


public class ChangeLevelOnUse : MonoBehaviour
{
    [Header("Level Index")]
    // Select the correct level index for a scene
    public int WorkshopLevelIndex = 0;
    public int DestinationLevelIndex = 1;
    [Header("VR Headset To Trigger Change")]
    public VRTK_InteractableObject headset;
    private float timeElapsed;

    [Header("Do not Destroy On Load")]
    public List<GameObject> objectsToKeep = new List<GameObject>();

    [Header("Snapdrop Zone Prefab Munition")]
    public VRTK_SnapDropZone snapZoneMunition;
    protected IEnumerator asyncLoadCoroutine;

    [Header("Start Position OnLevelLoaded")]
    public GameObject cameraRig;
    public Vector3 startPosition;
    private GameObject lookAt;

    [Header("Keep Objects OnLevelLoaded")]
    GameObject rightControllerBasePointer;
    GameObject leftControllerBasePointer;
    GameObject OvrAvatarSDKManager;


    // Singleton to controll all data used by various classes 
    protected Game_Manager controller = Game_Manager.Instance;

    ManageScenes sceneManagement = new ManageScenes();

    void Awake()
    {
        foreach (GameObject objectToKeep in objectsToKeep)
        {
            DontDestroyOnLoad(objectToKeep);
        }
        Invoke("GetExtraOculusObjects", 0.5f);
    }

    public void GetExtraOculusObjects()
    {
        rightControllerBasePointer = GameObject.Find("[VRTK][AUTOGEN][RigthController][BasePointerRenderer_Origin_Smoothed]");
        leftControllerBasePointer = GameObject.Find("[VRTK][AUTOGEN][LeftController][BasePointerRenderer_Origin_Smoothed]");
        OvrAvatarSDKManager = GameObject.Find("OvrAvatarSDKManager");
        rightControllerBasePointer.tag = "Player";
        leftControllerBasePointer.tag = "Player";
        OvrAvatarSDKManager.tag = "Player";
        DontDestroyOnLoad(rightControllerBasePointer);
        DontDestroyOnLoad(leftControllerBasePointer);
        DontDestroyOnLoad(OvrAvatarSDKManager);
    }

    public void OnEnable()
    {
        headset = (headset == null ? GetComponent<VRTK_InteractableObject>() : headset);

        if (headset != null)
        {
            headset.InteractableObjectGrabbed += InteractableObjectGrabbed;
            headset.InteractableObjectUngrabbed += InteractableObjectUngrabbed;
            this.snapZoneMunition.ObjectSnappedToDropZone += ObjectSnappedToDropZone;
            this.snapZoneMunition.ObjectExitedSnapDropZone += ObjectExitedSnapDropZone;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    public void OnDisable()
    {
        if (headset != null)
        {
            headset.InteractableObjectGrabbed -= InteractableObjectGrabbed;
            headset.InteractableObjectUngrabbed -= InteractableObjectUngrabbed;
            this.snapZoneMunition.ObjectSnappedToDropZone -= ObjectSnappedToDropZone;
            this.snapZoneMunition.ObjectExitedSnapDropZone -= ObjectExitedSnapDropZone;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void Start()
    {
        headset = this.GetComponent<VRTK_InteractableObject>();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetPlayerPosition(scene.buildIndex);
        // Tags need to be:
        // "SonarSensor_1" "SonarSensor_2" 
        // "LidarSensor" "RadarSensor" "CameraSensor"
        //if (scene.buildIndex != 0 && scene.buildIndex != 1)
        //{
            SetRendererList(this.controller);
            controller.FindProbes();
            CheckSnapUpdateMaterial();
            ExchangeFloorTag();
        //}
    }

    protected virtual void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        if (this.CheckForCurrentSnappedObject(snapZoneMunition))
        {
            controller.SetSnappedPatrone(snapZoneMunition.GetCurrentSnappedObject().tag);
        }
        else
        {
            controller.SetSnappedPatrone("default");
        }
        
    }

    protected virtual void ObjectExitedSnapDropZone(object sender, SnapDropZoneEventArgs e)
    {

    }

    private bool CheckForCurrentSnappedObject(VRTK_SnapDropZone snapDropZone)
    {
        if (snapDropZone.GetCurrentSnappedObject() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int GetActiveSceneBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    private void LoadLevel(int levelIndex, int workShopIndex)
    {
        if (GetActiveSceneBuildIndex() == this.DestinationLevelIndex)
        {
            // Allow async loading of the scene on background thread
            LoadTheSceneAsync(workShopIndex);
        }
        else if (GetActiveSceneBuildIndex() == this.WorkshopLevelIndex)
        {
            LoadTheSceneAsync(levelIndex);
        }
        else
        {
            return;
        }
    }

    private void SetRendererList(Game_Manager controller)
    {
        controller.GetMeshRenderer();
        controller.SetMaterials(controller.GetRenderer());
    }

    private void CheckSnapUpdateMaterial()
    {
        // Fetch light and set it
        controller.SetLight(SceneManager.GetActiveScene().buildIndex);
        string patrone = controller.GetSnappedPatrone();
        bool lightOn = false;

        if (patrone == "CameraSensor")
        {
            lightOn = true;
        }

        if (CheckForCurrentSnappedObject(this.snapZoneMunition))
        {
            Debug.Log("## Update Material" + CheckForCurrentSnappedObject(this.snapZoneMunition));
            controller.ToggleLight(SceneManager.GetActiveScene().buildIndex, lightOn);
            controller.UpdateMaterial(patrone);
        }
        else
        {
            Debug.Log("# Default Update Material" + CheckForCurrentSnappedObject(this.snapZoneMunition));
            controller.UpdateMaterial("default");
            controller.ToggleLight(SceneManager.GetActiveScene().buildIndex, lightOn);
        }
    }

    public void LoadTheSceneAsync(int workShopIndex)
    {
        asyncLoadCoroutine = LoadSceneAsync(workShopIndex);
        StartCoroutine(asyncLoadCoroutine);
    }

    private void ExchangeFloorTag()
    {
        Renderer[] _rend = controller.GetRenderer();
        for (int i = 0; i < _rend.Length; i++)
        {
            if (_rend[i].tag == "Floor")
            {
                _rend[i].tag = "IncludeTeleport";
            }
        }
        controller.SetRenderer(_rend);
    }

    private IEnumerator LoadSceneAsync(int workShopIndex)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(workShopIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void SetPlayerPosition(int buildIndex)
    {
        switch (buildIndex)
        {
            case 0:
                lookAt = GameObject.FindGameObjectWithTag("Robo");
                cameraRig.transform.position = new Vector3(startPosition.x, cameraRig.transform.position.y, startPosition.z);
                cameraRig.transform.eulerAngles = new Vector3(cameraRig.transform.rotation.x, -(lookAt.transform.eulerAngles.y), cameraRig.transform.rotation.z);
                break;

            case 1:
                lookAt = GameObject.FindGameObjectWithTag("SelectLevel2");
                cameraRig.transform.position = new Vector3(startPosition.x, cameraRig.transform.position.y, startPosition.z);
                cameraRig.transform.eulerAngles = new Vector3(cameraRig.transform.rotation.x, -(lookAt.transform.eulerAngles.y), cameraRig.transform.rotation.z);
                break;

            case 2:
                lookAt = GameObject.FindGameObjectWithTag("Pilz");
                cameraRig.transform.position = new Vector3(startPosition.x, cameraRig.transform.position.y, startPosition.z);
                cameraRig.transform.eulerAngles = new Vector3(cameraRig.transform.rotation.x, -(lookAt.transform.eulerAngles.y), cameraRig.transform.rotation.z);
                break;

            case 3:
                // Kitchen with checklist
                lookAt = GameObject.FindGameObjectWithTag("CheckListCanvas");
                cameraRig.transform.position = new Vector3(startPosition.x, cameraRig.transform.position.y, startPosition.z);
                cameraRig.transform.eulerAngles = new Vector3(cameraRig.transform.rotation.x, -(lookAt.transform.eulerAngles.y), cameraRig.transform.rotation.z);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Grabbing object to change Scene Level.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        LoadLevel(DestinationLevelIndex, WorkshopLevelIndex);
    }

    protected virtual void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
       
    }
}
