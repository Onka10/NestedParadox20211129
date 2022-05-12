using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class OmnipotenceAttack : MonoBehaviour
{
    //?A?^?b?N??????????
    private bool isAttacking;
    public bool IsAttacking => isAttacking;

    //?A?^?b?N???\????????
    private bool canAttack;
    public bool CanAttack => canAttack;

    //???????R?}???h
    private BossCommand currentCommand;
    public BossCommand CurrentCommand => currentCommand;

    //?A?^?b?N?N?[???^?C??
    private float attackTimeCount;
    [SerializeField] float attackCoolTime;

    //?R?}???h
    [SerializeField] private List<BossCommand> attackCommands;

    private void Update()
    {
        attackTimeCount = Time.deltaTime;
        if(attackTimeCount > attackCoolTime)
        {
            canAttack = true;
        }
    }

    private BossCommand SelectCommand()
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

    public async void Execute(int commandID)
    {
        /*
        //コマンドをランダムで選択
        currentCommand = SelectCommand();
        if(currentCommand == null || isAttacking)
        {
            return;
        }
        */
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
        //攻撃開始
        attackTimeCount = 0;
        canAttack = false;
        isAttacking = true;
        await currentCommand.Execute();
        isAttacking = false;

    }
}
