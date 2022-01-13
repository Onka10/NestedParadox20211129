using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MainCamera
{
    public class CameraState : MonoBehaviour
    {
        private float movingDistance;
        public float MovingDistance { get { return movingDistance; } }
        private Transform myTransform;
        private Vector3 previousPosition;        
        // Start is called before the first frame update
        void Start()
        {
            movingDistance = 0;
            myTransform = transform;
            previousPosition = new Vector3(0, 0, -10);            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            Vector3 movingVector = myTransform.position - previousPosition;
            if (movingVector.x > 0)
            {
                movingDistance = movingVector.magnitude;
            }
            else
            {
                movingDistance = movingVector.magnitude * -1;
            }
            previousPosition = myTransform.position;
        }
    }

}
