using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
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
    [Tooltip("Floor Grid Orientation Material")]
    public Material gridorientation_Material;
    public string gridorientation_Tag;
    [Tooltip("Excluded Tag List")]
    public List<string> excludeTags = new List<string>();
    [Tooltip("Spawn")]
    public GameObject spawn;
    //public SonarLaser sonar1;
    public SonarLaserAdv sonar2;
    public RadarLaser radar;
    public LiDar2 lidar;
    public LaserController laser_controller;

    // Private Properties
    protected List<string> exclude = new List<string>();
    //protected GameObject[] currentGameObjects;
    protected Scene cur_Scene;

    // Singleton to controll all data used by various classes 
    protected Game_Manager controller = Game_Manager.Instance;

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        snapZone.ObjectSnappedToDropZone += ObjectSnappedToDropZone;
        snapZone.ObjectUnsnappedFromDropZone += ObjectUnsnappedFromDropZone;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as 
        //this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        snapZone.ObjectSnappedToDropZone -= ObjectSnappedToDropZone;
        snapZone.ObjectUnsnappedFromDropZone -= ObjectUnsnappedFromDropZone;
    }

    public void Start()
    {
        GetScene();
        exclude = excludeTags;
        // Kann beliebig erweitert werden
        sonar2 = spawn.GetComponent<SonarLaserAdv>();
        radar = spawn.GetComponent<RadarLaser>();
        lidar = spawn.GetComponent<LiDar2>();
        laser_controller = spawn.GetComponent<LaserController>();
    }

    public void Update()
    {
        GetScene();
    }

    public void ResetMaterial(GameObject gameObject)
    {
        ResetMaterialFor(gameObject);
    }

    private void GetScene()
    {
        cur_Scene = SceneManager.GetActiveScene();
    }

    protected virtual void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        UpdateMaterial(snapZone.GetCurrentSnappedObject().tag);
    }

    protected virtual void ObjectUnsnappedFromDropZone(object sender, SnapDropZoneEventArgs e)
    {
         
    }

    private void UpdateMaterial(string tag)
    {
         switch (tag)
         {
             case "SonarSensor_1":
                 //Update Material
                 UpdateMaterial(sonar_1_Material);
                 ActivateAllRenderer();
                 SetLaserScript(tag);
                 break;
             case "SonarSensor_2":
                 //Update Material
                 UpdateMaterial(sonar_2_Material);
                 ActivateAllRenderer();
                 SetLaserScript(tag);
                 break;
             case "LidarSensor":
                //Update Material
                //UpdateMaterial(lidar_1_Material);
                 DeactivateAllRenderer();
                 SetLidarScript();
                 break;
             case "RadarSensor":
                 //Update Material
                 UpdateMaterial(radar_1_Material);
                 ActivateAllRenderer();
                 SetLaserScript(tag);
                 break;
             case "CameraSensor":
                 //Revert Material
                 ResetMaterial();
                 ActivateAllRenderer();
                //TODO: Camerasensor
                break;
             default:
                 //If no other case found
                 ResetMaterial();
                 ActivateAllRenderer();
                 break;

         }
    }

    private void DeactivateAllRenderer()
    {
        foreach (Renderer rend in controller.GetRenderer())
        {
            if (rend != null && !exclude.Contains(rend.tag))
            {
                rend.enabled = false;
            }
        }
    }

    private void ActivateAllRenderer()
    {
        foreach (Renderer rend in controller.GetRenderer())
        {
            if (rend != null && rend.tag != "LineRenderer")
            {
                rend.enabled = true;
            }
        }
    }

    private void UpdateMaterial(Material material)
    {
        Material[] m;
        //LightmapSettings.lightmaps = null;
        foreach (Renderer rend in controller.GetRenderer())
        {
          if (rend != null && !exclude.Contains(rend.tag)) //TODO: Über Layer definieren --> Belt/Patrone/Hände/Player/Guns/Bucketlist/Bucket etc
           {
                m = rend.materials;
                //Set grid orientation to floor
                if (rend.tag == gridorientation_Tag)
                {
                    rend.material = gridorientation_Material;
                }
                else
                {                 
                    //TODO: Check if Material is Water/Glas for SonarShader
                    for (int i = 0; i < m.Length; i++)
                    {
                        m[i] = material;
                    }
                    rend.materials = m;

                }               
           }  
        }
    }

    private void ResetMaterial()
    {
        foreach (Renderer rend in controller.GetRenderer())
        {
            if (rend != null)
            {
                rend.materials = controller.GetMaterial()[rend] as Material[];
            }
        }
    }

    private void ResetMaterialFor(GameObject gameObject)
    {
        foreach (Renderer rend in controller.GetRenderer())
        {
            if (rend != null)
            {
               rend.materials = controller.GetMaterial()[gameObject.GetComponent<Renderer>()] as Material[];
            }
        }
    }

    private void GetMaterials()
    {
        foreach (Renderer rend in controller.GetRenderer())
        {
            if (rend != null)
            {
                controller.GetMaterial().Add(rend, rend.materials);
            }
        }
    }

    private void GetMeshRenderer()
    {
        Renderer[] list = GameObject.FindObjectsOfType<Renderer>();
        controller.Set(list);
        GetMaterials();
    }

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        //if (scene.buildIndex != 0)
        //{
            GetMeshRenderer();
        //}           
    }

    public void SetLaserScript(string sensor)
    {
        laser_controller.enabled = true;
        // Lidar
        lidar.lidarActive = false;

        if (sensor == "SonarSensor_1") {
            // Sonar 
            //sonar1.enabled = true;
            //sonar1.Material = sonar_1_Material;
        }
        else if (sensor == "SonarSensor_2") {
            // Sonar 
            sonar2.enabled = true;
            sonar2.material = sonar_2_Material;
            radar.enabled = false;
        }
        else if (sensor == "RadarSensor") {
            // Sonar 
            radar.enabled = true;
            radar.material = radar_1_Material;
            sonar2.enabled = false;
        }
        else { }                 
    }

    public void SetLidarScript()
    {

        // Laser
        //sonar1.enabled = false;
        sonar2.enabled = false;
        radar.enabled = false;
        laser_controller.enabled = false;
        // Lidar
        lidar.lidarActive = true;
        
    }
}

 