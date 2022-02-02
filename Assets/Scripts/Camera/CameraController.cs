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

        private TempCharacter _playerinput;

        private void Start()
        {
            _playerinput = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
            myTransform = transform;
            characterDirection = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<PlayerInput>().CurrentDirection;
            characterDirection.Subscribe(x =>
            {
                distanceOffset = new Vector3(distanceOffset.x * -1, distanceOffset.y, distanceOffset.z);
            });
        }

        private void FixedUpdate()
        {
            myTransform.position = new Vector3(Mathf.Lerp(myTransform.position.x, _playerinput.MyTransform.position.x - distanceOffset.x, 0.05f),
                                             Mathf.Lerp(myTransform.position.y, _playerinput.MyTransform.position.y - distanceOffset.y, 0.05f),
                                             Mathf.Lerp(myTransform.position.z, _playerinput.MyTransform.position.z - distanceOffset.z, 0.05f)
                                             );
        }
    }

}
