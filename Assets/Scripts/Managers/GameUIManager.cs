using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] Text hpText;//テスト
    private TempCharacter player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();
        player.Hp_test.Subscribe(x => hpText.text = $"体力: {x}").AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
    }
    public void UnPause()
    {
        pausePanel.SetActive(false);
    }


}
