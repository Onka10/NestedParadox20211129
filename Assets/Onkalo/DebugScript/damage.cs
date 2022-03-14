using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NestedParadox.Players;

namespace Test{
public class damage : MonoBehaviour
    {
        [SerializeField]PlayerCore pc;
        public void ddamge(){
            pc.Damaged(10);
        }
    }
}
