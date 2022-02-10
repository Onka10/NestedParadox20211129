using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;


namespace NestedParadox.Monsters
{
    public class MonsterManager : MonoBehaviour
    {
        [SerializeField] Button button; //テスト用
        private int number;
        [SerializeField] List<GameObject> monsterPrefabList;
        [SerializeField] MonsterRow monsterRow;
        private List<MonsterBase> monsterList;         
        public int MonsterCount => monsterList.Count;

        // Start is called before the first frame update
        void Start()
        {
            monsterList = new List<MonsterBase>();
            button.OnClickAsObservable().Select(x => (CardID)Enum.ToObject(typeof(CardID), number%4)).Subscribe(x =>
            {
                Summon(x);
                number++;
            });//テスト用
        }

        // Update is called once per frame
        void Update()
        {
            monsterList.RemoveAll(a => a == null);
        }

        public void Summon(CardID cardID)
        {
            MonsterBase monster;
            Instantiate(monsterPrefabList[(int)cardID]).TryGetComponent<MonsterBase>(out monster);
            monsterList.Add(monster);
            monster.SetPositionAndInitialize(monsterRow.GetNextPosition());            
        }
    }
}