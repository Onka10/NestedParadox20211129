using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NestedParadox.Stages;

namespace NestedParadox.Managers
{
    public class GameManager : MonoBehaviour
    {
        //[SerializeField] StageManager stageManager;
        // Start is called before the first frame update
        void Start()
        {
            //stageManager.Construct();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LoadToTitleScene()
        {
            SceneManager.LoadScene("TitleScene");
        }
        public void LoadToGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }
        public void LoadToSelectCardScene()
        {
            SceneManager.LoadScene("SelectCardScene");
        }
    }
}
