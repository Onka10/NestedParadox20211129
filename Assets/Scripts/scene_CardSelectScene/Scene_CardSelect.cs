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
    public Button button_Back;
    public Button button_Play;
    public RectTransform contentView;

    public List<Scene_UIDeckButton> ui_deck;

                    //  ID  count
    private Dictionary<int, int> deck;
    private int deckCount = 0;

    private void Start()
    {
        deck = new Dictionary<int, int>();

        // load post deckData here.

        RefreshViews();
    }

    void Update()
    {
        // move RectTransform(Cards)
        if (Input.GetKey(KeyCode.Q))
        {
            contentView.position += Vector3.right * 20;
        }
        if (Input.GetKey(KeyCode.E))
        {
            contentView.position += Vector3.left*20;
        }
    }


    /// <summary>ID������ƁA���݂̃f�b�L�f�[�^�ƏƂ炵���킹�āA�V�����J�[�h��}�����܂��B</summary>
    /// <param name="targetUI">UICardButton�����̂܂ܓ���Ă��������B</param>
    /// <returns>�}���ł����ꍇ�ATrue��Ԃ��܂��B</returns>
    public bool AddToDeck(Scene_UICardButton targetUI)
    {
        int count = 0;
        foreach (var k in deck.Keys)
        {
            count += deck[k];
        }

        // 10���ȏ�̏ꍇ�A�J�[�h��}���ł��Ȃ�����false�ŏI���B
        if (count >= 10) return false;

        // ���߂Ă̒ǉ��̏ꍇ
        if (!deck.ContainsKey(targetUI.ID))
        {
            deck[targetUI.ID] = 1;
        }
        else    // ���ł�ID��Deck��Dict�ɑ��݂���ꍇ
        {
            switch (targetUI.rarelity)  //�@���A�x�ɂ���Ė���������m�F�B��������̏ꍇ�̓J�[�h��}���ł��Ȃ�����false�ŏI���B
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

        // �󂫃f�b�L�ւ̃J�[�h�}���{�����ڕύX�p����
        int i;
        for (i = 0; i < 10; i++)
        {
            if (ui_deck[i].targetCard == null)
            {
                break;
            }
        }

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

    /// <summary>���݃J�[�h���������t���b�V�����܂��B</summary>
    public void RefreshViews()
    {
        deckCount = 0;
        foreach (var k in deck.Keys)
        {
            deckCount += deck[k];
        }
        txt.text = deckCount.ToString() + " / 10";
        button_Play.interactable = deckCount >= 10;
    }

    public void OnButton_Play()
    {

        if (deckCount < 10) return;

        var ch = parent_deck.GetComponentsInChildren<Scene_UICardButton>();
        GameObject go = new GameObject("DeckData From Card Select Scene");
        SelectedDeckData sdd = go.AddComponent<SelectedDeckData>();
        sdd.deckData = new int[10];
        for (int i = 0; i < 10; i++)
        {
            sdd.deckData[i] = ui_deck[i].targetCard.ID;
        }

        DontDestroyOnLoad(go);

        button_Play.interactable = false;

        SceneManager.LoadScene(sceneName_Play);
    }

    public void OnButton_Back()
    {
        button_Back.interactable = false;
        SceneManager.LoadScene(sceneName_Back);
    }

    public void OnButton_Auto()
    {
        // here for auto card set program.
    }


}
