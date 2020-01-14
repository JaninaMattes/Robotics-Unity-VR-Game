using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class ToggleLevel : MonoBehaviour
{
    [Header("Snapdrop Zone Prefab")]
    public VRTK_SnapDropZone snapZone;
    [Tooltip("Level Index and Information")]
    public int WorkshopLevelIndex;
    public int LevelIndex;
    public GameObject lidarGrid;
    public GameObject player;

    protected bool isSnapped = false;

     void Awake()
    {
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(lidarGrid);
    }

    public void OnEnable()
    {
        Debug.Log("### START ####");
        snapZone.ObjectSnappedToDropZone += ObjectSnappedToDropZone;
        snapZone.ObjectUnsnappedFromDropZone += ObjectUnsnappedFromDropZone;
        snapZone.ObjectExitedSnapDropZone += ObjectExitedSnapDropZone;
        snapZone.ObjectEnteredSnapDropZone += OnObjectEnteredSnapDropZone;
    }

    public void OnDisable()
    {
        snapZone.ObjectSnappedToDropZone -= ObjectSnappedToDropZone;
        snapZone.ObjectUnsnappedFromDropZone -= ObjectUnsnappedFromDropZone;
        snapZone.ObjectExitedSnapDropZone -= ObjectExitedSnapDropZone;
        snapZone.ObjectEnteredSnapDropZone -= OnObjectEnteredSnapDropZone;
    }

    // Eventhandler
    protected virtual void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        Debug.Log("Object snapped to DropZone");
        SceneManager.LoadScene(LevelIndex);
    }

    protected virtual void ObjectUnsnappedFromDropZone(object sender, SnapDropZoneEventArgs e)
    {
        Debug.Log("Object unsnapped from DropZone");
        SceneManager.LoadScene(WorkshopLevelIndex);
    }

    protected virtual void ObjectExitedSnapDropZone(object sender, SnapDropZoneEventArgs e)
    {
        Debug.Log("Object exited DropZone");
        SceneManager.LoadScene(WorkshopLevelIndex);
    }

    protected virtual void OnObjectEnteredSnapDropZone(object sender, SnapDropZoneEventArgs e)
    {
        Debug.Log("Object entered SnapDropZone");
        SceneManager.LoadScene(LevelIndex);
    }

}