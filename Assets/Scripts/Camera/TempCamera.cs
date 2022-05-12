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
        private TempCharacter tempCharacter;
        private Transform myTransform;
        private IReadOnlyReactiveProperty<int> characterDirection;

        private PlayerMove _playermove;

        //ボスカメラのフラグ
        private bool isBossStage;

        private void Start()
        {
            isBossStage = false;
            _playermove = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<PlayerMove>();
            myTransform = transform;
            characterDirection = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<PlayerMove>().CurrentDirection;
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
        }

        public void ChangeToNormalCamera()
        {
            isBossStage = false;
        }

        private void FixedUpdate()
        {
            if(isBossStage)
            {
                myTransform.position = bossCameraPos;
                return;
            }
            if(myTransform.position.x < 6.4f)
            {
                myTransform.position = new Vector3(6.4f, 1.51f, -10);
            }
            else if(myTransform.position.x > 32)
            {
                myTransform.position = new Vector3(32, 1.51f, -10);
            }
            else if(myTransform.position.x == 6.4f || myTransform.position.x == 32)
            {
                myTransform.position = new Vector3(Mathf.Lerp(myTransform.position.x, _playermove.MyTransform.position.x - distanceOffset.x, 0.0001f),
                                             1.51f,
                                             Mathf.Lerp(myTransform.position.z, _playermove.MyTransform.position.z - distanceOffset.z, 0.05f)
                                             );
            }
            else
            {
                myTransform.position = new Vector3(Mathf.Lerp(myTransform.position.x, _playermove.MyTransform.position.x - distanceOffset.x, 0.05f),
                                             1.51f,
                                             Mathf.Lerp(myTransform.position.z, _playermove.MyTransform.position.z - distanceOffset.z, 0.05f)
                                             );
            }            
        }       
    }

}
