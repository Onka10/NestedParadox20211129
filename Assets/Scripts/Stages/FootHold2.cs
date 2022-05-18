using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace NestedParadox.Stages
{
    public class FootHold2 : MonoBehaviour
    {
        [SerializeField] float movingSpeed;
        [SerializeField] float movingDistance;
        [SerializeField] bool leftAndRight;
        [SerializeField] Rigidbody2D rb;
        private int startDelay;
        // Start is called before the first frame update
        void Start()
        {
            startDelay = Random.Range(0, 2000);
            if(leftAndRight)
            {
                MoveLeftAndRight();
            }
            else
            {
                MoveUpAndDown();
            }            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private async void MoveUpAndDown()
        {
            await UniTask.Delay(startDelay, cancellationToken: this.GetCancellationTokenOnDestroy());
            rb.velocity = new Vector3(0, movingSpeed, 0);
            Vector3 firstPosition = transform.position;
            await UniTask.WaitUntil(() => (transform.position.y - firstPosition.y) > movingDistance/2.0f, cancellationToken: this.GetCancellationTokenOnDestroy());
            while (true)
            {
                rb.velocity = new Vector3(0, -1*movingSpeed, 0);
                Vector3 upPosition = transform.position;
                await UniTask.WaitUntil(() => (-1 * (transform.position.y - upPosition.y)) > movingDistance, cancellationToken: this.GetCancellationTokenOnDestroy()) ;
                rb.velocity = new Vector3(0, movingSpeed, 0);
                Vector3 downPosition = transform.position;
                await UniTask.WaitUntil(() => (transform.position.y - downPosition.y) > movingDistance, cancellationToken: this.GetCancellationTokenOnDestroy());
            }
            
        }

        private async void MoveLeftAndRight()
        {
            await UniTask.Delay(startDelay, cancellationToken: this.GetCancellationTokenOnDestroy());
            rb.velocity = new Vector3(movingSpeed, 0, 0);
            Vector3 firstPosition = transform.position;
            await UniTask.WaitUntil(() => (transform.position.x - firstPosition.x) > movingDistance / 2.0f, cancellationToken: this.GetCancellationTokenOnDestroy());
            while (true)
            {
                rb.velocity = new Vector3(-1* movingSpeed, 0, 0);
                Vector3 upPosition = transform.position;
                await UniTask.WaitUntil(() => (-1 * (transform.position.x - upPosition.x)) > movingDistance, cancellationToken: this.GetCancellationTokenOnDestroy());
                rb.velocity = new Vector3(movingSpeed, 0, 0);
                Vector3 downPosition = transform.position;
                await UniTask.WaitUntil(() => (transform.position.x - downPosition.x) > movingDistance, cancellationToken: this.GetCancellationTokenOnDestroy());
            }
        }
    }

}
