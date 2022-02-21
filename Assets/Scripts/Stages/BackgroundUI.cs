using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace NestedParadox.Stages
{
    public class BackgroundUI : MonoBehaviour
    {
        MainCamera.CameraState cameraState;
        [SerializeField] float x_movingSpeed;
        [SerializeField] float y_movingSpeed;
        private Transform myTransform;
        // Start is called before the first frame update
        void Start()
        {
            myTransform = transform;
            cameraState = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera.CameraState>();            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            myTransform.position -= new Vector3(cameraState.MovingDistance.x * x_movingSpeed,0,0);
            myTransform.position -= new Vector3(0, cameraState.MovingDistance.y * y_movingSpeed, 0);
        }        
    }
}

