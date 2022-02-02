using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameStage
{
    public class Stage : MonoBehaviour
    {
        MainCamera.CameraState cameraState;
        [SerializeField] float movingSpeed;
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
                myTransform.position -= cameraState.MovingDistance * movingSpeed;
        }        
    }
}

