using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace NestedParadox.Stages
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] GameObject[] stage;
        private GameObject currentStage;

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
            int random = Random.Range(0, stage.Length);
            currentStage = Instantiate(stage[random]);
        }

        public void DeleteCurrentStage()
        {
            Destroy(currentStage);
        }

    }
}
