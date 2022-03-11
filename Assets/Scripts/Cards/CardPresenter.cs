using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPresenter : MonoBehaviour
{
    [SerializeField] NestedParadox.Cards.CardManager c;
    public List<GameObject> CardList = new List<GameObject>();


    public bool Check(int id){
        return CardList[id].GetComponent<IMagic>().CheckCondition();
        //モンスターの確認もここでやるとおもう
        //現状、Check & Doになってしまってる
    }

    // public void Execute(int id){
    //     CardList[id].GetComponent<IMagic>().ExecutionMagic();
    // }
}
