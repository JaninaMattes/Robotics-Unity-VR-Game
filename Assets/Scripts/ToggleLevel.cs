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
    public GameObject lidarGrid;

    protected bool isSnapped = false;

     void Awake()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(lidarGrid);
    }

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
}