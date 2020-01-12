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
    [Tooltip("Excluded Tag List")]
    public List<string> excludeTags = new List<string>();
    protected Hashtable _matList = new Hashtable();
    protected Renderer[] _renderer;
    protected List<string> exclude = new List<string>();
    //protected GameObject[] currentGameObjects;
    protected GameObject currentSnappedObject = null;
    protected Scene cur_Scene;
    protected string snapped_Tag = null;
    protected string comp_Tag = null;
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
        exclude = excludeTags;
        // Kann beliebig erweitert werden
    }

    public void Update()
    {
        GetScene();
        GetSnappedObj();

        if (isSnapped && cur_Scene.buildIndex != 0)
        {
            snapped_Tag = currentSnappedObject.tag;
                if (snapped_Tag != comp_Tag)
            {
                // update the materials per Level
                UpdateMaterial(snapped_Tag);
            }
            comp_Tag = snapped_Tag;
        }
    }

    public void ResetMaterial(GameObject gameObject)
    {
        ResetMaterialFor(gameObject);
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

    private void UpdateMaterial(string tag)
    {
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
        LightmapSettings.lightmaps = null;
        foreach (Renderer rend in _renderer)
        {
          if (rend != null && rend.tag != "Controller") // TODO: Über Layer definieren --> Belt/Patrone/Hände/Player/Guns/Bucketlist/Bucket etc
            {
                Material[] m = rend.materials;

                // TODO: Check if Material is Water/Glas for SonarShader
                for (int i = 0; i < m.Length; i++)
                {
                    m[i] = material;
                    rend.materials = m;
                }
            }  
        }
    }

    private void ResetMaterial()
    {
        foreach (Renderer rend in _renderer)
        {
            if (rend != null)
            {
                rend.materials = _matList[rend] as Material[];
            }
        }
    }

    private void ResetMaterialFor(GameObject gameObject)
    {
        foreach (Renderer rend in _renderer)
        {
            if (rend != null)
            {
                rend.materials = _matList[gameObject.GetComponent<Renderer>()] as Material[];
            }
        }
    }

    private void GetMaterials()
    {
        foreach (Renderer rend in _renderer)
        {
            if (rend != null)
            {
                _matList.Add(rend, rend.materials);
            }
        }
    }

    private void GetMeshRenderer()
    {
        _renderer = GameObject.FindObjectsOfType<Renderer>();
        GetMaterials();
    }

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        if (scene.buildIndex != 0)
        {
            GetMeshRenderer();
        }           
    }

}