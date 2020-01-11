using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SnapMat : MonoBehaviour
{
    private Hashtable _matList = new Hashtable();
    private Renderer[] _renderers;
    public Material changeMaterial;
    public VRTK_SnapDropZone dropZone;
    private LightmapData[] lightmapData;
    private string tag = null;
    private string compareTag = null;

    void Start()
    {
        lightmapData = LightmapSettings.lightmaps;
        _renderers = GameObject.FindObjectsOfType<Renderer>();
        foreach (Renderer _renderer in _renderers)
        {
            if (_renderer != null)
            {
                _matList.Add(_renderer, _renderer.materials);
            }
        }
    }

    void Update()
    {
        if (dropZone.GetCurrentSnappedObject() != null)
        {
            tag = dropZone.GetCurrentSnappedObject().tag;

            if (tag != compareTag)
            {
                SwitchCases(tag);
                compareTag = tag;
            }
        }

    }

    

    public void SwitchCases(string tag)
    {
        switch (tag)
        {
            case "SonarPatrone":
                SetMaterial(changeMaterial);
                break;
            case "LidarPatrone":
                ResetMaterial();
                break;
            case "CameraPatrone":

                break;
            default:
                return;
                
        }
    }

    public void SetMaterial(Material mat)
    {
        LightmapSettings.lightmaps = null;
        foreach (Renderer _renderer in _renderers)
        {
            
            if (_renderer != null)
            {
                Material[] m = _renderer.materials;
              
                    for (int i = 0; i < m.Length; i++)
                    {
                        m[i] = mat;
                        _renderer.materials = m;
                    }
                
            }
        }
    }

    public void ResetMaterial()
    {
        LightmapSettings.lightmaps = lightmapData;
        foreach (Renderer _renderer in _renderers)
        {
            if (_renderer != null)
            {
                _renderer.materials = _matList[_renderer] as Material[];

            }
        }
    }
}
