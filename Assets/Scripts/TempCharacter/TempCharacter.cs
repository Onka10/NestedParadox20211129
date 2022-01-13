using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TempCharacter : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float maxSpeed;
    [SerializeField] float movingPower;

    private ReactiveProperty<Vector3> currentDirection = new ReactiveProperty<Vector3>();
    public IReadOnlyReactiveProperty<int> CurrentDirection => currentDirection.Select(x => x.x < 0 ? 1 : -1).ToReactiveProperty<int>();

    private Transform myTransform;
    public Transform MyTransform { get { return myTransform; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = transform;
        currentDirection.Value = myTransform.localScale;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        float maxSpeed_temp = 0;
        if(Input.GetKey(KeyCode.RightArrow))
        {
            myTransform.localScale = new Vector3(-0.07f, 0.07f, 0.07f);
            currentDirection.Value = myTransform.localScale;
            maxSpeed_temp = maxSpeed;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            myTransform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
            currentDirection.Value = myTransform.localScale;
            maxSpeed_temp = -1*maxSpeed;
        }
        rb.AddForce(new Vector3(movingPower * (maxSpeed_temp - rb.velocity.x), 0, 0));
    }
}


