using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private static Game_Manager _Instance = null;
    // Game score
    protected int playerScore = 0;
    protected int playerHealth = 0;
    // Machine Learning Simulation
    protected List<GameObject> _bucketList = new List<GameObject>();
    protected Dictionary<string, Vector3> _originalPosition = new Dictionary<string, Vector3>();
    // Material Changer
    protected Renderer[] _renderer;
    protected Hashtable _matList = new Hashtable();

    private Game_Manager() { }

    /// <summary>
    /// Singleton Pattern to restrict instantiation
    /// </summary>
    public static Game_Manager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new Game_Manager();
            }
            return _Instance;
        }
    }

    public void Set(Dictionary<string, Vector3> _originalPosition)
    {
        this._originalPosition = _originalPosition;
    }
    public void Set(List<GameObject> _bucketList)
    {
        this._bucketList = _bucketList;
    }

    public void Set(Renderer[] _renderer)
    {
        this._renderer = _renderer;
    }

    public void Set(Hashtable _matList)
    {
        this._matList = _matList;
    }

    public void Add(GameObject _bucketList)
    {
        this._bucketList.Add(_bucketList);
    }

    public void Remove(GameObject _bucketList){
        this._bucketList.Remove(_bucketList);
    }

    public List<GameObject> GetBucketObjects()
    {
        return this._bucketList;
    }

    public Renderer[] GetRenderer()
    {
        return this._renderer;
    }

    public Hashtable GetMaterial()
    {
        return this._matList;
    }

    public void SetPlayerHealth(int playerHealth){
        this.playerHealth = playerHealth;
    }

    public void AddPlayerHealth(){
        ++ this.playerHealth;
    }

    public void ReducePlayerHealth()
    {
        --this.playerHealth;
    }

    public int GetPlayerHealth()
    {
        return this.playerHealth;
    }
    public void SetPlayerScore(int playerScore)
    {
        this.playerScore = playerScore;
    }

    public void AddPlayerScore()
    {
        ++this.playerScore;
    }


    public void ReducePlayerScore()
    {
        --this.playerScore;
    }


    public int GetPlayerScore()
    {
        return this.playerScore;
    }

    public void Add(string tag, Vector3 pos)
    {
        this._originalPosition.Add(tag, pos);
    }

    public Dictionary<string, Vector3> GetPosition( )
    {
        return this._originalPosition;
    }

    public void CleanUp(){
        Dictionary<string, Vector3> position = new Dictionary<string, Vector3>();
        List < GameObject > list = new List<GameObject>();
        _bucketList = list;
        _originalPosition = position;
    }
    public void ResetMaterial(GameObject obj)
    {
        Renderer m_ObjectRenderer = obj.GetComponent<Renderer>();
        foreach (Renderer rend in _renderer)
        {
            if (rend != null && rend == m_ObjectRenderer)
            {
                rend.materials = _matList[rend] as Material[];
            }
        }
    }
}
