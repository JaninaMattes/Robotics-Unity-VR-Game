using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private static Game_Manager _Instance = null;
    // Game score
    public int playerScore { get; set; }
    public int playerHealth { get; set; }
    // Machine Learning Simulation
    protected List<GameObject> _bucketList = new List<GameObject>();
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
}
