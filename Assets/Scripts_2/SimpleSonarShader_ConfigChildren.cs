using UnityEngine;

public class SimpleSonarShader_ConfigChildren : MonoBehaviour
{

    public Material SonarMaterial;

    private void Start()
    {
        // TODO
        foreach (Collider col in GetComponentsInChildren<Collider>(true))
        {
            //add SimpleSonarShader_MenuSelection to each gameobject
            col.gameObject.AddComponent<SimpleSonarShader_MenuSelection>();
        }

        foreach (Renderer rend in GetComponentsInChildren<Renderer>(true))
        {
            Texture mainTex = rend.material.mainTexture;
            rend.material = SonarMaterial;
            rend.material.mainTexture = mainTex;
        }
    }

}
