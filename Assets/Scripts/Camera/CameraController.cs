using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using NestedParadox.Players;

namespace MainCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 distanceOffset = new Vector3();
        private TempCharacter tempCharacter;
        private Transform myTransform;
        private IReadOnlyReactiveProperty<int> characterDirection;

        private NestedParadox.Players.PlayerInput _playerinput;

        private void Start()
        {
            _playerinput = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent <NestedParadox.Players.PlayerInput>();
            myTransform = transform;
            characterDirection = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<NestedParadox.Players.PlayerInput>().CurrentDirection;
            characterDirection.Subscribe(x =>
            {
                distanceOffset = new Vector3(distanceOffset.x * -1, distanceOffset.y, distanceOffset.z);
            });
        }

        private void FixedUpdate()
        {
            //プレイヤーがステージの端にいる時
            if(myTransform.position.x < 6.4f)
            {
                myTransform.position = new Vector3(6.4f, 1.5f, -10);
            }
            else if(myTransform.position.x > 31)
            {
                myTransform.position = new Vector3(31, 1.5f, -10);
            }
            else
            {
                myTransform.position += new Vector3(Mathf.Lerp(myTransform.position.x, _playerinput.MyTransform.position.x - distanceOffset.x, 0.05f), 0, 0) - myTransform.position;

                myTransform.position += new Vector3(0, Mathf.Lerp(myTransform.position.y, _playerinput.MyTransform.position.y - distanceOffset.y, 0.0001f), 0) - myTransform.position;
            }            
        }
    }

}
