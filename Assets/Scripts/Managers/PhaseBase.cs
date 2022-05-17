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
        [SerializeField] public PhaseBase next;

        public virtual async UniTask Execute()
        {
            await UniTask.Yield();
        }
    }
}