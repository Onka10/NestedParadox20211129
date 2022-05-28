using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private PlayerMove playerMove;
        [SerializeField] GuardKunManager guardKunManager;
        [SerializeField] List<GameObject> monsterPrefabList;
        [SerializeField] MonsterRow monsterRow;
        [SerializeField] GameObject[] monstersSprite;//召喚時に表示する偽sprite
                                                     
        public List<MonsterBase> monsterList;
        public List<MonsterBase> monsterList_temp; //モンスターが破壊されてもnullのままにするリスト
        public int MonsterCount => monsterList.Count;

        private bool canSummon;
        public bool CanSummon => canSummon;

        // Start is called before the first frame update
        void Start()
        {
            playerMove = PlayerMove.I;
            monsterList = new List<MonsterBase>();
            monsterList_temp = new List<MonsterBase>();
            canSummon = true;            

        }

        // Update is called once per frame
        void Update()
        {
            monsterList.RemoveAll(a => a == null);

        }

        public void InActivateCurrentMonster()
        {
            foreach(MonsterBase monster in monsterList)
            {
                monster.IsInActive = true;
            }
        }

        public void ActivateCurrentMonster()
        {
            foreach (MonsterBase monster in monsterList)
            {
                monster.IsInActive = false;
            }
        }

        public async void Summon(CardID cardID)
        {
            SoundManager.Instance.PlaySE(SESoundData.SE.Summon);
            canSummon = false;
            //モンスターの召喚アニメーションの表示
            MonsterSprite monsterSprite_clone = Instantiate(monstersSprite[(int)cardID]).GetComponent<MonsterSprite>();
            Vector3 currentSummonPosition = monsterSprite_clone.SetSummonPosition(playerMove.transform.position, playerMove.CurrentDirection.Value); //召喚位置をset                    
            await monsterSprite_clone.SummonAnimation();//召喚完了するまで待つ
                                                        
            //　本体の召喚
            if (Instantiate(monsterPrefabList[(int)cardID]).TryGetComponent<MonsterBase>(out MonsterBase monster))
            {
                monster.transform.position = currentSummonPosition;               
                monster.SetPositionAndInitialize(monsterRow.GetNextPosition(monsterList_temp));
                monsterList.Add(monster);
                if (monsterList_temp.Any(x => x == null))
                {
                    int index = monsterList_temp.FindIndex(x  => x == null);
                    monsterList_temp.Insert(index, monster);
                }
                else
                {
                    monsterList_temp.Add(monster);
                }
            }
            canSummon = true;
        }

        //ガードクインテット
        public async void QuintetSummon()
        {
            canSummon = false;
            GuardKun guardKun = guardKunManager.SelectQuintetSummonGuardKun();
            MonsterSprite monsterSprite_clone = Instantiate(monstersSprite[1]).GetComponent<MonsterSprite>();
            Vector3 currentSummonPosition = monsterSprite_clone.SetSummonPosition(playerMove.transform.position, playerMove.CurrentDirection.Value); //召喚位置をset                    
            await guardKun.MoveAndStop(currentSummonPosition);
            Debug.Log("準備完了");
            SoundManager.Instance.PlaySE(SESoundData.SE.Summon);
            //モンスターの召喚アニメーションの表示            
            await monsterSprite_clone.SummonAnimation();//召喚完了するまで待つ            
            //　本体の召喚
            for(int i=0; i<4; i++)
            {
                if (Instantiate(monsterPrefabList[1]).TryGetComponent<MonsterBase>(out MonsterBase monster))
                {
                    monster.transform.position = currentSummonPosition;
                    monsterList.Add(monster);
                    if(monsterList_temp.Any(x => x== null))
                    {
                        monsterList_temp.Insert(monsterList_temp.IndexOf(null), monster);
                    }
                    else
                    {
                        monsterList_temp.Add(monster);
                    }                    
                    monster.SetPositionAndInitialize(monsterRow.GetNextPosition(monsterList_temp));
                    await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());
                }
            }
            guardKun.SetActive();
            canSummon = true;
        }
    }
}