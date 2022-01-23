using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class EnemyMoving : MonoBehaviour
{
    private readonly ReactiveProperty<bool> isGrounded = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> IsGrounded => isGrounded;
    private readonly ReactiveProperty<bool> isJumping = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<bool> IsJumping => isJumping;

    private int direction;

    [SerializeField] float rayDistance;
    [SerializeField] float ray2AndRay3Distance;
    [SerializeField] Vector2 ray2;
    [SerializeField] Vector2 ray3;
    [SerializeField] Vector3 jumpingPower;
    [SerializeField] float movingSpeed;
    [SerializeField] LayerMask targetLayer;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded.Subscribe(x => Debug.Log(x.ToString()));
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, targetLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, ray2, ray2AndRay3Distance, targetLayer);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, ray3, ray2AndRay3Distance, targetLayer);
        if(hit.collider != null || hit2.collider != null || hit3.collider != null)
        {
            isGrounded.Value = true;
        }
        else
        {
            isGrounded.Value = false;
        }
        Debug.DrawRay(transform.position, Vector2.down * rayDistance);
        Debug.DrawRay(transform.position, ray2 * ray2AndRay3Distance);
        Debug.DrawRay(transform.position, ray3 * ray2AndRay3Distance);
    }

    public void Jump(int direction)
    {
        if(direction == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb.AddForce(jumpingPower);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.AddForce(new Vector3(jumpingPower.x * -1, jumpingPower.y, jumpingPower.z));
        }
    }

    public void Move(int direction)
    {
        if(direction == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector3(movingSpeed, 0, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector3(-1 * movingSpeed, 0, 0);
        }
    }

}
