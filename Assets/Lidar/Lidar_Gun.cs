namespace VRTK.Examples
{
    using UnityEngine;

    public class Lidar_Gun : Lidar_Basic
    {
    


        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            for (int i = 0; i < numberOfPoints; i++)
            {
                dir = Vector3.right + new Vector3(Random.Range(-scanSize, scanSize), Random.Range(-scanSize, scanSize), Random.Range(-scanSize, scanSize));
                FireLidar();
                Debug.Log("Gun Fired");
            }
        }

  



    }
}
