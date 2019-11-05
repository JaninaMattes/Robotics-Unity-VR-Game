namespace VRTK.Examples
{
    using UnityEngine;
    using System.Collections;

    public class Lidar_Bomb : Lidar_Basic
    {

        public int countdownTime = 4;
        bool activated;

        public int rows = 50;
        public int columns = 20;


        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            if (!activated)
            {
                //activated = true;
                StartCoroutine(StartCountdown(countdownTime));
            }
        }

        int currCountdownValue;
        public IEnumerator StartCountdown(int countdownValue = 5)
        {
            currCountdownValue = countdownValue;
            while (currCountdownValue > 0)
            {
                yield return new WaitForSeconds(1.0f);
                currCountdownValue--;
            }


            //for (int i = 0; i < numberOfPoints; i++)
            //{
            //    dir = Random.onUnitSphere;
            //    FireLidar();
            //}

            float stepX = 360 / columns;
            float stepY = 360 / rows;
            dir = Vector3.down;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    dir = Quaternion.Euler(0, stepX, 0) * dir;
                    FireLidar();
                }
                dir = Quaternion.Euler(stepY, 0, 0) * dir;
            }

        }


    }
}