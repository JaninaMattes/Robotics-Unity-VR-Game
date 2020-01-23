using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class Game_Manager {
    private static Game_Manager _Instance = null;
    // Game score
    private Game_Manager () { }

    /// <summary>
    /// Singleton Pattern to restrict instantiation
    /// </summary>
    public static Game_Manager Instance {
        get {
            if (_Instance == null) {
                _Instance = new Game_Manager ();

            }
            return _Instance;
        }
    }

    protected int playerScore = 0;
    protected int playerHealth = 0;
    // Machine Learning Simulation
    protected List<GameObject> _bucketList = new List<GameObject> ();
    //protected Dictionary<GameObject, Vector3> _originalPosition = new Dictionary<GameObject, Vector3>();
    protected Dictionary<GameObject, Vector3> _originalPositions = new Dictionary<GameObject, Vector3> ();
    // Material Changer
    protected Renderer[] _renderer;
    protected Hashtable _matList = new Hashtable ();
    protected Material[] _allMaterials;
    protected Material gridorientation_Material;

    //public SonarLaser sonar1;
    protected SonarLaserAdv _sonar2;
    protected RadarLaser _radar ;
    protected LiDar2 _lidar ;
    protected LaserController _laser_controller ;

    protected List<string> exclude = new List<string> ();
    protected string gridorientation_Tag;

    public void SetGridOrientationTag (string gridorientation_Tag) {
        gridorientation_Tag = gridorientation_Tag;
    }

    public string GetGridOrientationTag () {
        return gridorientation_Tag;
    }
    public void SetExcludeTag (List<string> exclude) {
        exclude = exclude;
    }
    public List<string> GetExcludeTag () {
        return exclude;
    }

    public void SetSonar (SonarLaserAdv sonar) {
        _sonar2 = sonar;
    }
    public void SetRadar (RadarLaser radar) {
        _radar = radar;
    }
    public void SetLidar (LiDar2 lidar) {
        _lidar = lidar;
    }
    public void SetLaserController (LaserController laser_controller) {
        _laser_controller = laser_controller;
    }

    public SonarLaserAdv GetSonar () {
        return _sonar2;
    }
    public RadarLaser GetRadar () {
        return _radar;
    }
    public LiDar2 GetLidar () {
        return _lidar;
    }
    public LaserController GetLaserController () {
        return _laser_controller;
    }

    public void Set (Dictionary<GameObject, Vector3> _originalPositions) {
        this._originalPositions = _originalPositions;
    }
    public void Set (List<GameObject> _bucketList) {
        this._bucketList = _bucketList;
    }

    public void SetRenderer (Renderer[] _renderer) {
        this._renderer = _renderer;
    }

    public void SetMaterials (Renderer[] renderer) {
        this._matList = new Hashtable ();

        foreach (Renderer rend in renderer) {
            if (rend != null) {
                this._matList.Add (rend, rend.materials);
            }
        }
    }

    /// <summary>
    /// Set all materials that come grom the Change Materials Script.
    /// </summary>
    /// <param name="_allMaterial"></param>
    public void SetAllMaterials (Material[] _allMaterial) {
        this._allMaterials = _allMaterial;
    }

    public Material[] GetAllMaterials () {
        return this._allMaterials;
    }

    public void AddToBucketList (GameObject _bucketList) {
        this._bucketList.Add (_bucketList);
    }

    public void Remove (GameObject _bucketList) {
        this._bucketList.Remove (_bucketList);
    }

    public List<GameObject> GetBucketObjects () {
        return this._bucketList;
    }

    public Renderer[] GetRenderer () {
        return this._renderer;
    }

    public Hashtable GetMaterial () {
        return this._matList;
    }

    public void SetPlayerHealth (int playerHealth) {
        this.playerHealth = playerHealth;
    }

    public void AddPlayerHealth () {
        ++this.playerHealth;
    }

    public void ReducePlayerHealth () {
        --this.playerHealth;
    }

    public int GetPlayerHealth () {
        return this.playerHealth;
    }
    public void SetPlayerScore (int playerScore) {
        this.playerScore = playerScore;
    }

    public void AddPlayerScore () {
        ++this.playerScore;
    }

    public void ReducePlayerScore () {
        --this.playerScore;
    }

    public int GetPlayerScore () {
        return this.playerScore;
    }

    public void AddPositions (GameObject obj) {
        if (!_originalPositions.ContainsKey (obj)) {
            this._originalPositions.Add (obj, obj.transform.position);
        }
    }

    public Dictionary<GameObject, Vector3> GetPositions () {
        return this._originalPositions;
    }

    public Vector3 FindOriginalPos (GameObject obj) {

        Vector3 position = new Vector3 ();
        foreach (KeyValuePair<GameObject, Vector3> entry in _originalPositions) {
            if (obj == entry.Key) {
                position = entry.Value;
            }
        }
        return position;
    }

    public void GetMeshRenderer () {
        Renderer[] list = GameObject.FindObjectsOfType<Renderer> ();
        SetRenderer (list);
    }

    public void CleanUp () {
        Dictionary<GameObject, Vector3> positions = null;
        List<GameObject> list = null;
        _bucketList = list;
        _originalPositions = positions;
    }

    public void ResetMaterial (GameObject obj) {
        Renderer m_ObjectRenderer = obj.GetComponent<Renderer> ();

        foreach (Renderer rend in _renderer) {
            if (rend != null && rend == m_ObjectRenderer) {
                rend.materials = _matList[rend] as Material[];
            }
        }
    }

    public void ResetMaterial () {
        foreach (Renderer rend in GetRenderer ()) {
            if (rend != null) {
                rend.materials = GetMaterial () [rend] as Material[];
            }
        }
    }

    /// <summary>
    /// Function that contains a switch case 
    /// to toggle the right renderer and material
    /// per sensor used 
    /// </summary>
    /// <param name="tag"></param>
    public void UpdateMaterial (string tag) {
        switch (tag) {
            case "SonarSensor_1":
                //Update Material
                UpdateMaterial (_allMaterials[0]);
                ActivateAllRenderer ();
                SetLaserScript (tag);
                break;
            case "SonarSensor_2":
                //Update Material
                UpdateMaterial (_allMaterials[1]);
                ActivateAllRenderer ();
                SetLaserScript (tag);
                break;
            case "LidarSensor":
                //Update Material
                //UpdateMaterial(lidar_1_Material);
                DeactivateAllRenderer ();
                SetLidarScript ();
                break;
            case "RadarSensor":
                //Update Material
                UpdateMaterial (_allMaterials[2]);
                ActivateAllRenderer ();
                SetLaserScript (tag);
                break;
            case "CameraSensor":
                //Revert Material
                ResetMaterial ();
                ActivateAllRenderer ();
                //TODO: Camerasensor
                break;
            default:
                //If no other case found
                ResetMaterial ();
                ActivateAllRenderer ();
                break;

        }
    }

    public void ActivateAllRenderer () {
        foreach (Renderer rend in GetRenderer ()) {
            if (rend != null && rend.tag != "LineRenderer") {
                rend.enabled = true;
            }
        }
    }

    public void DeactivateAllRenderer () {
        foreach (Renderer rend in GetRenderer ()) {
            if (rend != null && !exclude.Contains (rend.tag)) {
                rend.enabled = false;
            }
        }
    }

    public void UpdateMaterial (Material material) {
        Material[] m;
        //LightmapSettings.lightmaps = null;
        foreach (Renderer rend in GetRenderer ()) {
            if (rend != null && !exclude.Contains (rend.tag)) //TODO: Über Layer definieren --> Belt/Patrone/Hände/Player/Guns/Bucketlist/Bucket etc
            {
                m = rend.materials;
                //Set grid orientation to floor
                if (rend.tag == gridorientation_Tag) {
                    rend.material = gridorientation_Material;
                } else {
                    //TODO: Check if Material is Water/Glas for SonarShader
                    for (int i = 0; i < m.Length; i++) {
                        m[i] = material;
                    }
                    rend.materials = m;

                }
            }
        }
    }

    public void SetLaserScript (string sensor) {
        _laser_controller.enabled = true;
        // Lidar
        _lidar.lidarActive = false;

        if (sensor == "SonarSensor_1") {
            // Sonar 
            //sonar1.enabled = true;
            //sonar1.Material = sonar_1_Material;
        } else if (sensor == "SonarSensor_2") {
            // Sonar 
            _sonar2.enabled = true;
            _sonar2.material = _allMaterials[1];
            _radar.enabled = false;
            _laser_controller.sonarHits = new Vector4[50];
        } else if (sensor == "RadarSensor") {
            // Sonar 
            _radar.enabled = true;
            _radar.material = _allMaterials[2];
            _sonar2.enabled = false;
            _laser_controller.sonarHits = new Vector4[50];
        } else { }
    }

    public void SetLidarScript () {

        // Laser
        //sonar1.enabled = false;
        _sonar2.enabled = false;
        _radar.enabled = false;
        _laser_controller.enabled = false;
        // Lidar
        _lidar.lidarActive = true;
    }

}