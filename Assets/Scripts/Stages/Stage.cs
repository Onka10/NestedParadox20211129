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
            if(cameraState.MovingDistance > 0)
            {
                myTransform.position -= new Vector3(cameraState.MovingDistance * movingSpeed, 0, 0);
            }
            else
            {
                myTransform.position -= new Vector3(cameraState.MovingDistance * movingSpeed, 0, 0);
            }
        }        
    }
}

