using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class OmnipotenceAttack : MonoBehaviour
{
    //アタック中かどうか
    private bool isAttacking;
    public bool IsAttacking => isAttacking;

    //アタック可能かどうか
    private bool canAttack;
    public bool CanAttack => canAttack;

    //現在のコマンド
    private BossCommand currentCommand;
    public BossCommand CurrentCommand => currentCommand;

    //アタッククールタイム
    private float attackTimeCount;
    [SerializeField] float attackCoolTime;

    //コマンド
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

    public async void Attack()
    {
        //技の選択
        currentCommand = SelectCommand();
        if(currentCommand == null)
        {
            return;
        }

        //ここから攻撃が始まる
        attackTimeCount = 0;
        canAttack = false;
        isAttacking = true;
        await currentCommand.Execute();
        isAttacking = false;

    }
}
