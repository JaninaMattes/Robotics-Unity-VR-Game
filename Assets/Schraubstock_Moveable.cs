namespace VRTK.Examples
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using VRTK.Controllables;


    public class Schraubstock_Moveable : MonoBehaviour
    {

        public VRTK_BaseControllable controllable;

        public GameObject objectToMove;
        public float maxDistance= 0.2f;




        Vector3 startPos;

        // Start is called before the first frame update


        void Start()
        {
            startPos = objectToMove.transform.position;
        }

        protected virtual void OnEnable()
        {
            controllable = (controllable == null ? GetComponent<VRTK_BaseControllable>() : controllable);
            controllable.ValueChanged += ValueChanged;
        }

        protected virtual void ValueChanged(object sender, ControllableEventArgs e)
        {
            float normalizedVal = e.normalizedValue;

            objectToMove.transform.position = startPos + new Vector3(-normalizedVal * maxDistance, 0, 0);
        }

      

       

        public void MoveSchraubstock(float val)
        {
            gameObject.transform.position = startPos + new Vector3(0, val, 0);
        }


    }

}
