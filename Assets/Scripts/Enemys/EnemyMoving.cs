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
    [SerializeField] Vector3 jumpingPower;
    [SerializeField] float movingSpeed;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance);
        if(hit.collider == null)
        {
            isGrounded.Value = false;
            return;
        }
        if(hit.collider.gameObject.tag == "FootHold1" || hit.collider.gameObject.tag == "FootHold2" || hit.collider.gameObject.tag == "BaseField" )
        {
            isGrounded.Value = true;
        }
        else
        {
            isGrounded.Value = false;
        }
        Debug.DrawRay(transform.position, Vector2.down * rayDistance);
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
