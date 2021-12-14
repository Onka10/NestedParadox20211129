using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
    }

    public void Unpause()
    {
        pausePanel.SetActive(false);
    }

}
