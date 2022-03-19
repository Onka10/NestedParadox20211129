using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using NestedParadox.Players;

namespace NestedParadox.Monsters
{
    public class MonsterManager : Singleton<MonsterManager>
    {
        [SerializeField] Button[] button; //テスト用
        [SerializeField] EventSystem eventSystem; //テスト用
        private PlayerMove playerMove;
        [SerializeField] List<GameObject> monsterPrefabList;
        [SerializeField] MonsterRow monsterRow;
        [SerializeField] GameObject[] monstersSprite;//召喚時に表示する偽sprite
                                                     //
        private List<MonsterBase> monsterList;         
        public int MonsterCount => monsterList.Count;



        // Start is called before the first frame update
        void Start()
        {
            playerMove = PlayerMove.I;
            monsterList = new List<MonsterBase>();
            for(int i=0; i<button.Length; i++)
            {
                button[i].OnClickAsObservable().Subscribe(_ =>
                {
                    switch(eventSystem.currentSelectedGameObject.name)
                    {
                        case "SummonButton(0)":
                            Summon(CardID.DustDevil);
                            break;
                        case "SummonButton(1)":
                            Summon(CardID.GuardKun);
                            break;
                        case "SummonButton(2)":
                            Summon(CardID.SniperK);
                            break;
                        case "SummonButton(3)":
                            Summon(CardID.CatWarrior);
                            break;
                    }
                });//テスト用
            }            
        }

        // Update is called once per frame
        void Update()
        {
            monsterList.RemoveAll(a => a == null);
        }

        public async void Summon(CardID cardID)
        {
            //モンスターの召喚アニメーションの表示
            MonsterSprite monsterSprite_clone = Instantiate(monstersSprite[(int)cardID]).GetComponent<MonsterSprite>();
            Vector3 currentSummonPosition = monsterSprite_clone.SetSummonPosition(playerMove.transform.position, playerMove.CurrentDirection.Value); //召喚位置をset                    
            await monsterSprite_clone.SummonAnimation();//召喚完了するまで待つ            
            //　本体の召喚をspriteに任せるか迷い中
            if (Instantiate(monsterPrefabList[(int)cardID]).TryGetComponent<MonsterBase>(out MonsterBase monster))
            {
                monster.transform.position = currentSummonPosition;
                monsterList.Add(monster);
                monster.SetPositionAndInitialize(monsterRow.GetNextPosition());
            }
            
        }       
    }
}