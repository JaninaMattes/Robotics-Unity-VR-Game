using UnityEngine;
using System.Collections;


    [ExecuteInEditMode] //Execution of all instances during Edit-Mode. By default, Monobehaviours only get executed in Play-Mode
    [RequireComponent(typeof(Camera))] //Adds the required component automatically in order to allow the script to run on any GameObject which it is attached to.
    public class PixelEffect : MonoBehaviour
    {
        [Header("Pixels size")] //Description Header in Inspector
        [Range(1.0f, 20f)] //Slider in Inspector
        public float pixelWidth = 0.05f;
        [Range(1.0f, 20f)]
        public float pixelHeight = 0.05f;

        public Material pixelMaterial = null;
        public GameObject hitObject; // GameObject which is hit by the Raycast

        //Creates a Material with the PixelShader
        void SetMaterial()
        {
            pixelMaterial = new Material(Shader.Find("Custom/PixelShader"));
        }

        //Material will be cerated, as soon as the GameObject with this attached script is active/enabled.
        void OnEnable()
        {
            SetMaterial();
        }

        void OnDisable()
        {
            pixelMaterial = null;
        }

        //OnRenderImage is called after all rendering is complete to render image. Post Processing.
        //It allows you to modify final image by processing it with shader based filters.
        //The incoming image is source render texture.The result should end up in destination render texture.You must always issue a Graphics.Blit or render a fullscreen quad if you override this method.
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {

            if (pixelMaterial == null)
            {
                Graphics.Blit(source, destination);
                return;
            }
            //Set Pixelscale according to Screendata (source image data) in the shader.
            pixelMaterial.SetFloat("_PixelWidth", pixelWidth);
            pixelMaterial.SetFloat("_PixelHeight", pixelHeight);
            pixelMaterial.SetFloat("_ScreenWidth", source.width);
            pixelMaterial.SetFloat("_ScreenHeight", source.height);
           //Use the pixelMaterial/shader applied on the source image as a final result for the destination -> post processing.
            Graphics.Blit(source, destination, pixelMaterial);
        }

        void Update()
        {
        //Raycast pointing from the GameObjects camera to get the View direction / center spot of view direction.
        //The hitObject is the GameObject hit by the Raycast.
        Transform cameraTransform = Camera.main.transform;
        RaycastHit HitInfo;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 20.0f))
        {

            hitObject = HitInfo.transform.gameObject;
        }
        else
        {
            hitObject = null;
        }
        }
    }



