using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform myTransform;
    [SerializeField] Vector3 movingVelocity;
    [SerializeField]float maxSpeed;
    [SerializeField] float movingPower;
    [SerializeField] float jumpPower;
    [SerializeField] float gravityPower;
    public CharacterAnimState charaState;
    public bool IsCasual;
    public enum CharacterAnimState
    {
        Idle,
        Walk,
        Attack,
        GetHit,
        Jump
    }
    // Start is called before the first frame update
    void Start()
    {
        charaState = CharacterAnimState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x == 0)
        {
            animator.SetBool("WalkBool", false);
        }

        if(-3.2f <= myTransform.position.y && myTransform.position.y <= -3.1f && animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            animator.SetTrigger("GroundTrigger");
            animator.SetBool("WalkBool", false);
            charaState = CharacterAnimState.Idle;
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, -1 * gravityPower));
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

    }

    public void Move(int direction)
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            rb.AddForce(movingVelocity * direction * 0.5f, ForceMode2D.Force);
        }
        else
        {
            animator.SetBool("WalkBool", true);
            rb.AddForce(movingVelocity * direction, ForceMode2D.Force);
        }
        if(direction == 1)
        {
            myTransform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            myTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    public  void Attack()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            return;
        }
        animator.SetTrigger("AttackTrigger");
     
        rb.velocity = new Vector3(0, 0, 0);
    }

    public void Jump()
    {
        charaState = CharacterAnimState.Jump;
        animator.SetTrigger("JumpTrigger");
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Force);
     
    }

    public void SetIdleToAnimState()
    {
        charaState = CharacterAnimState.Idle;
    }

    public void GetHit()
    {
        
        animator.SetTrigger("GetHitTrigger");
    }
}
