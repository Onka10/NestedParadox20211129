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
    public RectTransform contentView;

    public List<Scene_UIDeckButton> ui_deck;

                    //  ID  count
    private Dictionary<int, int> deck;


    private void Start()
    {
        deck = new Dictionary<int, int>();

        RefreshViews();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            contentView.position += Vector3.right * 20;
        }
        if (Input.GetKey(KeyCode.E))
        {
            contentView.position += Vector3.left*20;
        }
    }


    /// <summary>IDを入れると、現在のデッキデータと照らし合わせて、新しいカードを挿入します。</summary>
    /// <param name="targetUI">UICardButtonをそのまま入れてください。</param>
    /// <returns>挿入可能な場合、Trueを返します。</returns>
    public bool AddToDeck(Scene_UICardButton targetUI)
    {
        int count = 0;
        foreach (var k in deck.Keys)
        {
            count += deck[k];
        }

        // 10枚ないかどうか
        if (count >= 10) return false;

        // 初めての追加の場合
        if (!deck.ContainsKey(targetUI.ID))
        {
            deck[targetUI.ID] = 1;
        }
        else
        {
            switch (targetUI.rarelity)
            {
                default:
                    Debug.LogError("Unknown Card Rarelity.");
                    return false;
                case Scene_UICardButton.UI_Card_Rarelity.Normal:
                    if (deck[targetUI.ID] >= 3)
                        return false;
                    break;
                case Scene_UICardButton.UI_Card_Rarelity.Rare:
                    if (deck[targetUI.ID] >= 1)
                        return false;
                    break;
            }

            deck[targetUI.ID]++;
        }

        RefreshViews();



        int i;
        for (i = 0; i < 10; i++)
        {
            if (ui_deck[i].targetCard == null)
            {
                break;
            }
        }
        Debug.Log(i);

        ui_deck[i].scs = this;
        ui_deck[i].targetCard = targetUI;
        ui_deck[i].btn.interactable = true;
        ui_deck[i].desc.text = "ID : "+targetUI.ID;

        return true;

    }

    public void RemoveFromDeck(int ID)
    {
        deck[ID]--;
        RefreshViews();


    }

    /// <summary>現在カード枚数をリフレッシュします。</summary>
    public void RefreshViews()
    {
        int count = 0;
        foreach (var k in deck.Keys)
        {
            Debug.Log(k + "  " + deck[k]);
            count += deck[k];
            Debug.Log(k + "  " + deck[k]);
        }
        txt.text = count.ToString() + " / 10";
        button_Play.interactable = count >= 10;
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

        if(deck.Count >= 10)
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
