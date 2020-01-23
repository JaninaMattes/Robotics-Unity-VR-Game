using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class ToggleLevel : MonoBehaviour
{  

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

    // Singleton to controll all data used by various classes 
    protected Game_Manager controller = Game_Manager.Instance;

    void Awake()
    {
        foreach (GameObject objectToKeep in objectsToKeep)
        {
            DontDestroyOnLoad(objectToKeep);
        }

        SetRendererList(this.controller);
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
        SetRendererList(this.controller);

        if (CheckForCurrentSnappedObject(this.snapZone))
        {
            DisableRenderer(GetCurrentSnappedObject(this.snapZone));
            UnFadeHeadset(this.fadeOutDuration);
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
        FadeHeadset(this.fadeColor, this.fadeDuration);    
    }

    protected virtual void ObjectExitedSnapDropZone(object sender, SnapDropZoneEventArgs e)
    {
        this.objectExitedSnapDropZone = true;
        if(GetActiveSceneBuildIndex() == this.LevelIndex)
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
        if(snapDropZone.GetCurrentSnappedObject() != null)
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
        if((GetActiveSceneBuildIndex() == this.LevelIndex) && (objectExitedDropZone))
        {
            SceneManager.LoadScene(workShopIndex);
        }
        else if((GetActiveSceneBuildIndex() == this.WorkshopLevelIndex) && (objectExitedDropZone == false))
        {
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            return;
        }
    }

    private void EnableRenderer(GameObject snappedObject)
    {
        if(snappedObject != null)
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

}