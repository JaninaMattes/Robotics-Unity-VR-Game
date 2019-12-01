namespace VRTK.Examples
{


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SonarLaser : MonoBehaviour
    {

        public VRTK_InteractableObject laserPistol;


        private LineRenderer lr;
        public Material material;
        public Vector4 sonarOrigin = Vector4.one;
        public float speed;
        bool test = false;

        void Start()
        {
            lr = GetComponent<LineRenderer>();
            lr.enabled = false;
        }

        protected virtual void OnEnable()
        {
            laserPistol = (laserPistol == null ? GetComponent<VRTK_InteractableObject>() : laserPistol);

            if (laserPistol != null)
            {
                laserPistol.InteractableObjectUsed += InteractableObjectUsed;
                laserPistol.InteractableObjectUnused += InteractableObjectUnused;
            }

        }

        protected virtual void OnDisable()
        {
            if (laserPistol != null)
            {
                laserPistol.InteractableObjectUsed -= InteractableObjectUsed;
                laserPistol.InteractableObjectUnused -= InteractableObjectUnused;
            }
        }


        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            lr.enabled = true;
            StartCoroutine(WaitSonarShot());
            lr.SetPosition(0, transform.position);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                //if (hit.collider) Überprüfung ob getroffenes GameObject einen Collider hat / Auf Kollision überprüfen
                //{
                lr.SetPosition(1, hit.point);
                sonarOrigin = hit.point;
                //}
            }
            else lr.SetPosition(1, transform.forward * 5000);
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {

        }



        void Update()
        {
            sonarOrigin.w = Mathf.Min(sonarOrigin.w + (Time.deltaTime * speed), 1);
            material.SetVector("_SonarOrigin", sonarOrigin);
        }

        IEnumerator WaitSonarShot()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            lr.enabled = false;
        }
    }

}