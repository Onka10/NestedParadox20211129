using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MainCamera
{
    public class CameraState : MonoBehaviour
    {
        private Vector3 movingDistance;
        public Vector3 MovingDistance { get { return movingDistance; } }
        private Transform myTransform;
        private Vector3 previousPosition;        
        // Start is called before the first frame update
        void Start()
        {
            movingDistance = Vector3.zero;
            myTransform = transform;
            previousPosition = new Vector3(0, 0, -10);            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            movingDistance = myTransform.position - previousPosition;
            previousPosition = myTransform.position;
        }
    }

}
