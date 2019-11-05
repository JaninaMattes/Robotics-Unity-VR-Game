namespace VRTK.Examples
{
    using UnityEngine;

    public class Lidar_Basic : VRTK_InteractableObject
    {
        public GameObject lidarPointPrefab;
        public int numberOfPoints = 10;
        public float pointSize = 1;
        public float scanSize = 0.3f;
        public int scanRadius = 20;
        [HideInInspector]
        public Vector3 dir;
        public Vector3 emissionPosOffset;


        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
        }



        protected void FireLidar()
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position + emissionPosOffset, transform.TransformDirection(dir), out hit, scanRadius))
            {
                GameObject hitPoint = Instantiate(lidarPointPrefab, hit.point, Quaternion.identity);
                Color pointCol = Color.Lerp(Color.green, Color.blue, hit.distance / 3);
                hitPoint.GetComponent<MeshRenderer>().material.SetColor("_UnlitColor", pointCol);
                hitPoint.transform.localScale = new Vector3(pointSize, pointSize, pointSize);

            }

        }

    }
}
