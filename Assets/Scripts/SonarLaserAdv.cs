using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRTK; 

    public class SonarLaserAdv : MonoBehaviour
    {
        public float ringRadius;
        public VRTK_InteractableObject laserPistol;
        private LineRenderer lineRenderer;
        public Material material;
        public float sonarLifeTime;
        public LaserController controller;
        private List<Vector4> sonarOrigins = new List<Vector4>();
        Game_Manager _controller = Game_Manager.Instance;


    protected virtual void OnEnable()
        {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

       
            controller.material = material;
    }

        protected virtual void OnDisable()
        {
          
        }


        public void ShootSonar()
        {
        if (laserPistol.IsGrabbed() && _controller.GetSnappedPatrone() == "SonarSensor_2")
        {
            lineRenderer.enabled = true;
            StartCoroutine(WaitSonarShot());
            lineRenderer.SetPosition(0, transform.position);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                lineRenderer.SetPosition(1, hit.point);
                var sonarOrigin = hit.point;
                sonarOrigins.Add(new Vector4(sonarOrigin.x, sonarOrigin.y, sonarOrigin.z, 0));
                Debug.Log($"Hit detected: x {sonarOrigin.x} y {sonarOrigin.y}");
            }

            else lineRenderer.SetPosition(1, transform.forward * 5000);
        }
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {

        }

        void Update()
        {
            lineRenderer.SetPosition(0, transform.position);
            sonarOrigins = sonarOrigins
               .Select(hit => new Vector4(hit.x, hit.y, hit.z, hit.w + (Time.deltaTime / sonarLifeTime)))
               .Where(hit => hit.w <= 1).ToList(); // delete all invalid elements from list

            controller.sonarHits = sonarOrigins.ToArray();            
        }

        IEnumerator WaitSonarShot()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            lineRenderer.enabled = false;
        }
    }