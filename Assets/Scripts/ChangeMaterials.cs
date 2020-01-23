using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

[ExecuteInEditMode]
public class ChangeMaterials : MonoBehaviour {
    [Header ("Snapdrop Zone Prefab")]
    //public VRTK_SnapDropZone snapZone;
    [Header ("Sensor Material")]
    [Tooltip ("Sonar Materials")]
    public Material sonar_1_Material;
    public Material sonar_2_Material;
    [Tooltip ("Radar Material")]
    public Material radar_1_Material;
    [Tooltip ("Lidar Material")]
    public Material lidar_1_Material; // Wichtig Texturen
    [Tooltip ("Floor Grid Orientation Material")]
    public Material gridorientation_Material;
    public string gridorientation_Tag;
    [Tooltip ("Excluded Tag List")]
    public List<string> excludeTags = new List<string> ();
    [Tooltip ("Spawn")]
    public GameObject spawn;

    // Private Properties
    protected Material[] materials = new Material[3];
    //protected GameObject[] currentGameObjects;
    protected Scene cur_Scene;

    // Singleton to controll all data used by various classes 
    protected Game_Manager controller = Game_Manager.Instance;

    void OnEnable () {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        // SceneManager.sceneLoaded += OnLevelFinishedLoading;
        //snapZone.ObjectSnappedToDropZone += ObjectSnappedToDropZone;
        //snapZone.ObjectUnsnappedFromDropZone += ObjectUnsnappedFromDropZone;
    }

    void OnDisable () {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as 
        //this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        // SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        //snapZone.ObjectSnappedToDropZone -= ObjectSnappedToDropZone;
        //snapZone.ObjectUnsnappedFromDropZone -= ObjectUnsnappedFromDropZone;
    }

    public void Start () {
        GetScene ();
        controller.SetExcludeTag (excludeTags);
        controller.SetGridOrientationTag (gridorientation_Tag);
        controller.SetGridOrientationMaterial (gridorientation_Material);
        // Kann beliebig erweitert werden
        controller.SetSonar (spawn.GetComponent<SonarLaserAdv> ());
        controller.SetRadar (spawn.GetComponent<RadarLaser> ());
        controller.SetLidar (spawn.GetComponent<LiDar2> ());
        controller.SetLaserController (spawn.GetComponent<LaserController> ());
        // Material setzen
        materials[0] = sonar_1_Material;
        materials[1] = sonar_2_Material;
        materials[2] = radar_1_Material;
        controller.SetAllMaterials (materials);
    }

    public void Update () {
        GetScene ();
    }

    public void ResetMaterial (GameObject gameObject) {
        ResetMaterialFor (gameObject);
    }

    private void GetScene () {
        cur_Scene = SceneManager.GetActiveScene ();
    }

     //protected virtual void ObjectSnappedToDropZone (object sender, SnapDropZoneEventArgs e) {
     //   UpdateMaterial (snapZone.GetCurrentSnappedObject ().tag);
    //}

    protected virtual void ObjectUnsnappedFromDropZone (object sender, SnapDropZoneEventArgs e) {

    }

    private void DeactivateAllRenderer () {
        controller.DeactivateAllRenderer ();
    }

    private void ResetMaterial () {
        controller.ResetMaterial ();
    }

    private void ResetMaterialFor (GameObject gameObject) {
        foreach (Renderer rend in controller.GetRenderer ()) {
            if (rend != null) {
                rend.materials = controller.GetMaterial () [gameObject.GetComponent<Renderer> ()] as Material[];
            }
        }
    }

    private void GetMaterials () {
        foreach (Renderer rend in controller.GetRenderer ()) {
            if (rend != null) {
                controller.GetMaterial ().Add (rend, rend.materials);
            }
        }
    }

    private void ActivateAllRenderer () {
        controller.ActivateAllRenderer ();
    }

    private void SetLaserScript (string sensor) {
        controller.SetLaserScript (sensor);
    }

    private void UpdateMaterial (Material material) {
        controller.UpdateMaterial (material);
    }

    // private void UpdateMaterial (string tag) {
    //    controller.UpdateMaterial (tag);
    //}

    public void SetLidarScript () {
        controller.SetLidarScript ();
    }
}