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
    private GameObject[] headsetsInScene;

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

    // Singleton to controll all data used by various classes 
    protected Game_Manager controller = Game_Manager.Instance;

    void Awake()
    {
        foreach (GameObject objectToKeep in objectsToKeep)
        {
            DontDestroyOnLoad(objectToKeep);
        }
    }

    void Update()
    {
        CheckHeadsetsInScene();
    }

    public void OnEnable()
    {
        this.snapZone.ObjectSnappedToDropZone += ObjectSnappedToDropZone;
        this.snapZone.ObjectExitedSnapDropZone += ObjectExitedSnapDropZone;
        this.headSet.InteractableObjectTouched += InteractableObjectTouched;
        this.headSet.InteractableObjectUntouched += InteractableObjectUntouched;
        this.fadeHeadset.HeadsetFadeComplete += OnHeadsetFadeComplete;
        this.fadeHeadset.HeadsetUnfadeComplete += OnHeadsetUnfadeComplete;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        this.snapZone.ObjectSnappedToDropZone -= ObjectSnappedToDropZone;
        this.snapZone.ObjectExitedSnapDropZone -= ObjectExitedSnapDropZone;
        this.headSet.InteractableObjectTouched -= InteractableObjectTouched;
        this.headSet.InteractableObjectUntouched -= InteractableObjectUntouched;
        this.fadeHeadset.HeadsetFadeComplete -= OnHeadsetFadeComplete;
        this.fadeHeadset.HeadsetUnfadeComplete -= OnHeadsetUnfadeComplete;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetPlayerPosition();
        
        if (CheckForCurrentSnappedObject(this.snapZone))
        {
            DisableRenderer(GetCurrentSnappedObject(this.snapZone));
            UnFadeHeadset(this.fadeOutDuration);
        }
        // Tags need to be:
        // "SonarSensor_1" "SonarSensor_2" 
        // "LidarSensor" "RadarSensor" "CameraSensor"
        if (scene.buildIndex != 0 && scene.buildIndex != 1)
        {
            SetRendererList(this.controller);
            CheckSnapUpdateMaterial();
        }
    }

    protected virtual void OnHeadsetFadeComplete(object sender, HeadsetFadeEventArgs a)
    {
        LoadLevel(this.LevelIndex, this.WorkshopLevelIndex, this.objectExitedSnapDropZone);
    }

    protected virtual void OnHeadsetUnfadeComplete(object sender, HeadsetFadeEventArgs a)
    {
        EnableCollider(GetCurrentSnappedObject(this.snapZone));
    }

    protected virtual void InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {
        EnableRenderer(GetCurrentSnappedObject(this.snapZone));
    }

    protected virtual void InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
    {
        DisableRenderer(GetCurrentSnappedObject(this.snapZone));
    }

    protected virtual void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        this.objectExitedSnapDropZone = false;
        DisableCollider(GetCurrentSnappedObject(this.snapZone));
        DisableRenderer(GetCurrentSnappedObject(this.snapZone));
        FadeHeadset(this.fadeColor, this.fadeDuration);
    }

    protected virtual void ObjectExitedSnapDropZone(object sender, SnapDropZoneEventArgs e)
    {
        this.objectExitedSnapDropZone = true;
        if (GetActiveSceneBuildIndex() == this.LevelIndex)
        {
            FadeHeadset(this.fadeColor, this.fadeDuration);
        }
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
        if ((GetActiveSceneBuildIndex() == this.LevelIndex) && (objectExitedDropZone))
        {
            Debug.Log("0.0 LOAD LEVEL ################");
            // Allow async loading of the scene on background thread
            LoadTheSceneAsync(workShopIndex);
            //SceneManager.LoadScene (workShopIndex);
        }
        else if ((GetActiveSceneBuildIndex() == this.WorkshopLevelIndex) && (objectExitedDropZone == false))
        {
            Debug.Log("0.1 LOAD LEVEL ################");
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

    private void CheckHeadsetsInScene()
    {
        this.headsetsInScene = GameObject.FindGameObjectsWithTag("Headset");

        if (this.headsetsInScene.Length > 1)
        {
            Destroy(this.headsetsInScene[1]);
        }
    }

    private void SetRendererList(Game_Manager controller)
    {
        controller.GetMeshRenderer();
        controller.SetMaterials(controller.GetRenderer());
    }

    private void CheckSnapUpdateMaterial()
    {
        if (CheckForCurrentSnappedObject(this.snapZonePatrone))
        {
            controller.UpdateMaterial(GetCurrentSnappedObject(this.snapZonePatrone).tag);
        }
        else
        {
            controller.UpdateMaterial("default");
        }
    }

    public void LoadTheSceneAsync(int workShopIndex)
    {
        Debug.Log(" 1 LOAD LEVEL ################");
        asyncLoadCoroutine = LoadSceneAsync(workShopIndex);
        StartCoroutine(asyncLoadCoroutine);
    }

    private IEnumerator LoadSceneAsync(int workShopIndex)
    {
        Debug.Log($"2 Level Index{workShopIndex}");
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

    private void SetPlayerPosition()
    {
        lookAt = GameObject.FindGameObjectWithTag("CheckListCanvas");
        cameraRig.transform.position = new Vector3(startPosition.x, cameraRig.transform.position.y, startPosition.z);
        cameraRig.transform.eulerAngles = new Vector3(cameraRig.transform.rotation.x, -(lookAt.transform.eulerAngles.y), cameraRig.transform.rotation.z);

    }

}