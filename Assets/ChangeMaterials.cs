using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

[ExecuteInEditMode]
public class ChangeMaterials : MonoBehaviour
{
    [Header("Snapdrop Zone Prefab")]
    public VRTK_SnapDropZone snapZone;
    [Header("Sensor Material")]
    [Tooltip("Sonar Materials")]
    public Material sonar_1_Material;
    public Material sonar_2_Material;
    [Tooltip("Radar Material")]
    public Material radar_1_Material;
    [Tooltip("Lidar Material")]
    public Material lidar_1_Material; // Wichtig Texturen

    protected Renderer[] _renderer;
    protected Hashtable _matList = new Hashtable();
    //protected GameObject[] currentGameObjects;
    public GameObject currentSnappedObject = null;
    protected Scene cur_Scene;
    public string snapped_Tag;
    protected bool isSnapped = false;

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as 
        //this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void Start()
    {
        GetScene();        
    }

    public void Update()
    {
        //currentGameObjects = cur_Scene.GetRootGameObjects();
        //UpdateMaterial(sonar_1_Material);
        GetScene();
        //GetMeshRenderer();
        GetSnappedObj();

        if (isSnapped) //&& cur_Scene.buildIndex != 0)
        {
            // update the materials per Level
            UpdateMaterial();
        }
    }

    private void GetScene()
    {
        cur_Scene = SceneManager.GetActiveScene();
    }

    private void GetSnappedObj()
    {
        if (snapZone.GetCurrentSnappedObject() != null)
        {
            currentSnappedObject = snapZone.GetCurrentSnappedObject();
            isSnapped = true;
        }
        else
        {
            isSnapped = false;
        }
    }

    private void UpdateMaterial()
    {
        Debug.Log("#######################");
        snapped_Tag = snapZone.GetCurrentSnappedObject().tag;

        switch (snapped_Tag)
        {
            case "SonarSensor_1":
                //Update Material
                UpdateMaterial(sonar_1_Material);
                break;
            case "SonarSensor_2":
                //Update Material
                UpdateMaterial(sonar_2_Material);
                break;
            case "LidarSensor":
                //Update Material
                UpdateMaterial(lidar_1_Material);
                break;
            case "RadarSensor":
                //Update Material
                UpdateMaterial(radar_1_Material);
                break;
            case "CameraSensor":
                //Revert Material
                ResetMaterial();
                break;
            default:
                //If no other case found
                ResetMaterial();
                break;
        }
    }

    private void UpdateMaterial(Material material)
    {
        foreach (Renderer rend in _renderer)
        {
          if (rend != null && rend.tag != "Controller") { rend.material = material; }  
        }
    }

    private void ResetMaterial()
    {
        foreach (Renderer _renderer in _renderer)
        {
            if (_renderer != null)
            {
                _renderer.materials = _matList[_renderer] as Material[];
            }
        }
    }

    private void GetMaterials()
    {
        foreach (Renderer rend in _renderer)
        {
            if (rend != null)
            {
                _matList.Add(rend, rend.sharedMaterial);
            }
        }
    }

    private void GetMeshRenderer()
    {
        _renderer = GameObject.FindObjectsOfType<Renderer>();
        GetMaterials();
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        if (scene.buildIndex != 0)
        {
            GetMeshRenderer();
        }
           
    }

}