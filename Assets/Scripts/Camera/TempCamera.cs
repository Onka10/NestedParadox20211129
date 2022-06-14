using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using NestedParadox.Players;

namespace MainCamera
{
    public class TempCamera : MonoBehaviour
    {
        [SerializeField] private Vector3 distanceOffset = new Vector3();
        [SerializeField] private Vector3 bossCameraPos;
        [SerializeField] private float normalStageCameraSize;
        private TempCharacter tempCharacter;        
        [SerializeField] private Camera camera;
        private IReadOnlyReactiveProperty<int> characterDirection;

        [SerializeField] private PlayerMove _playermove;

        //ボスカメラのフラグ
        private bool isBossStage;

        private void Start()
        {
            ChangeToNormalCamera();            
            characterDirection = _playermove.CurrentDirection;
            Vector3 distanceOffset_temp = new Vector3(distanceOffset.x, distanceOffset.y, distanceOffset.z);
            characterDirection.Subscribe(x =>
            {
                if(x == -1)
                {
                    distanceOffset = new Vector3(distanceOffset_temp.x, distanceOffset_temp.y, distanceOffset_temp.z);
                }
                else if(x == 1)
                {
                    distanceOffset = new Vector3(distanceOffset_temp.x * -1, distanceOffset_temp.y, distanceOffset_temp.z);
                }
            });
        }

        
        public void ChangeToBossCamera()
        {
            isBossStage = true;
            camera.orthographicSize = 8.0f;
            base.transform.position = bossCameraPos;
        }

        public void ChangeToNormalCamera()
        {
            isBossStage = false;
            camera.orthographicSize = normalStageCameraSize;
        }
        

        private void FixedUpdate()
        {
            if(isBossStage)
            {                
                return;
            }

            if(transform.position.x < 6.4f)
            {
                transform.position = new Vector3(6.4f, 1.51f, -10);
            }
            else if(transform.position.x > 32)
            {
                transform.position = new Vector3(32, 1.51f, -10);
            }
            else if(transform.position.x == 6.4f || transform.position.x == 32)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, _playermove.transform.position.x - distanceOffset.x, 0.0001f),
                                             1.51f,
                                             Mathf.Lerp(transform.position.z, _playermove.transform.position.z - distanceOffset.z, 0.05f)
                                             );
            }
            else
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, _playermove.transform.position.x - distanceOffset.x, 0.05f),
                                             1.51f,
                                             Mathf.Lerp(transform.position.z, _playermove.transform.position.z - distanceOffset.z, 0.05f)
                                             );
            }            
        }       
    }

}
