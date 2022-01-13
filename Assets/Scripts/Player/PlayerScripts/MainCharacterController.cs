using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{

    [SerializeField] CharacterMove characterMove;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform myTransform;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D) && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Die")))
        {
            characterMove.Move(1);
        }
        else if(Input.GetKey(KeyCode.A) && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Die")))
        {
            characterMove.Move(-1);
        }

        if( Input.GetKeyDown(KeyCode.W) && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Die")))
        
        {
            characterMove.Jump();
        }

        if(Input.GetKeyDown(KeyCode.S) && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack4")) && !(animator.GetCurrentAnimatorStateInfo(0).IsName("Die")))
        {
            characterMove.Attack();
        }
    }
}
