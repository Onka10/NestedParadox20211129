using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NestedParadox.Stages
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] GameObject[] stage;


        void Start()
        {            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Construct()
        {
            RandomGenerateStage();            
        }

        public void RandomGenerateStage()
        {
            int random = Random.Range(9, stage.Length);
            Instantiate(stage[random]);
        }

    }
}
