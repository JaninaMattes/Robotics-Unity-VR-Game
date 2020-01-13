using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

[ExecuteInEditMode]
public class ToggleLevel : MonoBehaviour
{
    [Header("Snapdrop Zone Prefab")]
    public VRTK_SnapDropZone snapZone;
    [Tooltip("Level Index and Information")]
    public int WorkshopLevelIndex;
    public int LevelIndex;
    public string sceneLoadingName;
    public string sceneLoadingNameWorkshop;

    protected bool isSnapped = false;
    protected ManageScenes sceneManagement;
    protected GameObject stagingObjects;
   
    public void OnEnable()
    {
        snapZone.ObjectSnappedToDropZone += ObjectSnappedToDropZone;
        snapZone.ObjectUnsnappedFromDropZone += ObjectUnsnappedFromDropZone;
    }

    public void OnDisable()
    {
        snapZone.ObjectSnappedToDropZone -= ObjectSnappedToDropZone;
        snapZone.ObjectUnsnappedFromDropZone -= ObjectUnsnappedFromDropZone;
    }

    public void Start()
    {
        stagingObjects = GameObject.FindGameObjectWithTag("stage");
    }

    public void Update()
    {
    }

    // Eventhandler
    protected virtual void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        Debug.Log("Object snapped to DropZone");
        sceneManagement.levelInd = LevelIndex;
        sceneManagement.sceneLoadingName = sceneLoadingName;
        sceneManagement.loadLevel = true;
    }

    protected virtual void ObjectUnsnappedFromDropZone(object sender, SnapDropZoneEventArgs e)
    {
        Debug.Log("Object unsnapped from DropZone");
        sceneManagement.levelInd = WorkshopLevelIndex;
        sceneManagement.sceneLoadingName = sceneLoadingNameWorkshop;
        sceneManagement.loadLevel = true;
    }
}