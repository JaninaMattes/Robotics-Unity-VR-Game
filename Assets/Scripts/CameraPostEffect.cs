using UnityEngine;
using System.Collections;

/// <summary>
/// Render the scene to a texture:
/// Needs to be attached to the camera game object to
/// make a blurred outline for glow effects.
/// </summary>
public class CameraPostEffect : MonoBehaviour
{
    Camera AttachedCamera;
    public Shader Post_Outline;
    public Shader DrawSimple;
    Camera TempCam;
    public Material Post_Mat;

    void Start()
    {
        AttachedCamera = GetComponent<Camera>();
        TempCam = new GameObject().AddComponent<Camera>();
        TempCam.enabled = false;
        Post_Mat = new Material(Post_Mat);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //set up a temporary camera
        TempCam.CopyFrom(AttachedCamera);
        TempCam.clearFlags = CameraClearFlags.Color;
        TempCam.backgroundColor = Color.black;

        //cull any layer that isn't the outline - The value is shifted by the value of layer's decimal value
        // Bitmasks are used for performance reasons
        TempCam.cullingMask = 1 << LayerMask.NameToLayer("Outline");

        //make the temporary rendertexture
        RenderTexture TempRT = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.R8);

        //put it to video memory
        TempRT.Create();

        //set the camera's target texture when rendering
        TempCam.targetTexture = TempRT;

        //render all objects this camera can render, but with our custom shader.
        TempCam.RenderWithShader(DrawSimple, "");

        //copy the temporary RT to the final image
        Graphics.Blit(TempRT, destination);

        //release the temporary RT
        TempRT.Release();
    }

}