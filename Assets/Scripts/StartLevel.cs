namespace VRTK.Examples
{
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    // Select the correct level index for a scene
    public int LevelIndex;
    public VRTK_InteractableObject sceneChange;
    public VRTK_InteractableObject laserGun;
    public GameObject unLoad;
    private float timeElapsed;
 
        // Allow a delay in loading
        [SerializeField]
    private float delayBeforeLoading = 5.0f;

         void Start()
        {
            sceneChange = this.GetComponent<VRTK_InteractableObject>();
        }

        protected virtual void OnEnable()
        {
            sceneChange = (sceneChange == null ? GetComponent<VRTK_InteractableObject>() : sceneChange);

            if (sceneChange != null)
            {
                sceneChange.InteractableObjectUsed += InteractableObjectUsed;
                sceneChange.InteractableObjectUnused += InteractableObjectUnused;
            }

        }

        protected virtual void OnDisable()
        {
            if (sceneChange != null)
            {
                sceneChange.InteractableObjectUsed -= InteractableObjectUsed;
                sceneChange.InteractableObjectUnused -= InteractableObjectUnused;
            }
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            if(laserGun.IsGrabbed() == false)
            {
                laserGun.gameObject.transform.SetParent(unLoad.transform);
            }
            SceneManager.LoadScene(LevelIndex, LoadSceneMode.Additive);
            DestroyImmediate(unLoad);
        }
        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {

        }
    }
}
