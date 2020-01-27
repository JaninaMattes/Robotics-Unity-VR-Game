using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColorChange : MonoBehaviour
{
    public Material TargetMat;
    public List<string> excludeObj;
    // Start is called before the first frame update
    void Start()
    {
        Game_Manager controller = Game_Manager.Instance;

        controller.SetExcludeTag(excludeObj);
        
        controller.GetMeshRenderer();

        controller.UpdateMaterial(TargetMat);
        
    }

}
