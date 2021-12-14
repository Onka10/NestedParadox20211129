using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToSelectCharacterScene()
    {
        SceneManager.LoadScene("SelectCharacterScene");
    }

}
