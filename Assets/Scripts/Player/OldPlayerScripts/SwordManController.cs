using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManController : MonoBehaviour
{
    [SerializeField] CharacterMove characterMove;
    [SerializeField] SwordConroller swordConroller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIdleToAnimState()
    {
        characterMove.charaState = CharacterMove.CharacterAnimState.Idle;
    }

    public void SetCollider()
    {
        swordConroller.SetCollider();
    }

    public void UnsetCollider()
    {
        swordConroller.UnsetCollider();
    }
}
