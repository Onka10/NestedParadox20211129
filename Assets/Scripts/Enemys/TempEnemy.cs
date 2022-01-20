using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float movingPower;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.AddForce(new Vector3(movingPower, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.AddForce(new Vector3(-1*movingPower, 0, 0));
        }
    }
}
