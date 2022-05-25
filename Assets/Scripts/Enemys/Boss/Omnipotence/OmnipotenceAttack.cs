using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;

public class OmnipotenceAttack : MonoBehaviour
{
    //?A?^?b?N??????????
    private bool isAttacking;
    public bool IsAttacking => isAttacking;

    //?A?^?b?N???\????????
    private ReactiveProperty<bool> canAttack = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> CanAttack => canAttack;

    //???????R?}???h
    private BossCommand currentCommand;
    public BossCommand CurrentCommand => currentCommand;

    //?A?^?b?N?N?[???^?C??
    private float attackTimeCount;
    [SerializeField] float attackCoolTime;

    //?R?}???h
    [SerializeField] private List<BossCommand> attackCommands;

    private void Start()
    {
        canAttack.Value = false;
        isAttacking = false;
    }

    private void Update()
    {
        currentCommand = RandomSelectCommand();
        if(currentCommand != null && !isAttacking)
        {
            canAttack.Value = true;
        }
        else
        {
            canAttack.Value = false;
        }
    }

    private BossCommand RandomSelectCommand()
    {
        List<BossCommand> possibleCommands = new List<BossCommand>();
        foreach(BossCommand attackCommand in attackCommands)
        {
            if(attackCommand.CanAttack)
            {
                possibleCommands.Add(attackCommand);
            }
        }
        if(possibleCommands.Count == 0)
        {
            return null;
        }

        int random = Random.Range(0, possibleCommands.Count);
        return possibleCommands[random];
    }

    public async void Execute()
    {               
        /* テスト用
        switch(commandID)
        {
            case 0:
                currentCommand = attackCommands[0];
                break;
            case 1:
                currentCommand = attackCommands[1];
                break;
            case 2:
                currentCommand = attackCommands[2];
                break;
            case 3:
                currentCommand = attackCommands[3];
                break;
        }
        */

        //攻撃開始               
        isAttacking = true;
        await currentCommand.Execute();
        isAttacking = false;
    }
}
