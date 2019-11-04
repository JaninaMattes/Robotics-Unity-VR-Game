using UnityEngine;

public class SonarShaderConfigure : MonoBehaviour
{
    // Reference to object material
    public Material SonarMaterial;

    private void Start()
    {
        foreach (Renderer rend in GetComponentsInChildren<Renderer>(true))
        {
            Texture mainTex = rend.material.mainTexture;
            rend.material = SonarMaterial;
            rend.material.mainTexture = mainTex;
        }
    }
    
}
