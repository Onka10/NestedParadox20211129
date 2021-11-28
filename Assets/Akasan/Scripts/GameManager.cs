using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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