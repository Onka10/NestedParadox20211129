using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_CardSelect : MonoBehaviour
{

    [SerializeField] private string sceneName_Back;
    [SerializeField] private string sceneName_Play;

    public Text txt;
    public RectTransform parent_deck;
    public RectTransform parent_cards;
    public Button button_Play;

    public int cardCount;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        txt.text = cardCount.ToString() + " / 10";
        button_Play.interactable = cardCount >= 10;
    }

    public void OnButton_Play()
    {

        var ch = parent_deck.GetComponentsInChildren<Scene_UICardButton>();
        GameObject go = new GameObject("DeckData From Card Select Scene");
        SelectedDeckData sdd = go.AddComponent<SelectedDeckData>();
        sdd.deckData = new int[10];
        for (int i = 0; i < 10; i++)
        {
            sdd.deckData[i] = ch[i].ID;
        }
        DontDestroyOnLoad(go);

        if(cardCount >= 10)
            SceneManager.LoadScene(sceneName_Play);
    }

    public void OnButton_Back()
    {
        SceneManager.LoadScene(sceneName_Back);
    }

    public void OnButton_Auto()
    {
        // here for auto card set program.
    }


}
