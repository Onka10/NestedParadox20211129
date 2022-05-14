using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using MainCamera;
using NestedParadox.Players;


namespace NestedParadox.Managers
{
    public class PhaseBase : MonoBehaviour
    {
        [SerializeField] protected TempCamera mainCamera;       
        [SerializeField] protected PlayerCore player;        

        public virtual async UniTask Execute()
        {
            await UniTask.Yield();
        }
    }
}