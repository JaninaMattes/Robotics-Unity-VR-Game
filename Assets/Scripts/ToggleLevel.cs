using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class ToggleLevel : MonoBehaviour
{

    public int DebuggingLevel;

    [Header("Level Index")]
    public int WorkshopLevelIndex;
    public int LevelIndex;

    [Header("Do not Destroy On Load")]
    public List<GameObject> objectsToKeep = new List<GameObject>();

    [Header("Snapdrop Zone Prefab")]
    public VRTK_SnapDropZone snapZone;

    [Header("VR Headset")]
    public VRTK_InteractableObject headSet;
    public GameObject headsetObj;
    private GameObject[] gunsInScene;

    [Header("Level Fade Transition")]
    public VRTK_HeadsetFade fadeHeadset;
    public Color fadeColor;
    [Range(0.0f, 10.0f)]
    public float fadeDuration = 0;
    [Range(0.0f, 10.0f)]
    public float fadeOutDuration = 0;
    private bool objectExitedSnapDropZone = false;
    [Header("Snapdrop Zone Prefab Patrone")]
    public VRTK_SnapDropZone snapZonePatrone;
    protected IEnumerator asyncLoadCoroutine;

    [Header("Start Position OnLevelLoaded")]
    public GameObject cameraRig;

    public Vector3 startPosition;
    private GameObject lookAt;

    GameObject rightControllerBasePointer;
    GameObject leftControllerBasePointer;
    GameObject OvrAvatarSDKManager;


    // Singleton to controll all data used by various classes 
    protected Game_Manager controller = Game_Manager.Instance;

    void Awake()
    {      
        //CheckSnapUpdateMaterial();
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

    void Update()
    {
        CheckGunsInScene();
    }

    public void OnEnable()
    {
        this.snapZone.ObjectSnappedToDropZone += ObjectSnappedToDropZone;
        this.snapZone.ObjectExitedSnapDropZone += ObjectExitedSnapDropZone;
        this.headSet.InteractableObjectUsed += InteractableObjectUsed;
        this.headSet.InteractableObjectUntouched += InteractableObjectUntouched;
        this.fadeHeadset.HeadsetFadeComplete += OnHeadsetFadeComplete;
        this.fadeHeadset.HeadsetUnfadeComplete += OnHeadsetUnfadeComplete;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        this.snapZone.ObjectSnappedToDropZone -= ObjectSnappedToDropZone;
        this.snapZone.ObjectExitedSnapDropZone -= ObjectExitedSnapDropZone;
        this.headSet.InteractableObjectUsed -= InteractableObjectUsed;
        this.headSet.InteractableObjectUntouched -= InteractableObjectUntouched;
        this.fadeHeadset.HeadsetFadeComplete -= OnHeadsetFadeComplete;
        this.fadeHeadset.HeadsetUnfadeComplete -= OnHeadsetUnfadeComplete;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetPlayerPosition(scene.buildIndex);
        if (scene.buildIndex != 0 && scene.buildIndex != 1)
        {
            Destroy(this.headsetObj);
            SetRendererList(this.controller);
            controller.FindProbes();
            CheckSnapUpdateMaterial();
            ExchangeFloorTag();
        }     
    }

    protected virtual void OnHeadsetFadeComplete(object sender, HeadsetFadeEventArgs a)
    {
        
    }

    protected virtual void OnHeadsetUnfadeComplete(object sender, HeadsetFadeEventArgs a)
    {
        
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        if (this.headSet.IsGrabbed())
        {
            LoadLevel(this.LevelIndex, this.WorkshopLevelIndex, this.objectExitedSnapDropZone);
        }
    }

    protected virtual void InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
    {
        
    }

    protected virtual void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        
    }

    protected virtual void ObjectExitedSnapDropZone(object sender, SnapDropZoneEventArgs e)
    {
        
    }

    private GameObject GetCurrentSnappedObject(VRTK_SnapDropZone snapDropZone)
    {
        if (CheckForCurrentSnappedObject(this.snapZone))
        {
            return snapDropZone.GetCurrentSnappedObject();
        }
        else
        {
            return null;
        }
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

    private void LoadLevel(int levelIndex, int workShopIndex, bool objectExitedDropZone)
    {
        if ((GetActiveSceneBuildIndex() == this.LevelIndex) )
        {
            // Allow async loading of the scene on background thread
            LoadTheSceneAsync(workShopIndex);
            //SceneManager.LoadScene (workShopIndex);
        }
        else if ((GetActiveSceneBuildIndex() == this.WorkshopLevelIndex) )
        {
            LoadTheSceneAsync(levelIndex);
            // SceneManager.LoadScene (levelIndex);
        }
        else
        {
            return;
        }
    }

    private void EnableRenderer(GameObject snappedObject)
    {
        if (snappedObject != null)
        {
            snappedObject.GetComponent<Renderer>().enabled = true;
        }
    }

    private void DisableRenderer(GameObject snappedObject)
    {
        if (snappedObject != null)
        {
            snappedObject.GetComponent<Renderer>().enabled = false;
        }
    }

    private void EnableCollider(GameObject snappedObject)
    {
        if (snappedObject != null)
        {
            snappedObject.GetComponent<Collider>().enabled = true;
        }
    }

    private void DisableCollider(GameObject snappedObject)
    {
        if (snappedObject != null)
        {
            snappedObject.GetComponent<Collider>().enabled = false;
        }
    }

    private void FadeHeadset(Color color, float fadeDuration)
    {
       this.fadeHeadset.Fade(color, fadeDuration);
    }

    private void UnFadeHeadset(float fadeOutDuration)
    {
       this.fadeHeadset.Unfade(fadeOutDuration);
    }

    private void CheckGunsInScene()
    {
        this.gunsInScene = GameObject.FindGameObjectsWithTag("Scanner");

        if (this.gunsInScene.Length > 1)
        {
            Destroy(this.gunsInScene[1]);
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
        //TODO: controller.SetLight(SceneManager.GetActiveScene().buildIndex);
        string patrone = controller.GetSnappedPatrone();
        bool lightOn = false;

        if (patrone == "CameraSensor")
        {
            lightOn = true;
        }

        if (CheckForCurrentSnappedObject(this.snapZonePatrone))
        {
            Debug.Log("## Update Material" + CheckForCurrentSnappedObject(this.snapZonePatrone));
            //controller.ToggleLight(SceneManager.GetActiveScene().buildIndex, lightOn);
            controller.UpdateMaterial(patrone);
        }
        else
        {
            Debug.Log("# Default Update Material" + CheckForCurrentSnappedObject(this.snapZonePatrone));
            controller.UpdateMaterial("default");
            //controller.ToggleLight(SceneManager.GetActiveScene().buildIndex, lightOn);
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

        if (asyncLoad.isDone)
        {
            DebuggingLevel = SceneManager.GetActiveScene().buildIndex;
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
}