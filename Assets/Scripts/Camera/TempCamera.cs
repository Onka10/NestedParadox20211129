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
        private TempCharacter tempCharacter;
        private Transform myTransform;
        private IReadOnlyReactiveProperty<int> characterDirection;

        private PlayerInput _playerinput;

        private void Start()
        {
            _playerinput = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<PlayerInput>();
            myTransform = transform;
            characterDirection = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<PlayerInput>().CurrentDirection;
            characterDirection.Subscribe(x =>
            {
                distanceOffset = new Vector3(distanceOffset.x * -1, distanceOffset.y, distanceOffset.z);
            });
        }

        private void FixedUpdate()
        {
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
                myTransform.position = new Vector3(Mathf.Lerp(myTransform.position.x, _playerinput.MyTransform.position.x - distanceOffset.x, 0.0001f),
                                             1.51f,
                                             Mathf.Lerp(myTransform.position.z, _playerinput.MyTransform.position.z - distanceOffset.z, 0.05f)
                                             );
            }
            else
            {
                myTransform.position = new Vector3(Mathf.Lerp(myTransform.position.x, _playerinput.MyTransform.position.x - distanceOffset.x, 0.05f),
                                             1.51f,
                                             Mathf.Lerp(myTransform.position.z, _playerinput.MyTransform.position.z - distanceOffset.z, 0.05f)
                                             );
            }            
        }
    }

}
