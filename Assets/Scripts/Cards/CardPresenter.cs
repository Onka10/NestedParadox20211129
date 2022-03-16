using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.Monsters;

public class CardPresenter : Singleton<CardPresenter>
{
    public List<GameObject> CardList = new List<GameObject>();
    [SerializeField] MonsterManager monsterManager;


    public bool Check(int id){
        //現状、Check & Doになってしまってる
        return CardList[id].GetComponent<ICard>().CheckTrigger();
    }

    public void Execute(int id){//オンカロの担当は魔法のみ
        var Magic = CardList[id].GetComponent<IMagic>();

        if(Magic != null)   Magic.Execution();
        else{
            //モンスターなら召喚
            // monsterManager.Summon(id);
            Debug.Log("モンスター召喚");
        }
    }
}
